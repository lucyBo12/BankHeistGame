using UnityEngine;
using System.Collections.Generic;

public class CopSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> activeUnits = new List<GameObject>();
    [SerializeField] private Transform[] spawnPoints = new Transform[0]; 

    private async void FixedUpdate()
    {
        if (GameManager.WantedLevel == 0) return;
    }

}
