using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cop_GetTarget : AINode
{
    public override float Weight(AIBase npc)
    {
        var Room = GameManager.GetRoom(npc.gameObject);

        return Room.players.Count > 0 ? 1 : 0 ;
    }

    public override void OnStart(AIBase npc)
    {
        var Room = GameManager.GetRoom(npc.gameObject);
        if(Room.players.Count > 0) {
            var target = Room.players[0].GetComponent<Character>();
            Room.players.ForEach(x => { 
                var character = x.GetComponent<Character>();
                if (character.health < target.health) {
                    target = character;
                }
            
            });
            npc.Target = target.gameObject;
        }
    }

}
