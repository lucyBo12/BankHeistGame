using UnityEngine;

public class AI_GetCivillianGoalNode : AINode {

    public override bool Conditional => base.Conditional; //false

    public AI_GetCivillianGoalNode(string GUID = "") : base(GUID) { 
    
    }   

    public override void OnStart(AIBase npc) {
        npc.Goal = new AIGoal(new Vector3(Random.Range(-5, 5), 0.32f, Random.Range(-5, 5)));
        npc.Agent.SetDestination(npc.Goal.TargetLocation);
    }

    public override void OnEnd(AIBase npc) {
        npc.Goal = new AIGoal();
    }

    public override bool Active(AIBase npc)
    {
        return Vector3.Distance(npc.transform.position, npc.Goal.TargetLocation) > 1;
    }
}
