using UnityEngine.AI;
using UnityEngine;

public class DebugAgentControl : MonoBehaviour
{
    [SerializeField]private Transform target;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;

    [ContextMenu("Start Debug")]
    private void StartDebug() {
        agent.destination = target.position;
        Vector3 dir = agent.path.corners[0];
        animator.SetFloat("inputx", dir.x);
        animator.SetFloat("inputy", dir.y);
        Debug.Log($"Dir: {dir}"); 
    }
}
