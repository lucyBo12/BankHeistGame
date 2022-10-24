using UnityEngine;

public class AI_GetCivillianGoalNode : AINode {

    public override bool Active(AIBase npc) => base.Active(npc);

    public override float Weight(AIBase npc) => npc.Goal.ActiveGoal ? 0 : 1;

    public override void OnStart(AIBase npc)
    {
        npc.Goal = new AIGoal(new Vector3(Random.Range(-5, 5), 0.33f, Random.Range(-5, 5)));
    }

}
