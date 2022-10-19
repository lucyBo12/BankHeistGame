
using UnityEngine;

[System.Serializable]
public class AICivillianMovement : AINode
{
    AINode[] AINode.Nodes { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void CallEnd()
    {
        
    }

    public void CallStart()
    {
    }

    public void CallUpdate()
    {
    }

    public AINode NextNode()
    {
        return null;
    }

    public float Weight()
    {
        return 1;
    }

    void AINode.CallEnd()
    {
        throw new System.NotImplementedException();
    }

    void AINode.CallStart()
    {
        throw new System.NotImplementedException();
    }

    void AINode.CallUpdate()
    {
        throw new System.NotImplementedException();
    }

    AINode AINode.NextNode()
    {
        throw new System.NotImplementedException();
    }

    float AINode.Weight()
    {
        throw new System.NotImplementedException();
    }
}
