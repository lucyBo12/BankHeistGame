using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    public CharacterController controller;
    public float mouseOffset = 90;
    public float speed = 2f;

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

    private void Move(Vector2 input) => 
        controller.Move((new Vector3(input.x, 0, input.y) * speed) * Time.deltaTime);

    private void Aim(Vector2 mousePosition) {
        var center = Camera.main.WorldToScreenPoint(transform.position);

        mousePosition.x = mousePosition.x - center.x;
        mousePosition.y = mousePosition.y - center.y;

        var angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, -(angle + mouseOffset) + 180, 0);
    }


}
