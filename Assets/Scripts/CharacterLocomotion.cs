using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    //Properties
    public CharacterController controller;
    public Animator animator;
    public float speed = 2f;

    //Attributes
    public float angle { get; private set; }
    public float currentSpeed { get; private set; }
    private InputMaster.PlayerActions PlayerActions => GameManager.Input.Player;


    private void Start() {
        var action = GameManager.Input.Player;
        action.Enable();
    }

    private void Update() {
        Aim(PlayerActions.Aim.ReadValue<Vector2>());
        Move(PlayerActions.Movement.ReadValue<Vector2>());
    }

    private void Move(Vector2 input) {
        currentSpeed = PlayerActions.Crouch.IsPressed() ? speed * 0.5f : speed;
        controller.Move((new Vector3(input.x, 0, input.y) * currentSpeed) * Time.deltaTime);
        AnimateMovement(input);
    }
        

    private void Aim(Vector2 mousePosition) {
        var center = Camera.main.WorldToScreenPoint(transform.position);

        mousePosition.x = mousePosition.x - center.x;
        mousePosition.y = mousePosition.y - center.y;

        angle = Mathf.Atan2(mousePosition.x, mousePosition.y) * Mathf.Rad2Deg;
        angle = angle < 0 ? angle + 360 : angle;
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    private void AnimateMovement(Vector2 input) {

        animator.SetFloat("inputx", input.x, 0.1f, Time.deltaTime);
        animator.SetFloat("inputy", input.y, 0.1f, Time.deltaTime);
    }


}
