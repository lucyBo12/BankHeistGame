using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class OptionsButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI text;

    public GameObject optionPanel;
    public GameObject soundPanel;

    public AudioSource hover;
    public AudioSource click;

    AudioController ac;


    //OnHover text color and size will change
    public void OnPointerEnter(PointerEventData eventData)
    {
        text.GetComponent<TextMeshProUGUI>().fontSize = 80;
        text.GetComponent<TextMeshProUGUI>().color = new Color32(79, 103, 255, 255);
        hover.Play();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        text.GetComponent<TextMeshProUGUI>().fontSize = 60;
        text.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);

    }

    public void ContinueButton()
    {
        optionPanel.SetActive(false);
        click.Play();
        ac.isMenu = false;
    }

    public void AudioButton()
    {
        soundPanel.SetActive(true);
        optionPanel.SetActive(false);
        click.Play();
        //so when player goes back out of audio menu the button text is not still enlarged and changed colour
        text.GetComponent<TextMeshProUGUI>().fontSize = 60;
        text.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
    }

}
