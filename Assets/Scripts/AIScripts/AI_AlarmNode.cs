using UnityEngine;

public class AI_AlarmNode : AINode
{
    public AI_AlarmNode(string GUID = "") : base(GUID) { 
    
    }

    public override void OnStart(AIBase npc)
    {
        npc.Goal = new AIGoal(GameUtil.ClosestAlarm(npc.transform));
        npc.Agent.SetDestination(npc.Goal.TargetLocation);
    }

    public override void OnUpdate(AIBase npc)
    {

    }

    public override void OnEnd(AIBase npc)
    {
        npc.Agent.isStopped = true;
        npc.Agent.destination = npc.transform.position;
    }

    public override bool Active(AIBase npc) {
       return !(Vector3.Distance(npc.transform.position, npc.Goal.Target.position) < 1) || 
            Weight(npc) > npc.Character.fear;
    }
    
    public override float Weight(AIBase npc)
    {
        var pd = Vector3.Distance(GameUtil.ClosestPlayer(npc.transform).position, npc.transform.position);
        var ad = Vector3.Distance(GameUtil.ClosestAlarm(npc.transform).position, npc.transform.position);
        var f = npc.Character.fear;
        var t = npc.Character.staff;

        return (pd - (f*10) +(t?1:0)) / (ad + (f * 10));
    }
}
