using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Interactable
{


    public override void Interact()
    {
        if(destroyOnInteract)
            Destroy(gameObject);
    }

}
