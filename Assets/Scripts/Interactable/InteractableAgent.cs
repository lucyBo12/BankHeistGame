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


    private void Start()
    {
        GameManager.Input.Player.Interact.performed += evt => Interact();
    }

    private void Interact() {
        if (!closest) return;

        closest.Interact(transform);
    }

    private void FixedUpdate()
    {
        if (closest) {
            if (closestInteractableDistance > radius) {
                closest.ClosePrompt();
                closest = null;
            }
        }

        Interactable x = null;
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, radius, Vector3.up, 5f, layer);
        foreach (RaycastHit h in hits) {
            var interactable = h.transform.GetComponent<Interactable>();
            if (!x) {
                x = interactable;
            }
            else { 
                x = (Vector3.Distance(interactable.transform.position, transform.position)) < (Vector3.Distance(x.transform.position, transform.position)) ?
                    interactable : x;
            }
        }

        if (x && !x.Equals(closest)) {
            if (closest) {
                closest.ClosePrompt();
            }

            closest = x;
            closest.ShowPrompt();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }


}
