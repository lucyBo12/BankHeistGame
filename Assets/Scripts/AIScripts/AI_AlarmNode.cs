using UnityEngine;

public class AI_AlarmNode : AINode
{
    public AI_AlarmNode(string GUID = "") : base(GUID) { 
    
    }

    public override void OnStart(AIBase npc)
    {
        npc.Goal = new AIGoal(GameUtil.ClosestTransform(npc.transform, GameManager.Alarms.ToArray()));
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
        var pd = Vector3.Distance(GameUtil.ClosestTransform(npc.transform , GameManager.Players.ToArray()).position, npc.transform.position); //pd =  distance to closest player
        var ad = Vector3.Distance(GameUtil.ClosestTransform(npc.transform, GameManager.Alarms.ToArray()).position, npc.transform.position); // ad =  distance to closest alarm 
        var f = npc.Character.fear;
        var t = npc.Character.staff;

        return (pd - (f*10) +(t?1:0)) / (ad + (f * 10));
    }
}
