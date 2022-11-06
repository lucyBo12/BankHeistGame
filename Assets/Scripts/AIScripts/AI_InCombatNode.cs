
public class AI_InCombatNode : AINode
{
    public AI_InCombatNode(string GUID = "") : base(GUID) { 
    
    }

    public override bool Conditional => true;
    public override bool BoolResult(AIBase npc) => GameManager.InCombat;
}
