using UnityEngine;
using System.Collections.Generic;

public class CopSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> activeUnits = new List<GameObject>();

    private async void FixedUpdate()
    {
        if (GameManager.WantedLevel == 0) return;

    }

}
