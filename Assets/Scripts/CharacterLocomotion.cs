using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    //Properties
    public CharacterController controller;
    public float mouseOffset = 90;
    public float speed = 2f;

    //Attributes
    public float angle { get; private set; }
    private InputMaster.PlayerActions PlayerActions => GameManager.Input.Player;


    private void Start()
    {
        var action = GameManager.Input.Player;
        action.Enable();
    }

    private void Update()
    {
        Aim(PlayerActions.Aim.ReadValue<Vector2>());
        Move(PlayerActions.Movement.ReadValue<Vector2>());
    }

    private void Move(Vector2 input) {
        bool crouch = PlayerActions.Crouch.IsPressed();
        controller.Move((new Vector3(input.x, 0, input.y) * (crouch ? (speed / 3) : speed)) * Time.deltaTime);
    }
        

    private void Aim(Vector2 mousePosition) {
        var center = Camera.main.WorldToScreenPoint(transform.position);

        mousePosition.x = mousePosition.x - center.x;
        mousePosition.y = mousePosition.y - center.y;

        angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        angle = -(angle + mouseOffset) + 180;
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }


}
