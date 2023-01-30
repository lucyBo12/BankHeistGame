using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBehaviour : MonoBehaviour
{

    //money values
    int roll = 200;
    int stack = 1000;
    int note = 100;

    public CharacterClass player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");
        if(other.tag == "Player")
        {
            player = other.GetComponent<CharacterClass>();
            gameObject.SetActive(false);
            if(this.tag == "moneyStack")
            {
                player.money = player.money + stack;
            }

            if (this.tag == "moneyRoll")
            {
                player.money = player.money + roll;
            }

            if (this.tag == "moneyNote")
            {
                player.money = player.money + note;
            }
        }
    }
}
