using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioClip[] footsteps;
    public AudioSource source;

    float walkRate;
    float threshold;
    float timeSinceLast;

    public CharacterLocomotion player;

    // Update is called once per frame
    void Update()
    {
        if(player.IsMoving)
        {
            if(Time.time - timeSinceLast > walkRate)
            {
                timeSinceLast = Time.time;
                source.PlayOneShot(footsteps[Random.Range(0, footsteps.Length)]);

            }
        }
    }
}
