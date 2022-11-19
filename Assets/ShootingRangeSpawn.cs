using UnityEngine;

public class ShootingRangeSpawn : MonoBehaviour
{
    [SerializeField] 
    private GameObject hostileTarget, civillianTarget;
    [SerializeField]
    private float distance, time;

    [SerializeField]private float hostileRate = 0f;
    private Vector3 targetDestination => 
        new Vector3(transform.position.x + distance, transform.position.y + 0.5f, transform.position.z);


    public void Spawn() {
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

        ShootingRangeTarget target = SpawnTarget(prefab);
        target.Move(transform.position, distance, time);
    }

    private ShootingRangeTarget SpawnTarget(GameObject prefab) {
        GameObject obj = Instantiate(prefab, transform);
        obj.transform.rotation = new Quaternion(0, transform.rotation.y, 0, 0);
        return obj.GetComponent<ShootingRangeTarget>();
    }


    public void Clear() {
        foreach (Transform child in transform) { 
            Destroy(child);
        }
    }

}
