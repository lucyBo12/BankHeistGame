using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Unity.Mathematics;
using UnityEngine;

public class Cop_Attack : AINode

{
    public Cop_Attack(string GUID = "") : base(GUID)
    {

    }

    public override void OnUpdate(AIBase npc)
    {
        var Room = GameManager.GetRoom(npc.gameObject);
        
        //aim
        npc.transform.LookAt(npc.Target.transform);

        //gets cop to fire
        npc.Character.inventoryManager.activeWeapon.Fire();

    }
    public override float Weight(AIBase npc)
    {
        if (!npc.TargetInRange()) return 0;
        var dist = Mathf.Clamp(5 - (Vector3.Distance(npc.transform.position, npc.Target.transform.position)), 0f, 5f);
        return 1f - (dist / 5f);
    }

    public override void OnEnd(AIBase npc)
    {
        Debug.LogWarning("Attack: End");
        var room = GameManager.GetRoom(npc.gameObject);
        if (room is not null && room.HasInhibtant(npc.Target)) return;

        npc.Target = null;
        Debug.LogWarning("Attack: End [Null Target]");
    }

    public override bool Active(AIBase npc) => npc.Target is not null && npc.TargetInRange();

}
