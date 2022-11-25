using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectPool 
{
    private static bool ResourcesLocationPresent;
    private static Transform Bullet { get; set; }



    [RuntimeInitializeOnLoadMethod]
    private static void Initialize() { 
        
    }

}
