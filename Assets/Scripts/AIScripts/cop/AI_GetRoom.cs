using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_GetRoom : AINode
{
    public AI_GetRoom(string GUID = "") : base(GUID)
    {

    }
    public override float Weight(AIBase npc)
    {
        return 1f;
    }

    public override void OnStart(AIBase npc)
    {
        var closestPlayer = GameUtil.ClosestTransform(npc.transform, GameManager.Players.ToArray());
        var Room = GameManager.GetRoom(closestPlayer.gameObject);
        npc.Goal = new AIGoal(Room.transform);
        npc.Agent.destination = npc.Goal.TargetLocation;

    }

    public override bool Active(AIBase npc)
    {
        var room= GameManager.GetRoom(npc.gameObject);  
        if (room == null) return false;
        return npc.Agent.remainingDistance > 0.5f && room.players.Count == 0;
    }

    public override void OnEnd(AIBase npc)
    {
        npc.Agent.isStopped = true;
        npc.Goal = new AIGoal(); 
    }
}
