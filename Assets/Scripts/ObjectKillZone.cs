using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ObjectKillZone : MonoBehaviour
{
    private BoxCollider boxCollider => GetComponent<BoxCollider>();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + boxCollider.center, boxCollider.size);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 8) return;

        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        other.gameObject.SetActive(false);
    }

}
