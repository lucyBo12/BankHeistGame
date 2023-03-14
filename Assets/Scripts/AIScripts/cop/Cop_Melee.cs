using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cop_Melee : AINode
{
    float coolDown = 3;
    float timer = 0;

    public Cop_Melee(string GUID = "") : base(GUID)
    {

    }

    public override bool Active(AIBase npc)
    {
        return Vector3.Distance(npc.transform.position, npc.Target.transform.position) < 1;

    }

    public override float Weight(AIBase npc)
    {
        return Active(npc) ? 1 : 0;
    }

    public override void OnUpdate(AIBase npc)
    {
       if (timer > 0 )
        {
            timer -= Time.deltaTime;
            return;
        }

        timer = coolDown;
        Debug.Log("nice job!");
        npc.Target.GetComponent<Character>().Damage(10);
        //
    }
}
