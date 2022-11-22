using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableAgent : MonoBehaviour
{
    [SerializeField, Range(0.1f, 5f)]
    private float radius;
    [SerializeField]
    private LayerMask layer;
    public Interactable closest;
    public float closestInteractableDistance => Vector3.Distance(closest.transform.position, transform.position);


    private void FixedUpdate()
    {
        if (closest) {
            closest = closestInteractableDistance > radius ? null : closest;
        }

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, radius, Vector3.up, 5f, layer);
        foreach (RaycastHit h in hits) {
            var interactable = h.transform.GetComponent<Interactable>();
            if (!closest) {
                closest = interactable;
            }
            else { 
                closest = (Vector3.Distance(interactable.transform.position, transform.position)) < closestInteractableDistance ?
                    interactable : closest;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }


}
