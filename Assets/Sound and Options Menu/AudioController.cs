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
        if (Settings.userProfile)
        {
            Settings.userProfile.GetAudioLevel();
        }
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.O))
        {
            optionsPanel.SetActive(true);
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
