using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableDoor : Interactable
{
    public AudioSource doorSound;
    public GameObject vaultMinigame;

    public override void Interact(Transform user)
    {
        //user.GetComponent<Character>();
        //doorSound.Play();
        //SceneManager.LoadScene("TestRoom007");
        //vaultMinigame.SetActive(true);
        base.Interact(user);
    }
}
