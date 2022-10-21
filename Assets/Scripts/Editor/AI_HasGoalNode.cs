using UnityEngine;
public class AI_HasGoalNode : AINode
{

    public override bool Active(AIBase npc) {
        return npc.Goal.Target || 
            npc.Goal.TargetLocation != Vector3.zero;
    }

}
