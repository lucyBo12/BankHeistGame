using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_Player : MonoBehaviour
{

    void Start() =>
        GameManager.Players.Add(transform);


}
