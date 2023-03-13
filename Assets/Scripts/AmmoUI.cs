using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    //public static AmmoUI Instance;
    //private void Awake() => Instance = this;
    public GameObject player;
    Weapon weapon;
    public TextMeshProUGUI ammoText;
    int ammo;
    int clip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        weapon = player.GetComponent<InventoryManager>().activeWeapon;
        ammo = weapon.clip.ammo;
        clip = weapon.clip.clip;
        ammoText.SetText(ammo + "/" + clip);
    }
}
