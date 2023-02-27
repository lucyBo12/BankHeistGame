using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cop_Cover : AINode
{
    public override float Weight(AIBase npc)
    {
        var Room = GameManager.GetRoom(npc.gameObject);

        var dd = Vector3.Distance(GameUtil.ClosestTransform(npc.transform, Room.exitPoint).position, npc.transform.position); // ad =  distance to closest door
        var pd = Vector3.Distance(GameUtil.ClosestTransform(npc.transform, GameManager.Players.ToArray()).position, npc.transform.position); //pd =  distance to closest player
        var aa = Room.cops.Count; //aa num of allays^*
        var ae = Room.players.Count; //ae number of enemies^*
        var hp = npc.GetComponent<Character>().health; //hp cop health 
        


        return (( hp) + (ae - aa) - dd);



    }
    /**
     * ahp = ally hp
     * ehp = enemy hp
     * an = number of allies
     * cover = (0-4)
     * 
     * (hp + an + ahp / ehp + dp + ae) * cover
     * 
     */
}
