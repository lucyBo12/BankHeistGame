using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableWeapon : Interactable
{
    [SerializeField]private GameObject weapon;

    public override void Interact(Transform user)
    {
        base.Interact(user);
    }

}
