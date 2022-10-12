using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int MaxHP = 100;
    public int currentHP;   

    public HealthBar HP;


    public int MaxArmor = 50;
    public int currentArmor; 

    public ArmorBar Armor; 

    // Start is called before the first frame update
    void Start()
    {
        currentHP = MaxHP;
        HP.SetMaxHP(MaxHP);
        Armor.SetMaxArmor(MaxArmor);  
    }


}
