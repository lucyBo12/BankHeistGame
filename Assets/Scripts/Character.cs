using UnityEngine;

public class Character : MonoBehaviour
{
    public InventoryManager inventoryManager;

    CharacterLocomotion player;
    [SerializeField] Transform maskRoot;
    [SerializeField] Transform duffleRoot;
    Animator animator;

    //player stats
    public int health;
    public bool isDead => health <= 0;
    public bool safe = false;
    public int money { get; private set; }
    public int noOfdeaths;
    public string playerName;


    //equipment
    public int ammo;
    public int grenades;
    public GameObject gun;
    public Transform[] weapons;
    GameObject ammoUI;


    public void Start()
    {
        if (transform.CompareTag("Player"))
            GameManager.Players.Add(transform);
        playerName = PlayerNameInput.userInput;
        Debug.Log(playerName);
        
    }

    public void Damage(int damage)
    {
        health -= damage;
        
        animator?.SetBool("isDead", isDead);
        if (isDead)
        {
            gameObject.SetActive(false);
            noOfdeaths++;
            money = 0;

            if (CompareTag("Cop")) {
                GameManager.WantedLevel += Mathf.Pow(0.1f, (1f + (GameManager.WantedLevel * 0.1f)));
            }
        }
    }

    public void ResetCharacter() {
        health = 100;
        animator?.SetBool("isDead", isDead);
        ResetMoney();
    }

    public void SetMask(int id) {
        for (int i = 0; i < maskRoot.childCount; i++) { 
            maskRoot.GetChild(i).gameObject.SetActive(i == id || i == 2);
        }
    }

    public void AddMoney(int value) {
        money += value;
        duffleRoot.GetChild(0).gameObject.SetActive(money < 50000);
        duffleRoot.GetChild(1).gameObject.SetActive(money >= 50000 && money < 100000);
        duffleRoot.GetChild(2).gameObject.SetActive(money >= 100000);
    }

    //Set Money to zero
    public void ResetMoney()
    {
        money = 0;
        foreach (Transform child in duffleRoot)
        {
            child.gameObject.SetActive(false);
        }
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
