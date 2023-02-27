using UnityEngine;

public class Character : MonoBehaviour
{
    public int maxHealth;
    public InventoryManager inventoryManager;

    CharacterLocomotion player;
    Animator animator;

    //player stats
    public int health;
    public bool dead;
    public int money;
    public int noOfdeaths;
    public string playerName;

    //equipment
    public int ammo;
    public int grenades;
    public GameObject gun;
    public Transform[] weapons;

    //loadout
    //public bool isSwitching => player.PlayerActions.ChangeWeapon.IsPressed();
    // public bool isSwitching2 => player.PlayerActions.ChangeWeapon1.IsPressed();
    //public bool isSwitching3 => player.PlayerActions.ChangeWeapon2.IsPressed();




    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        health = 10;
    }

    // Update is called once per frame
    void Update()
    {

        //switch weapon
        //  if (isSwitching)
        //    {
        //        ChangeWeapon(1);
        //     }
        //     if (isSwitching2)
        //     {
        //        ChangeWeapon(2);
        //     }
        //     if (isSwitching3)
        //     {
        //          ChangeWeapon(3);
        //     }
    }


    public void gotShot()
    {
        health--;
        if (health <= 0)
        {
            dead = true;
            animator.SetBool("isDead", dead);
            noOfdeaths++;
            player.speed = 0f;
        }
    }

    public void revived()
    {
        dead = false;
        health = 5;
        player.speed = 2f;
    }


    /**
     * returns player health
     * sets player status to dead if health == 0
     */
    int GetHealth()
    {

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
    *Returns player money count
    */
    int GetMoney()
    {
        return money;
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
