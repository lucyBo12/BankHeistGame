using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject blood;

 

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Shot " + other.name);

        if (other.gameObject.tag == "Enemy")
        {
            GameObject splat = Instantiate(blood, transform.position -new Vector3(0,0,1), transform.rotation);
            Destroy(splat,5);
        }

        Destroy(gameObject);

        
    }
}
