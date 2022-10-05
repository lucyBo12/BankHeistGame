using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{


    void Start()
    {
        GameManager.Input.Enable();
        GameManager.Input.Player.Aim.performed += evt => ShowDebug();
        GameManager.Input.Player.Movement.performed += evt => ShowDebug();
    }

    void ShowDebug() {
        Debug.Log($"Aim: {GameManager.Input.Player.Aim.ReadValue<Vector2>()} " +
            $"\nMovement: {GameManager.Input.Player.Movement.ReadValue<Vector2>()}");
    }

}
