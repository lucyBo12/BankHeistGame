using Unity.Netcode;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 50;
    private Rigidbody rb => GetComponent<Rigidbody>();

    private void Start() {
        rb.AddForce(Vector3.forward * speed, ForceMode.Impulse);
        Destroy(gameObject, 2f);
    }


    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
