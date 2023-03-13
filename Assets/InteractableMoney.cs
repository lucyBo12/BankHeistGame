using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableMoney : Interactable
{
    [SerializeField] int value = 10;
    public AudioSource moneySound;
    public GameObject moneyText;

    public override void Interact(Transform user)
    {
        user.GetComponent<Character>().money += value;
        int money = user.GetComponent<Character>().money;
        moneySound.Play();
        base.Interact(user);
        moneyText = GameObject.FindGameObjectWithTag("MoneyUI"); 
        moneyText.GetComponent<MoneyUI>().UpdateCounter(value, money);

    }
}
