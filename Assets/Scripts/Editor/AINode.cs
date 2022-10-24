using UnityEditor.Experimental.GraphView;

public class AINode : Node
{
    public string GUID;
    public bool EntryPoint = false;
    public object nodeType;

    public virtual void OnStart(AIBase npc) { 
    
    }

    public virtual void OnUpdate(AIBase npc) { 
        
    }

    public virtual void OnEnd(AIBase npc) { 
        
    }

    public virtual float Weight(AIBase npc) => 0;

    public virtual bool Active(AIBase npc) => true;
}
