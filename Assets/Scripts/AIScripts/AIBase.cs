using System.Linq;
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
    public CharacterSheet profile;
    public AIGoal Goal { get; set; }
    public Character Character { get; private set;} 
    public GameObject Target { get; set; }
    public AITree behaviour;
    public bool aiEnabled = true;
    public AINode currentNode { get; private set; }
    public Animator Animator { get; private set; }


    private void Start() {
        Goal = new AIGoal();
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
        Character = GetComponent<Character>();
    }

    private void LateUpdate() {
        //Process tree logic
        HandleTree();

        if (Agent.destination.Equals(transform.position))
            return; //Are we going somewhere?

        //Animate movement
        var dir = transform.forward;
        Animator.SetFloat("inputx", dir.x);
        Animator.SetFloat("inputy", dir.y);
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
        currentNode = nextNodes.Length == 0 ? behaviour.StartNode() : nextNodes.ToList().OrderByDescending(x => x.Weight(this)).First();
        currentNode.OnStart(this);
        Debug.Log($"{currentNode.GetType().Name} | W: {currentNode.Weight(this)}");
    }

    public bool TargetInRange() {
        if (Target is null) {
            return false;
        }

        if (Physics.Raycast(transform.position + Vector3.up, -((transform.position - Target.transform.position).normalized * 100) + Vector3.up, out var hit)) {
            return hit.transform.CompareTag("Player");
        }

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        if (Target is null) return;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position + Vector3.up, -((transform.position - Target.transform.position).normalized * 100) + Vector3.up);
    }

    //Variables for ai behaviour defined here
    //range is the minimum fear radius around an npc
    [System.Serializable]
    public struct CharacterSheet {
        [Range(0, 1)]public float fear;
        public bool staff;
        [Range(0, 1)] public float charge;

        public CharacterSheet(int fear, bool staff,int charge) {
            this.fear = fear;
            this.staff = staff;
            this.charge = charge;
        }

    }

    
}
