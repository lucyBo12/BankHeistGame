using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenuButton : MonoBehaviour
{
    public GameObject settings;
    public void OpenMenu()
    {
        settings.SetActive(true);
    }
}
