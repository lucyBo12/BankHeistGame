using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClass : MonoBehaviour
{

    public CharacterController controller;

    //player stats
    public int health;
    public bool alive = true;
    
    //equipment
    public int ammo;
    public int grenades;
    public GameObject gun;
    public Transform weapons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IsAlive();
    }

    /**
     * Returns player status 
     */
    bool IsAlive()
    {
        return alive;
    }

    /**
     * returns player health
     * sets player status to dead if health == 0
     */
    int Health()
    {
        if(health == 0)
        {
            alive = false;
        }
        return health;
    }


    /**
    *Returns plaurt ammo count
    *secondary weapon e.g pistol wont have ammo count
    */
    int AmmoCount()
    {
        return ammo;
    }    
}
