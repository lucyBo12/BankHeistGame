using UnityEditor.Experimental.GraphView;

public class AINode : Node
{
    public string GUID;
    public bool EntryPoint = false;

    public virtual void OnStart(AIBase npc) { 
    
    }

    public virtual void OnUpdate(AIBase npc) { 
        
    }

    public virtual void OnEnd(AIBase npc) { 
        
    }

    public virtual bool Active(AIBase npc) {
        return true;
    }
}
