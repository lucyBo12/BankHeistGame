using UnityEditor.Experimental.GraphView;

public class AINode : Node
{
    public string GUID;
    public bool EntryPoint = false;
    public virtual bool Conditional => false;

    public AINode(string GUID = "") { 
        this.GUID = GUID;
    }

    /**
     * Calls on node activation.
     * 
     * @param AIBase
     */
    public virtual void OnStart(AIBase npc) {
    }

    /**
     * Calls for every rendered frame node is active.
     * 
     * @param AIBase
     */
    public virtual void OnUpdate(AIBase npc) { 
        
    }

    /*
     * Called on the last frame before leaving node.
     * 
     * @param AIBase
     */
    public virtual void OnEnd(AIBase npc) { 
        
    }

    /**
     * Returns confidence of given nodes use.
     * 
     * @param AIBase
     */
    public virtual float Weight(AIBase npc) => 0;

    /**
     * Returns if node should remain has chosen node for given frame.
     * 
     * @param AIBase
     */
    public virtual bool Active(AIBase npc) => false;


    public virtual bool BoolResult(AIBase npc) => false;

    public virtual string ToString(AIBase npc) {
        return $"{GetType().Name}: [{Active(npc)}] - {GUID}";
    }
}
