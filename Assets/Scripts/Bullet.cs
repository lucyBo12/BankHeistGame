using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 50;
    private Rigidbody rb => GetComponent<Rigidbody>();

    private void Start() => rb.AddForce(Vector3.forward, ForceMode.Acceleration);

    private void OnCollisionEnter(Collision collision)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.localPosition = Vector3.zero;
        Invoke("Disable", 1f);
    }

    private void Disable() {
        gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
