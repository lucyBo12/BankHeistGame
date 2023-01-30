using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI scoreText1, scoreText2, scoreText3;
    [SerializeField] public TextMeshProUGUI nameText1, nameText2, nameText3;
    public ScoreBoardTracker scoreTracker;
    

    // Start is called before the first frame update
    void Start()
    {
        //just test values
        //scores will actually be added using the endGame script that will fetch the players score and name from the CharacterClass
        scoreTracker = GetComponent<ScoreBoardTracker>();
        scoreTracker.AddScore(new Scores(name: "Dave", score: 100));
        scoreTracker.AddScore(new Scores(name: "Debbie", score: 2000));
        scoreTracker.AddScore(new Scores(name: "Barry", score: 0));
        //scoreTracker.AddScore(new Scores(name: "hmm", score: 1500));

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
}
