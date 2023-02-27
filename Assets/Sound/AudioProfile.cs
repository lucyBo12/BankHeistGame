using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[System.Serializable] public class Audio
{
    public float volume = 1f;
    public string name;
    public float tempVol = 1f;
}

//user profile
public class Settings
{
    public static AudioProfile userProfile;
}
[CreateAssetMenu(menuName = "Create Audio Profile")]
public class AudioProfile : ScriptableObject
{
    public AudioMixer mixer;
    public Audio[] volumesList;

    //return the volume level for the specified slider
    public float GetAudioLevel(string name)
    {
        float volume = 1f;
        for(int i=0; i<volumesList.Length; i++)
        {
            if(volumesList[i].name == name)
            {
                volumesList[i].tempVol = volumesList[i].volume;
                if(mixer)
                {
                    mixer.SetFloat(volumesList[i].name, Mathf.Log(volumesList[i].volume) * 20f);

                }
                volume = volumesList[i].volume;
                break;
            }
        }
        return volume;
    }

    //return volume level for all sliders
    public void GetAudioLevel()
    {
        for (int i = 0; i < volumesList.Length;i++)
        {
            volumesList[i].tempVol = volumesList[i].volume;
            mixer.SetFloat(volumesList[i].name, Mathf.Log(volumesList[i].volume) * 20f);
        }
    }

    //set temporary values
    public void SetAudio(string name, float volume)
    {
        for(int i = 0; i<volumesList.Length; i++)
        {
            if(volumesList[i].name == name)
            {
                mixer.SetFloat(volumesList[i].name, Mathf.Log(volume) * 20f);
                volumesList[i].tempVol = volume;
                break;
            }
        }
    }

    //saves audio levels
    public void SaveChanges()
    {
        float volume = 0f;
        for (int i = 0; i < volumesList.Length; i++)
        {
            volume = volumesList[i].tempVol;
            mixer.SetFloat(volumesList[i].name, Mathf.Log(volume) * 20f);
            volumesList[i].volume = volume;
            
        }
    }
}
