using UnityEngine;
using System;

[Serializable]
public class DialogueNodeData
{
    public string Guid;

    public string Name;
    public string DialogueText;
    public float Delay;
    public string ConsoleCommand;
    
    public Vector2 Position;
}