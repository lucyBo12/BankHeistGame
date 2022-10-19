using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICopBehaviour : AIBase
{

    private void Awake()
    {
        CurrentNode = new AICivillianMovement();
    }


}
