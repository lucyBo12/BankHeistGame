using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;


[RequireComponent(typeof(Slider))]
public class SliderManager : MonoBehaviour
{
    //fields
    [SerializeField] private string sliderName;
    [SerializeField] private Text sliderVal;
    Slider volumeSlider
    {
        get { return GetComponent<Slider>(); }
    }

    private void Start()
    {
        ResetValues();
        UpdateMixer(volumeSlider.value);
        volumeSlider.onValueChanged.AddListener(delegate
        {
            UpdateMixer(volumeSlider.value);
        });
    }

    public void UpdateMixer(float volume)
    {
        //applies log to volume so it can still be heard below 50%
        //this is bc slider is linear and audio mixer is logarithmic
        //mixer.SetFloat(sliderName, Mathf.Log(volume)*20);
        sliderVal.text = Mathf.Round(volume * 100) + "%";
        if(Settings.userProfile)
        {
            Settings.userProfile.SetAudio(sliderName, volume);
        }
    }

    public void ResetValues()
    {
        if(Settings.userProfile)
        {
            float vol = Settings.userProfile.GetAudioLevel(sliderName);
            UpdateMixer(vol);
            volumeSlider.value = vol;
        }
    }


}
