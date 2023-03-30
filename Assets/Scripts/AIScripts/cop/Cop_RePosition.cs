using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cop_RePosition : AINode
{
    public Cop_RePosition(string GUID = "") : base(GUID)
    {
        
    }

    public override void OnStart(AIBase npc)
    {
        npc.Agent.SetDestination(npc.Target.transform.position);
        Debug.LogWarning($"RePosition: Going to [{npc.Target.name}]");
    }


    public override float Weight(AIBase npc)
    {
        if (!npc.TargetInRange()) return 1f;

        var dist = Mathf.Clamp(10 - (Vector3.Distance(npc.transform.position, npc.Target.transform.position)), 0f, 10f);

        return (dist / 10f);
    }

    public override void OnEnd(AIBase npc)
    {
        Debug.LogWarning("RePosition: End");
        npc.Agent.isStopped = true;
        npc.Goal = new AIGoal();
    }

    public override bool Active(AIBase npc) => npc.Agent.remainingDistance > 2f;


}
