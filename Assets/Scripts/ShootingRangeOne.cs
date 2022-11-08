
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingRangeOne : MonoBehaviour
{
    public static ShootingRangeOne Instance;
    private void Awake() => Instance = this;


    [SerializeField]
    private GameObject hostileTarget, civillianTarget, spawnLeft, spawnRight;
    private Coroutine coroutine;
    [SerializeField]
    private int enemyCount = 30;
    [SerializeField]
    private float leftSpawnRate = 0.5f;
    [SerializeField]
    private float rightSpawnRate = 0.2f;

    private float hostileRate = 0f;

    public bool isActive = false;

    public void StartChallenge() {
        if(coroutine != null)
            StopCoroutine(coroutine);

        coroutine = StartCoroutine(Challenge());
        isActive = true;
    }

    public void StopChallenge() {
        if (coroutine != null)
            StopCoroutine(coroutine);

        foreach (Transform c in spawnLeft.transform) { 
            Destroy(c);
        }
        foreach (Transform c in spawnRight.transform) {
            Destroy(c);
        }

        isActive = false;
    }

    IEnumerator Challenge() {
        Debug.Log("Start");

        int spawnCount = enemyCount;
        while (spawnCount > 0) {

            //Wait for shortest wait
            yield return new WaitForSeconds(leftSpawnRate < rightSpawnRate ? 
                leftSpawnRate : rightSpawnRate);

            //Determine what should spawn
            var prefab = hostileTarget;
            float roll = Random.Range(0f, 1f);
            if (roll < hostileRate) {
                prefab = civillianTarget;
                hostileRate = 0f;
            }
            else {
                hostileRate += hostileRate == 0 ? 0.1f : Mathf.Pow(hostileRate, 1.1f);
            }

            //Spawn on shortest spawn 
            var spawn = leftSpawnRate < rightSpawnRate ? spawnLeft : spawnRight;
            Spawn(prefab, spawn.transform);
            spawnCount--;

            yield return new WaitForSeconds(leftSpawnRate > rightSpawnRate ?
                leftSpawnRate : rightSpawnRate);

            //Determine what should spawn
            prefab = hostileTarget;
            roll = Random.Range(0f, 1f);
            if (roll < hostileRate)
            {
                prefab = civillianTarget;
                hostileRate = 0f;
            }
            else
            {
                hostileRate += hostileRate == 0 ? 0.1f : Mathf.Pow(hostileRate, 1.1f);
            }

            //Spawn on shortest spawn 
            spawn = leftSpawnRate > rightSpawnRate ? spawnLeft : spawnRight;
            Spawn(prefab, spawn.transform);
            spawnCount--;

        }

        Debug.Log("End");
    }

    private void Spawn(GameObject prefab, Transform spawn) =>
        Instantiate(prefab, spawn.transform);


}
