using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AIUtil 
{
    public static bool Cower(AIBase npc)
    {
        //fear value as a int (minimum 0 - max 10)
        int x = (int)(npc.profile.fear * 10);

        //y is the new cower range
        int y = (int)(5 * (MathF.Pow(1.1f, x)));

        var T = GameUtil.ClosestTransform(npc.transform, GameManager.Players.ToArray());

        //distance of player from npc
        float Distance = Vector3.Distance(T.position, npc.transform.position);

        return Distance < y;
    }
}
