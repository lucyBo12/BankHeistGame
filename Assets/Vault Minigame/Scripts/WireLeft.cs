using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Colours {red, yellow, green, blue};
public class WireLeft : MonoBehaviour
{

    public bool isMoving = false;
    public bool canMove = false;
    public bool joined = false;
    public Vector3 joinedPos;
    public Colours colour;
    public Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
