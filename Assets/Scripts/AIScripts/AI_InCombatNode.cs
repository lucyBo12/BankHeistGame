
public class AI_InCombatNode : AINode
{
    public override bool Conditional => true;
    public override bool BoolResult(AIBase npc) => GameManager.InCombat;
}
