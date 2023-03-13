using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCheck : MonoBehaviour
{
    static bool done;

    public void Update()
    {
        done = WireBehaviourLeft.done;
        if(done)
        {
            gameObject.SetActive(false);
        }
    }
}
