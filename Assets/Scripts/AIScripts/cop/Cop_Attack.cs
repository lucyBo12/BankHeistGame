using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        Vector3 playerPos = npc.Target.transform.position;
        Vector3 npcPos = npc.transform.position;
        Vector3 delta = new Vector3(playerPos.x - npcPos.x, 0.0f, playerPos.z - npcPos.z);
        Quaternion rotation = Quaternion.LookRotation(delta);
        npc.transform.rotation = rotation;
        //gets cop to fire
        npc.Character.inventoryManager.activeWeapon.Fire();

    }
    public override float Weight(AIBase npc)
    {
        var Charge = npc.profile.charge;
        if(Charge < 0.3) {Charge = 0.3f;};
        var Clip = npc.GetComponent<Weapon>().clip.quantity;
        var ShotsFired = Clip * Charge; 

        return ShotsFired;
        /*var Room = GameManager.GetRoom(npc.gameObject);
        var ae = Room.players.Count; //ae number of enemies^*
        if (ae == 0)
        {
            return 0;
        }
        var aa = Room.cops.Count; //aa num of allays^*
        var hp = npc.GetComponent<Character>().health; //hp cop health 
        var pd = Vector3.Distance(GameUtil.ClosestTransform(npc.transform, GameManager.Players.ToArray()).position, npc.transform.position);//distance to player
       



        return ((hp+aa) - (pd+ ae));*/
    }

    /**
     * ((Ahp/pd + pa)+aa+(ph-hp))
     * ahp = ally hp*
     * pd = player distance *
     * aa = number of players (exluding pd)
     * ph = targeted player hp*
     * hp = cop health*
     * 
     * */
}
