using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CopSpawner : MonoBehaviour
{
    public static CopSpawner Instance { get; private set; }
    public void Awake() => Instance = this;


    [SerializeField] private List<GameObject> activeUnits = new List<GameObject>();
    [SerializeField] private Transform[] spawnPoints = new Transform[0];



    private int unitCount => Mathf.CeilToInt(Mathf.Pow((2 + GameManager.Players.Count), 1 + (GameManager.WantedLevel / 5)));
    private Coroutine coroutine;


    private void LateUpdate()
    {
        if (GameManager.State != GameState.Active)
            return;

        if (coroutine is not null) return;

        coroutine = StartCoroutine(SpawnCor());
    }

    public void Unlist(GameObject unit) {
        if (!activeUnits.Contains(unit)) return;
        activeUnits.Remove(unit);
    }

    private IEnumerator SpawnCor() {
        while (GameManager.State == GameState.Active) {
            yield return new WaitForSecondsRealtime(GameManager.WantedLevel * 2);
            int count = unitCount;
            if (count <= activeUnits.Count) continue;

            for (int i = activeUnits.Count; i < count; i++)
            {
                var unit = ObjectPool.Get(ObjectPool.CopPool);
                activeUnits.Add(unit);
                unit.transform.position = GameUtil.Random(spawnPoints).position;
                unit.gameObject.SetActive(true);
                yield return new WaitForSeconds(1.2f);
            }
        }
        
        coroutine = null;
    }

}
