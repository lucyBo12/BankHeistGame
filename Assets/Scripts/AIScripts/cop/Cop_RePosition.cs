using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cop_RePosition : AINode
{
    public Cop_RePosition(string GUID = "") : base(GUID)
    {
        
    }

    public override void OnStart(AIBase npc)
    {
        npc.Agent.SetDestination(npc.Target.transform.position);
    }

    public override void OnUpdate(AIBase npc)
    {
        if (Vector3.Distance(npc.transform.position, npc.Target.transform.position) < 2) { 
            npc.Agent.isStopped = true;
        }
    }

    public override float Weight(AIBase npc)
    {
        return npc.TargetInRange() ? (Vector3.Distance(npc.transform.position, npc.Target.transform.position)) / 10 : 1;
    }

    public override void OnEnd(AIBase npc)
    {
        npc.Goal = new AIGoal();
        var Room = GameManager.GetRoom(npc.gameObject);
        var Room2 = GameManager.GetRoom(npc.Target.gameObject);
        if (Room != Room2 && Room.players.Count > 0)
        {
            npc.Target = null;
        }
    }

    public override bool Active(AIBase npc) => npc.Agent.remainingDistance > 0.5f;


}
