using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIBase : MonoBehaviour
{
    public NavMeshAgent Agent { get; protected set; }
    public Transform Target { get; protected set; }

}
