using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class VaultUnlocker : MonoBehaviour
{
    public GameObject[] buttons;
    public static int switchCount = 0;
    public GameObject canv;
    public GameObject locked;
    public GameObject unlocked;
    public GameObject vaultDoor;
    // Start is called before the first frame update
    void Start()
    {
        SetGame();
    }

    private void Update()
    {
        if(switchCount == 0 )
        {
            Debug.Log("WIN!");
            locked.SetActive(false);
            unlocked.SetActive(true);
            StartCoroutine(waiter());
        }
    }

    void SetGame()
    {
        for(int i = 0; i< buttons.Length; i++)
        {
            int r = Random.Range(0, 2);
            if(r == 0)
            {
                buttons[i].SetActive(true);
                switchCount++;
            }
        }

    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(1);
        canv.SetActive(false);
        vaultDoor.SetActive(false);

    }


}
