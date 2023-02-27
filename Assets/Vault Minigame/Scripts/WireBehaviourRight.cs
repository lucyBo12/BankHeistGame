using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireBehaviourRight : MonoBehaviour
{

    WireRight wireR;
    int wireCount = 0;
    // Start is called before the first frame update
    void Start()
    {
       // wireR = gameObject.GetComponent<WireRight>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        wireR = gameObject.GetComponent<WireRight>();
        if (collision.GetComponent<WireLeft>())
        {
            WireLeft wireL = collision.gameObject.GetComponent<WireLeft>();
            //Debug.Log(wireL.colour);
            Debug.Log(wireR.colour);
            if(wireR.colour == wireL.colour)
            {
                Debug.Log("Trigger");
                wireR.joined = true;
                wireL.joinedPos = gameObject.transform.position;
                wireL.joined = true;
                wireCount++;
                if (wireCount == 4)
                {
                    Debug.Log("Done");
                }

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<WireLeft>())
        {
            WireLeft wireL = collision.GetComponent<WireLeft>();
            wireR.joined = false;
            wireL.joined = false;
        }
    }
}
