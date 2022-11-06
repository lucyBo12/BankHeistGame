using UnityEngine;
public class AI_HasGoalNode : AINode
{
    public override bool Conditional => true;

    public AI_HasGoalNode(string GUID = "") : base(GUID) {
        
    }

    public override bool BoolResult(AIBase npc)
    {
        return npc.Goal.HasGoal;
    }



}
