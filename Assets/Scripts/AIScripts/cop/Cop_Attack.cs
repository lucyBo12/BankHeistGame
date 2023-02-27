using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class Cop_Attack : AINode
{
    public override float Weight(AIBase npc)
    {
        var Room = GameManager.GetRoom(npc.gameObject);
        var ae = Room.players.Count; //ae number of enemies^*
        if (ae == 0)
        {
            return 0;
        }
        var aa = Room.cops.Count; //aa num of allays^*
        var php = (100 * ae) / Room.players.Sum(x => x.GetComponent<Character>().health); //php player health (percentage of all players health in room)^*
        var hp = npc.GetComponent<Character>().health; //hp cop health 
        var pd = Vector3.Distance(GameUtil.ClosestTransform(npc.transform, GameManager.Players.ToArray()).position, npc.transform.position);




        return (( pd) + aa + (hp));
    }

    /**
     * ((Ahp/pd + pa)+aa+(ph-hp))
     *
     * ahp = ally hp*
     * pd = player distance *
     * aa = number of players (exluding pd)
     * ph = targeted player hp*
     * hp = cop health*
     * 
     * */
}
