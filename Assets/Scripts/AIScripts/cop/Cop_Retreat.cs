using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Cop_Retreat : AINode
{
    public Cop_Retreat(string GUID = "") : base(GUID)
    {

    }

    public override void OnStart(AIBase npc)
    {
        var Room = GameManager.GetRoom(npc.gameObject);
        npc.Goal = new AIGoal(GameUtil.ClosestTransform(npc.transform, Room.exitPoint));
        npc.Agent.SetDestination(npc.Goal.TargetLocation);
    }

    public override bool Active(AIBase npc)
    {
        return Vector3.Distance(npc.transform.position, npc.Goal.TargetLocation) > 0.5f;
    }

    public override float Weight(AIBase npc)
    {
        var Room = GameManager.GetRoom(npc.gameObject);
        
        var dd = Vector3.Distance(GameUtil.ClosestTransform(npc.transform, Room.exitPoint).position, npc.transform.position); // ad =  distance to closest door
        var aa = Room.cops.Count; //aa num of allays^*
        var ae = Room.players.Count; //ae number of enemies^*
        var php = (100*ae)/Room.players.Sum(x => x.GetComponent<Character>().health); //php player health (percentage of all players health in room)^*
        var hp = npc.GetComponent<Character>().health; //hp cop health 


        return ((php - hp)+(ae-aa)-dd);

       

    }
}
