using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cop_HasTarget : AINode
{
    public override bool Conditional => true;

    public Cop_HasTarget(string GUID = "") : base(GUID)
    {

    }

    public override bool BoolResult(AIBase npc)
    {
        //checks if the target (aka player) and cop are in the same room 
        if (npc.Target is not null) {
            var Room = GameManager.GetRoom(npc.Target);
            var Room2 = GameManager.GetRoom(npc.gameObject);
            if(Room != Room2) { npc.Target = null; } 
            return Room == Room2;
        }return false;
    }


}
