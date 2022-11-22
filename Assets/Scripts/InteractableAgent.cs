using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableAgent : MonoBehaviour
{
    [SerializeField, Range(0.1f, 5f)]
    private float radius;
    [SerializeField]
    private LayerMask layer;
    public GameObject closest;
    public float closestInteractableDistance => closest != null ? 
        Vector3.Distance(closest.transform.position, transform.position) : float.MaxValue;


    private void Start() {
        GameManager.Input.Player.Interact.performed += evt => InvokeInteractable();
    }

    private void InvokeInteractable() {
        if (!closest) return;

        Interactable interactable = closest.GetComponent<Interactable>();
        if (!interactable) return;

        interactable.Interact(transform);
    }

    private void FixedUpdate()
    {
        if (closest != null && closestInteractableDistance >= (radius * 1.1f)) {
            SetInteractable(null);
            return;
        }

        GameObject selected = closest;
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, radius, Vector3.up / 2, radius, layer);
        foreach (RaycastHit h in hits) {
            selected = (Vector3.Distance(h.transform.position, transform.position)) < closestInteractableDistance ? 
                h.transform.gameObject : selected;
        }

        if (selected == null || selected.Equals(closest)) return;
        SetInteractable(selected);
    }

    private void SetInteractable(GameObject interactableObj) {
        if (interactableObj == null) {
            var interactable = closest.GetComponent<Interactable>();
            if (interactable) interactable.ClosePrompt();
            closest = null;
            return;
        }

        if (closest != null) 
            closest.GetComponent<Interactable>().ClosePrompt();

        closest = interactableObj;
        closest.GetComponent<Interactable>().ShowPrompt();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }


}
