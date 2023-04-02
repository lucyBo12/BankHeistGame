using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultGameButtons : MonoBehaviour
{
    

    VaultUnlocker vaultGame;
    public void OnPress()
    {
        this.gameObject.SetActive(false);
        VaultUnlocker.switchCount--;
    }

    
}
