using System;
using System.Collections.Generic;

[Serializable]
public class ScoreData
{
    public List<Scores> scores;

    public ScoreData()
    {
        scores = new List<Scores>();
    }
}