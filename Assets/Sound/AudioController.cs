using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioProfile profile;
    [SerializeField] List<SliderManager> volumeSliders = new List<SliderManager>();


    public void SetProfile(AudioProfile profile)
    {
        Settings.userProfile = profile;
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

    //save changes on button press
    public void SaveButton()
    {
        if (Settings.userProfile)
        {
            Settings.userProfile.SaveChanges();
        }
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
    }

}
