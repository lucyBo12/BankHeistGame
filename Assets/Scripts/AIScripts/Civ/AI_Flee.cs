using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Flee : AINode
{
    public AI_Flee(string GUID = "") : base(GUID)
    {

    }

    public override void OnStart(AIBase npc)
    {
        var Room = GameManager.GetRoom(npc.gameObject);
        npc.Goal = new AIGoal(GameUtil.ClosestTransform(npc.transform, Room.exitPoint));
        npc.Agent.SetDestination(npc.Goal.TargetLocation);
    }

    public override void OnEnd(AIBase npc)
    {
        npc.Agent.isStopped = true;
        npc.Agent.destination = npc.transform.position;
    }

    public override bool Active(AIBase npc) => Weight(npc) > npc.Character.fear;

    public override float Weight(AIBase npc)
    {
        var Room = GameManager.GetRoom(npc.gameObject);
        var pd = Vector3.Distance(GameUtil.ClosestTransform(npc.transform, GameManager.Players.ToArray()).position, npc.transform.position);// pd =  distance to closest player
        var dd = Vector3.Distance(GameUtil.ClosestTransform(npc.transform, Room.exitPoint).position, npc.transform.position); // ad =  distance to closest door
        var f = npc.Character.fear;

        return (pd - (f * 10)) / (dd + (f * 10));
    }
}

