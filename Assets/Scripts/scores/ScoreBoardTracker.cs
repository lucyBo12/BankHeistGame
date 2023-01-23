using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreBoardTracker : MonoBehaviour
{
    private ScoreData scoreData;
    void Awake()
    {
        var json = PlayerPrefs.GetString("scores", "{}");
        scoreData = JsonUtility.FromJson<ScoreData>(json);
    }

    

    public void AddScore(Scores playerScore)
    {
        scoreData.scores.Add(playerScore);
    }

    private void OnDestroy()
    {
        SaveScore();
    }


    //save to json file
    public void SaveScore()
    {
        var json = JsonUtility.ToJson(scoreData);
        Debug.Log(json);
        PlayerPrefs.SetString("scores", json);
    }

    public IEnumerable<Scores> GetHighScores()
    {
        return scoreData.scores.OrderByDescending(x => x.score);
    }
}
