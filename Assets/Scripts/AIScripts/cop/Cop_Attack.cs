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
        var Charge = npc.profile.charge;
        if(Charge < 0.3) {Charge = 0.3f;};
        var Clip = npc.GetComponent<InventoryManager>().activeWeapon.clip.quantity;
        var ShotsFired = Clip * Charge; 

        return ShotsFired;
    }

    public override bool Active(AIBase npc)
    {
        return npc.Target is not null && npc.TargetInRange();
    }

}
