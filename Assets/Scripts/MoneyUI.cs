using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    public TextMeshProUGUI totalMoneyText;
    public TextMeshProUGUI playerMoneyText;
    public TextMeshProUGUI addedMoneyText;
    public TextMeshProUGUI lostMoneyText;
    public int total;
    public int playerTot;
    public int tempTotal;

    public void Awake()
    {
        totalMoneyText.SetText("£" + 0);
        playerMoneyText.SetText("£" + 0);
        total = 0;
        playerTot = 0;
    }

    public void Update()
    {
        //totalMoneyText.SetText("£" + total);
        //playerMoneyText.SetText("£" + playerTot);
        //if( tempTotal != total)
        //{
        //    StartCoroutine(AddedMoney(total - tempTotal));
        //}
    }
    public void UpdateCounter(int moneyAdded, int playerTotal)
    {
        tempTotal = total;
        total += moneyAdded;
        playerTot = playerTotal;
        totalMoneyText.SetText("£" + total);
        playerMoneyText.SetText("£" + playerTotal);
        StartCoroutine(AddedMoney(moneyAdded));
        Debug.Log("Test");

        

    }

    IEnumerator AddedMoney(int moneyAdded)
    {
        addedMoneyText.SetText("+ £" + moneyAdded);
        yield return new WaitForSeconds(3);
        addedMoneyText.SetText("");
        tempTotal = total;
    }

    public void MinusMoney(int moneyLost, int total)
    {

    }




}
