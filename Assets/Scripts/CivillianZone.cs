using Unity.Mathematics;
using UnityEngine;

public class CivillianZone : MonoBehaviour
{
    [SerializeField] private new bool enabled;
    [SerializeField] private int2 zone;
    [SerializeField] private int count = 5;

    private float halfX => zone.x / 2;
    private float halfZ => zone.y / 2; 
    private float2 upper => new float2(transform.position.x - halfX, transform.position.z + halfZ);
    private float2 lower => new float2(transform.position.x + halfX, transform.position.z - halfZ);

    private void OnEnable()
    {
        if (!enabled) return;
        Spawn();
    }

    private void Spawn() {
        for (int i = 0; i < count; i++) {
            GameObject gO = ObjectPool.Get(ObjectPool.CivPool);
            
        }
    }

    private Vector3 GetValidPosition() {
        if (zone.x * zone.y == 0) return transform.position;

        for (int i = 0; i < 10; i++) { 
            var x = UnityEngine.Random.Range(lower.x, upper.x);
            var z = UnityEngine.Random.Range(lower.y, upper.y);

            Vector3 point = new Vector3(x, transform.position.y, z);
            Collider[] hits = Physics.OverlapSphere(point, 0.33f);

            if (hits.Length == 0) 
                return point;
        }

        return transform.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 1f, 1f, 0.33f);
        Gizmos.DrawCube(transform.position, new Vector3(zone.x, 3, zone.y));
    }

}
