using UnityEditor.Experimental.GraphView;

public class AINode : Node
{
    public string GUID;
    public bool EntryPoint = false;
    public virtual bool Conditional => false;

    public AINode(string GUID = "") { 
        this.GUID = GUID;
    }

    public virtual void OnStart(AIBase npc) { 
    
    }

    public virtual void OnUpdate(AIBase npc) { 
        
    }

    public virtual void OnEnd(AIBase npc) { 
        
    }

    public virtual float Weight(AIBase npc) => 0;

    public virtual bool Active(AIBase npc) => false;

    public virtual bool BoolResult(AIBase npc) => false;

    public virtual string ToString(AIBase npc) {
        return $"{GetType().Name}: [{Active(npc)}] - {GUID}";
    }
}
