using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableMoney : Interactable
{
    [SerializeField] int value = 10;

    public override void Interact(Transform user)
    {
        user.GetComponent<Character>().money += value;
        base.Interact(user);
    }
}
