using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 50;
    private Rigidbody rb => GetComponent<Rigidbody>();

    private void FixedUpdate() => rb.velocity = Vector3.forward * speed;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Hit {collision.transform.name}");
        gameObject.SetActive(false);
        transform.localPosition = Vector3.zero;
    }
}
