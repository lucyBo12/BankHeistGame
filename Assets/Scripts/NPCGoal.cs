using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCGoal : MonoBehaviour
{
    [SerializeField] private Queue<AIBase> queue = new Queue<AIBase>();
    [SerializeField] private Vector3 queueDirection = new Vector3(0, 0, -1);
    [SerializeField] private float interactionTime = 1f;
    [SerializeField, Range(0, 1f)] private float positionOffset = 0.2f;
    [SerializeField] private string animationTrigger = "Interact";


    private void OnEnable() => StartCoroutine(ManageQueue());

    public void AddToQueue(AIBase civ) { 
        queue.Enqueue(civ);
        Vector3 destination = new Vector3(
            transform.position.x + (queueDirection.x * (queue.Count > 1 ? queue.Count : 1) + Random.Range(-positionOffset, positionOffset)), //X
            transform.position.y, //Y
            transform.position.z + (queueDirection.z * (queue.Count > 1 ? queue.Count : 1) + Random.Range(-positionOffset, positionOffset))  //Z                   
            );

        civ.Goal = new AIGoal(destination);
        civ.Agent.SetDestination(destination);
    }

    //This coroutine manages the queue for interactions over time.
    private IEnumerator ManageQueue() {
        //When not in combat, we keep processing the queue
        while (!GameManager.InCombat) {
            yield return new WaitForSeconds(3f);
            while (queue.Count > 0) {
                //Get NPC at front of queue
                AIBase npc = queue.Dequeue();

                //Play animation
                npc.Animator.Play(animationTrigger);

                //Wait for interaction time
                yield return new WaitForSeconds(interactionTime);

                //Release NPC
                npc.Goal.Release();
            }
        }

        //If we get here, it's because we are not in combat, so we must release all that are in queue.
        queue.ToList().ForEach(x => x.Goal.Release());
        queue.Clear();
    }
}
