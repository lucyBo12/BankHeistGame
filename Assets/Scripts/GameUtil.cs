using UnityEngine;

public static class GameUtil
{

    public static Transform ClosestTransform(Transform transform, Transform[] others) {
 
        Transform t = transform;
        float d = float.MaxValue;

        foreach (Transform o in others) {
            t = Vector3.Distance(o.position, transform.position) < d ? o : t;
        }

        return t;
    }

 
    



}
