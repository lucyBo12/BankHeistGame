using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cop_Cover : AINode
{
    public Cop_Cover(string GUID = "") : base(GUID)
    {

    }
    public override void OnStart(AIBase npc)
    {
        var Room = GameManager.GetRoom(npc.gameObject);
        npc.Goal = new AIGoal(GameUtil.ClosestTransform(npc.transform, Room.coverPoints));
        npc.Agent.SetDestination(npc.Goal.TargetLocation);
        //pick best cover point, check cover point is empty
    }
    public override float Weight(AIBase npc)
    {
        var Room = GameManager.GetRoom(npc.gameObject);

        var cpd = Vector3.Distance(GameUtil.ClosestTransform(npc.transform, Room.coverPoints).position, npc.transform.position); // cpd = distance to cover point
        var pd = Vector3.Distance(GameUtil.ClosestTransform(npc.transform, GameManager.Players.ToArray()).position, npc.transform.position); //pd =  distance to closest player
        var aa = Room.cops.Count; //aa num of allays^*
        var ae = Room.players.Count; //ae number of enemies^*
        var hp = npc.GetComponent<Character>().health; //hp cop health 
        var php = (100 * ae) / Room.players.Sum(x => x.GetComponent<Character>().health); //php player health (percentage of all players health in room)^*



        return ((float)((ae + pd + php) / (aa + (hp) + cpd)));
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
