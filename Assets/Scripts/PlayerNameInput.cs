using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerNameInput : MonoBehaviour
{
    public TMP_InputField nameInput;
    public GameObject startMenu;
    public TextMeshProUGUI warningText;
    public GameObject healthUI;
    

    public static string userInput;
    //public string userInput;
    // Start is called before the first frame update

    public void Start()
    {
        warningText.SetText("");
    }

    public void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            this.gameObject.SetActive(false);
        }
    }

    public void StartButton()
    {
        userInput = nameInput.text;
        if (userInput == "")
        {
            //show error message
            Debug.Log("NO!");
            warningText.SetText("NO!");
        }
        else
        {
            startMenu.SetActive(false);
            LobbyManager.CreateOrJoinLobby();
            healthUI.SetActive(true);

        }
    }


}
