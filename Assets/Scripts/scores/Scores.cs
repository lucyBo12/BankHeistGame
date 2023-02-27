using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Scores
{
    public string playerName;
    public int score;

    public Scores(string name, int score)
    {
        this.playerName = name;
        this.score = score;
    }
}
