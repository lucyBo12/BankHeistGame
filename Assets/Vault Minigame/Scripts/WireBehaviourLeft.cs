using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireBehaviourLeft : MonoBehaviour
{

    WireLeft wire;
    public bool mouseDown = false;
    LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        wire = gameObject.GetComponent<WireLeft>();
        line = gameObject.GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        //move wire with player mouse
        Move();

        //move linerenderer to mimic wire
        line.SetPosition(1, new Vector3(gameObject.transform.position.x - 0.1f, gameObject.transform.position.y - 0.1f, gameObject.transform.position.z));



    }

    private void Move()
    {
        if(wire.canMove && mouseDown)
        {

            wire.isMoving = true;
            float mouseX = Input.mousePosition.x;
            float mouseY = Input.mousePosition.y;
            Vector3 mousePosition = Input.mousePosition;
            gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mouseX, mouseY, 0));
            
            

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
        wire.canMove = true;
    }
}
