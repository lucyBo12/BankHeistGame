using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretController : MonoBehaviour
{


    public Turret turret;
    public BulletFire bulletFire;
    private InputAction fire;
    private InputAction look;

    public float angle { get; private set; }

    private void Awake()
    {
        turret = new Turret();
    }
    private void OnEnable()
    {
        look = turret.Player.Look;
        look.Enable();

        fire = turret.Player.Fire;
        fire.Enable();
        fire.performed += Fire;
    }

    private void OnDisable()
    {
        look.Disable();
        
        fire.Disable();

    }

    private void Update()
    {
        Vector2 mousePos = look.ReadValue<Vector2>();
        Aim(mousePos);
    }
    
    private void Aim(Vector2 mousePosition) {
        //Get GameObject position in screen space
        var center = Camera.main.WorldToScreenPoint(transform.position);

        //Offset the difference
        mousePosition.x = mousePosition.x - center.x;
        mousePosition.y = mousePosition.y - center.y;

        //Apply Tan to get rotation in radians and convert to degrees
        angle = Mathf.Atan2(mousePosition.x, mousePosition.y) * Mathf.Rad2Deg;
        
        //Offset to ensure absolute value (0-360)
        angle = angle < 0 ? angle + 360 : angle;

        //Apply calculated angled to Transform
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    private void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("FIRE!");
        bulletFire.Shoot();
    }


}
