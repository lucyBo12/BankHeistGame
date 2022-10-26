using UnityEngine;

public class AI_GetCivillianGoalNode : AINode {

    public AI_GetCivillianGoalNode(string GUID = "") : base(GUID) { 
    
    }   

    public override void OnStart(AIBase npc)
    {
        Debug.Log($"{GetType().Name}: Start \n[{Active(npc)}] - {GUID}");
    }

    public override void OnUpdate(AIBase npc)
    {
        Debug.Log($"{GetType().Name}: Update \n[{Active(npc)}] - {GUID}");
    }

    public override void OnEnd(AIBase npc)
    {
        Debug.Log($"{GetType().Name}: End \n[{Active(npc)}] - {GUID}");
    }

}
