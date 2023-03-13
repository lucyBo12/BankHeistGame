using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioProfile profile;
    [SerializeField] List<SliderManager> volumeSliders = new List<SliderManager>();

    public GameObject soundPanel;
    public GameObject optionsPanel;
    public AudioSource click;

    public GameObject player;
    public bool isMenu = false;


    public void SetProfile(AudioProfile profile)
    {
        Settings.userProfile = profile;
        EditorUtility.SetDirty(profile);
    }

    private void Awake()
    {
        if(profile != null)
        {
            SetProfile(profile);
        }
    }

    
    public void Start()
    {
        SetProfile(profile);
        ResetButton();

        //GameManager.Input.Player.OpenMenu.performed += evt => OpenMenu();
    }

    public void OpenMenu()
    {
        //Debug.Log("Working");
        isMenu = true;
        optionsPanel.SetActive(true);
        player.GetComponent<CharacterLocomotion>().PlayerActions.Disable();
    }
    public void CloseMenu()
    {
        isMenu = false;
        optionsPanel.SetActive(false);
        player.GetComponent<CharacterLocomotion>().PlayerActions.Enable();
    }


    public void Update()
    {
        if((isMenu == false) && GameManager.Input.Player.OpenMenu.IsPressed())
        {
            OpenMenu();
        }
        if ((isMenu == true) && GameManager.Input.Player.OpenMenu.triggered)
        {
            CloseMenu();
        }

    }

    //save changes on button press
    public void SaveButton()
    {
        if (Settings.userProfile)
        {
            Settings.userProfile.SaveChanges();
            
        }
        click.Play();
    }

    //reset values on button press
    public void ResetButton()
    {
        if (Settings.userProfile)
        {
            Settings.userProfile.GetAudioLevel();
        }
        for(int i = 0; i<volumeSliders.Count;i++)
        {
            volumeSliders[i].ResetValues();
        }
        click.Play();
    }

    public void BackButton()
    {
        soundPanel.SetActive(false);
        optionsPanel.SetActive(true);
        click.Play();

    }

}
