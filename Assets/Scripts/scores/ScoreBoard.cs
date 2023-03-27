using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI scoreText1, scoreText2, scoreText3;
    [SerializeField] public TextMeshProUGUI nameText1, nameText2, nameText3;
    public ScoreBoardTracker scoreTracker;
    public GameObject[] players;

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        scoreTracker = GetComponent<ScoreBoardTracker>();
        for (int i = 0; i<players.Length; i++)
        {
            scoreTracker.AddScore(new Scores(name: players[i].GetComponent<Character>().playerName, score: players[i].GetComponent<Character>().money));
        }
        //just test values
        //scores will actually be added using the endGame script that will fetch the players score and name from the CharacterClass
        //scoreTracker.AddScore(new Scores(name: "Debbie", score: 2000));

        SetScoreBoard();
        
    }


    public void SetScoreBoard()
    {
        var scores = scoreTracker.GetHighScores().ToArray();
        Debug.Log(scores);
        nameText1.SetText(scores[0].playerName);
        scoreText1.SetText(scores[0].score.ToString());
        nameText2.SetText(scores[1].playerName);
        scoreText2.SetText(scores[1].score.ToString());
        nameText3.SetText(scores[2].playerName);
        scoreText3.SetText(scores[2].score.ToString());
    }

    public void FinishButton()
    {
        for(int i = 0; i<players.Length; i++)
        {
            players[i].GetComponent<Character>().money = 0;
        }
        
        //player.GetComponent<CharacterLocomotion>().enabled = true;
        //SceneManager.LoadScene("TestRoom005");
        Application.Quit();
        this.gameObject.SetActive(false);
    }

    public void PlayAgainButton()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<Character>().money = 0;
            players[i].GetComponent<CharacterLocomotion>().enabled = true;
        }
        GameManager.StartNewGame();
        SceneManager.LoadScene("TestRoom005");
    }
}

