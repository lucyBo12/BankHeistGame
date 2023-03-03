using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioSource footsteps;
    CharacterLocomotion characterMovement;

    public void Start()
    {
        

    }

    public void Update()
    {
        if(characterMovement.IsMoving)
        {
            footsteps.Play();
            Debug.Log("Works");
        }
        else
        {
            footsteps.Pause();
        }

    }
}
