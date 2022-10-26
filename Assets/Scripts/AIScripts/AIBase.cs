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
    public AINode currentNode { get; private set; }


    private void LateUpdate()
    {
        HandleTree();
    }

    /**
     * ==========================================================================
     *  Update and handle tree logic and node switching per frame. Requires both
     *  an established AITree(behavior) and 'aiEnabled' to be true expected
     *  processing and output.
     * ==========================================================================
     */
    private void HandleTree() {
        if (!behaviour || !aiEnabled) 
            return; //Are we able to process?

        //Ensure we have node to process with
        if (currentNode == null)
            currentNode = behaviour.StartNode();

        //Node isn't finished
        if (currentNode.Active(this)) {
            currentNode.OnUpdate(this);
            return;
        }

        //Node is finished so we establish next node
        currentNode.OnEnd(this);
        AINode[] nextNodes = behaviour.Next(currentNode, this);
        currentNode = nextNodes.Length == 0 ? behaviour.StartNode() : nextNodes[0];
        currentNode.OnStart(this);
    }

}
