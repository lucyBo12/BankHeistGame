using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Cower : AINode
{
    // Start is called before the first frame update
    public override bool Active(AIBase npc)
    {

        //fear value as a int (minimum 0 - max 10)
        int x = (int)(npc.Character.fear * 10);

        //y is the new cower range
        int y = (int)(5 * (MathF.Pow(1.1f, x)));  

        var T = GameUtil.ClosestPlayer(npc.transform);

        //distance of player from npc
        float Distance = Vector3.Distance(T.position, npc.transform.position);

        Debug.Log(y); 
        return Distance < y;
    }

    public override float Weight(AIBase npc)
    {
        return Active(npc) ? 1 : 0;  
    }
}
