using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIBase : MonoBehaviour
{
    public NavMeshAgent Agent { get; protected set; }
    public Transform Target { get; protected set; }
    public AIGoal Goal { get; set; }
    public AITree behaviour;
    public bool aiEnabled = true;


    private void LateUpdate()
    {
           
    }


}
