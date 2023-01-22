using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Cower : AINode
{
    public AI_Cower(string GUID = "") : base(GUID)
    {

    }

    public override bool Active(AIBase npc) => AIUtil.Cower(npc);


    public override float Weight(AIBase npc)
    {
        return Active(npc) ? 1 : 0;  
    }
}
