using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WireBehaviourLeft : MonoBehaviour
{

    WireLeft wire;
    public bool mouseDown;
    LineRenderer line;
    static int wireCount = 0;
    public GameObject canvas;
    public static bool done = false;


    // Start is called before the first frame update
    void Start()
    {
        wire = gameObject.GetComponent<WireLeft>();
        line = gameObject.GetComponent<LineRenderer>();
        mouseDown = false;


    }

    // Update is called once per frame
    void Update()
    {
        //move wire with player mouse
        Move();

        //move linerenderer to mimic wire
        line.SetPosition(1, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z));



    }

    private void Move()
    {
        if(wire.canMove && mouseDown)
        {

            wire.isMoving = true;
            Debug.Log("Moving");
            float mouseX = Input.mousePosition.x;
            float mouseY = Input.mousePosition.y;
            Debug.Log(Input.mousePosition);
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);




        }
        else
        {
            wire.isMoving = false;
        }
    }

    private void OnMouseUp()
    {
        mouseDown = false;
        if(wire.joined)
        {
            gameObject.transform.position = wire.joinedPos;
        }
        else
        {
            gameObject.transform.position = wire.startPos;
        }
        
    }

    private void OnMouseDown()
    {
        mouseDown = true;
    }

    private void OnMouseExit()
    {
        if(!wire.isMoving)
        {
            wire.canMove = false;
        }
    }

    private void OnMouseOver()
    {
        Debug.Log("Hovering");
        wire.canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<WireRight>())
        {
            WireRight wireR = collision.GetComponent<WireRight>();
            //Debug.Log(wireR.colour);
            if(wireR.colour == wire.colour)
            {
                wire.joined = true;
                wireR.joined = true;
                wire.joinedPos = collision.transform.position;
                wireCount++;
                Debug.Log(wireCount);
                if(wireCount == 4 )
                {
                    
                    Debug.Log("Done");
                    canvas.SetActive(false);
                    done = true;
                    //vaultDoor.SetActive(false);
                    SceneManager.LoadScene("BankLevel");
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "wireR")
        {
            
            if (collision.GetComponent<WireLeft>())
            {
                WireRight wireR = collision.GetComponent<WireRight>();
                wireR.joined = false;
                wire.joined = false;
            }
        }
    }
}
