using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 50;
    private Rigidbody rb => GetComponent<Rigidbody>();

    private void FixedUpdate() => rb.velocity = transform.forward * speed;

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
        transform.localPosition = Vector3.zero;
    }
}
