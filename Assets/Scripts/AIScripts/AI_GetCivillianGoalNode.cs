using UnityEngine;

public class AI_GetCivillianGoalNode : AINode {

    public override bool Conditional => base.Conditional; //false

    public AI_GetCivillianGoalNode(string GUID = "") : base(GUID) { 
    
    }   

    public override void OnStart(AIBase npc) {
        NPCGoal goal = GameManager.Goals[Random.Range(0, GameManager.Goals.Count)];
        goal.AddToQueue(npc);
        Debug.Log($"Goal: {goal.name} [{npc.Goal.TargetLocation}]");
    }

    public override void OnEnd(AIBase npc) {
        npc.Goal = new AIGoal();
    }

    public override bool Active(AIBase npc) => !GameManager.InCombat;
}
