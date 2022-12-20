using UnityEngine;

public static class GameUtil
{

    public static Transform ClosestPlayer(Transform transform) {
        Transform[] players = GameManager.Players.ToArray();
        Transform t = transform;
        float d = float.MaxValue;

        foreach (Transform p in players) {
            t = Vector3.Distance(p.position, transform.position) < d ? p : t;
        }

        return t;
    }

    public static Transform ClosestAlarm(Transform transform)
    {
        Transform[] alarms = GameManager.Alarms.ToArray();
        Transform t = transform;
        float d = float.MaxValue;

        foreach (Transform a in alarms)
        {
            t = Vector3.Distance(a.position, transform.position) < d ? a : t;
        }

        return t;
    }

}
