
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingRangeOne : MonoBehaviour
{
    public static ShootingRangeOne Instance;
    private void Awake() => Instance = this;


    [SerializeField]
    private ShootingRangeSpawn spawnLeft, spawnRight;
    private Coroutine coroutine;
    [SerializeField]
    private int enemyCount = 30;
    [SerializeField]
    private float leftSpawnRate = 0.5f;
    [SerializeField]
    private float rightSpawnRate = 0.2f;


    public void StartChallenge() {
        if(coroutine != null)
            StopCoroutine(coroutine);

        coroutine = StartCoroutine(Challenge());
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
    }

    IEnumerator Challenge() {
        Debug.Log("Start");

        int spawnCount = enemyCount;
        while (spawnCount > 0) {

            //Wait for shortest wait
            yield return new WaitForSeconds(leftSpawnRate < rightSpawnRate ? 
                leftSpawnRate : rightSpawnRate);
    
            //Spawn on shortest spawn 
            var spawn = leftSpawnRate < rightSpawnRate ? spawnLeft : spawnRight;
            spawn.Spawn();
            spawnCount--;

            yield return new WaitForSeconds(leftSpawnRate > rightSpawnRate ?
                leftSpawnRate : rightSpawnRate);

            //Spawn on shortest spawn 
            spawn = leftSpawnRate > rightSpawnRate ? spawnLeft : spawnRight;
            spawn.Spawn();
            spawnCount--;
        }

        Debug.Log("End");
    }

}
