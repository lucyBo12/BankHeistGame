using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cop_Retreat : AINode
{
  
    public override float Weight(AIBase npc)
    {
        var pd = Vector3.Distance(GameUtil.ClosestTransform(npc.transform, GameManager.Players.ToArray()).position, npc.transform.position);// pd =  distance to closest player
        var dd = Vector3.Distance(GameUtil.ClosestTransform(npc.transform, GameManager.ExitPoint.ToArray()).position, npc.transform.position); // ad =  distance to closest door
        var f = npc.Character.fear;

        return (pd - (f * 10)) / (dd + (f * 10));

        //((php - %hp)+(ae-aa)-ad)
        /**
         * aa num of allays
         * hp cop health 
         * ae number of enemies
         * php player health
         * ad  exit distance 
         */ 
    }
}
