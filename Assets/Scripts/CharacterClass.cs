using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClass : MonoBehaviour
{

    public CharacterController character;
    public CharacterLocomotion player;

    //player stats
    public int health;
    public bool alive = true;
    
    //equipment
    public int ammo;
    public int grenades;
    public GameObject gun;
    public Transform[] weapons;

    //loadout
    public bool isSwitching => player.PlayerActions.ChangeWeapon.IsPressed();
    public bool isSwitching2 => player.PlayerActions.ChangeWeapon1.IsPressed();
    public bool isSwitching3 => player.PlayerActions.ChangeWeapon2.IsPressed();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IsAlive();

        //switch weapon
        if (isSwitching)
        {
            ChangeWeapon(1);
        }
        if (isSwitching2)
        {
            ChangeWeapon(2);
        }
        if (isSwitching3)
        {
            ChangeWeapon(3);
        }
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
    int GetHealth()
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
    int GetAmmo()
    {
        return ammo;
    }

    /**
     * Changes player selected weapon
     */
    public void ChangeWeapon(int num)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == num)
            {
                weapons[i].gameObject.SetActive(true);
                gun = weapons[i].gameObject;
            }
            else
            {
                weapons[i].gameObject.SetActive(false);
            }
        }
    }

    /**
     * Identifies paramaters tag
     * checks if tag is equal to "Weapon"
     * 
     * @param GameObject the item to be analysed
     */
    public bool IsWeapon(GameObject item)
    {
        if (item.tag == "Weapon")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
