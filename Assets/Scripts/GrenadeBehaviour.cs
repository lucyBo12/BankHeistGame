using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBehaviour : MonoBehaviour
{
    public GameObject explode;
    float lifetime = 3f; //lifetime of cell

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime); 
    }

    /**
     * create explode object at position of grenade
     * Destroys grenade
     */
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explode, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    /**
     * Creates explode object at position of grenade
     */
    private void OnDestroy()
    {
        Instantiate(explode, transform.position, transform.rotation);
    }
}
