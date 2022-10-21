using UnityEngine.AI;
using UnityEngine;

public class DebugAgentControl : MonoBehaviour
{
    [SerializeField]private Transform target;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;

    private bool working;

    [ContextMenu("Start Debug")]
    private void StartDebug() {
        working = true;
        agent.destination = target.position;
    }

    private void Update()
    {
        if (!working) return;
        Vector3 dir = agent.path.corners[0];
        animator.SetFloat("inputx", dir.x);
        animator.SetFloat("inputy", dir.z);
        Debug.Log($"Dir: {dir}");
    }

}
