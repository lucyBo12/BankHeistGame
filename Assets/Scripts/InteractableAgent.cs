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
    public float closestInteractableDistance => closest ? Vector3.Distance(closest.transform.position, transform.position) : float.MaxValue;


    private void FixedUpdate()
    {
        if (closest != null && closestInteractableDistance > radius) {
            SetInteractable();
            return;
        }

        Interactable selected = closest;
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, radius, Vector3.up, 5f, layer);
        foreach (RaycastHit h in hits) {
            var interactable = h.transform.GetComponent<Interactable>();
            selected = (Vector3.Distance(interactable.transform.position, transform.position)) < closestInteractableDistance ? 
                interactable : closest;
        }

        if (selected == null || selected.Equals(closest)) return;
        SetInteractable(selected);
    }

    private void SetInteractable(Interactable interactable = null) {
        if (interactable == null) {
            if (closest) closest.ClosePrompt();
            closest = null;
            return;
        }

        if (closest != null) 
            closest.ClosePrompt();

        closest = interactable;
        closest.ShowPrompt();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }


}
