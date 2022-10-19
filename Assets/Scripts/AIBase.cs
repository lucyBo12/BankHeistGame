using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIBase : MonoBehaviour
{
    public AINode CurrentNode { get; protected set; }
    public AINode[] Nodes { get; protected set; }
    public NavMeshAgent Agent { get; protected set; }

    public delegate void OnTaskCompleted();
    public event OnTaskCompleted TaskCompletedEvent;



    protected void Update() {
    }

    private void Start()
    {
    }

}
