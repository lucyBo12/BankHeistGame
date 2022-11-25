using UnityEngine;
using UnityEngine.AI;

/**
 * Centeral class for AI Behaviour. Pre-Assembled 'AITree' objects
 * are assigned to 'AIBase' component and computed from here. Other
 * aspects of ai logic are also defined here, including 'NavMeshAgent'
 * for pathfinding and 'AIGoal' for goal tracking respectivley. 
 * 
 * requires: NavMeshAgent
 * author: Joseph Denby 
 * email: jd744@kent.ac.uk
 */

[RequireComponent(typeof(NavMeshAgent))]
public class AIBase : MonoBehaviour
{
    public NavMeshAgent Agent { get; protected set; }
    public CharacterSheet Character;
    public AIGoal Goal { get; set; }
    public AITree behaviour;
    public bool aiEnabled = true;
    public AINode currentNode { get; private set; }
    [SerializeField] Animator animator;


    private void Start() {
        Goal = new AIGoal();
        Agent = GetComponent<NavMeshAgent>();
    }

    private void LateUpdate() {
        //Process tree logic
        HandleTree();

        if (Agent.destination.Equals(transform.position))
            return; //Are we going somewhere?

        //Animate movement
        var dir = transform.forward;
        animator.SetFloat("inputx", dir.x);
        animator.SetFloat("inputy", dir.y);
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
        Debug.Log(currentNode.GetType().Name);
    }

    //Variables for ai behaviour defined here
    //range is the minimum fear radius around an npc
    [System.Serializable]
    public struct CharacterSheet {
        [Range(0, 4)]public int fear;
        public bool staff;

        public CharacterSheet(int fear, bool staff) {
            this.fear = fear;
            this.staff = staff;          
        }

        
        
    }

    
}
