using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableExitDoor : Interactable
{
    public GameObject highscore;
    GameObject player;
    public override void Interact(Transform user)
    {
        highscore.SetActive(true);
        user.GetComponent<Character>().GetComponent<Weapon>().fireSound.enabled = false;
        GameManager.CheckGameState();
        base.Interact(user);
    }
}
