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

        if (collision.gameObject.layer != 7) return;

        switch (collision.gameObject.tag) {
            case "RangeTarget":
                collision.gameObject.GetComponent<ShootingRangeTarget>().Hit();
                break;
        }
    }
}
