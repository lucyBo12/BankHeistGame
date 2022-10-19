using UnityEngine;

public interface AINode 
{
    public Event OnTaskComplete { get; set; }
    public void CallStart();
    public void CallUpdate();
    public void CallEnd();
    public float Weight();
    public AINode NextNode();
    public AINode[] Nodes { get; set; }
}
