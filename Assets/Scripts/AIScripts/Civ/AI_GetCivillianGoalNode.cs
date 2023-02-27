using UnityEngine;

public class AI_GetCivillianGoalNode : AINode {

    public override bool Conditional => base.Conditional; //false

    public AI_GetCivillianGoalNode(string GUID = "") : base(GUID) { 
    
    }   

    public override void OnStart(AIBase npc) {
        var Room = GameManager.GetRoom(npc.gameObject);
        NPCGoal goal = Room.goals[Random.Range(0, Room.goals.Length)];
        goal.AddToQueue(npc);
        Debug.Log($"Goal: {goal.name} [{npc.Goal.TargetLocation}]");
    }

    public override void OnEnd(AIBase npc) {
        npc.Goal = new AIGoal();
    }

    public override bool Active(AIBase npc) => !GameManager.InCombat;
}
