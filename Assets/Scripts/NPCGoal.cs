using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGoal : MonoBehaviour
{
    [SerializeField] private Queue<AIBase> queue = new Queue<AIBase>();
    [SerializeField] private Vector3 queueDirection = new Vector3(0, 0, -1);
    [SerializeField, Range(0, 1f)] private float positionOffset = 0.2f;
    [SerializeField] private string animationTrigger = "Interact";

    void Start() => GameManager.Goals.Add(this);

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
}
