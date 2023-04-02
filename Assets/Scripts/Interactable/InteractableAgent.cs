
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractableAgent : MonoBehaviour
{
    private Interactable active;
    private Interactable closest => interactables.OrderBy(go => (go.transform.position - transform.position).sqrMagnitude).FirstOrDefault();
    public List<Interactable> interactables = new List<Interactable>();


    private void Start() =>
        GameManager.Input.Player.Interact.performed += evt => Interact();


    private void Interact() {
        if (!active) return;
        if (Vector3.Distance(active.transform.position, transform.position) > 2f) return;
        active.Interact(transform);
        
    }

    private void FixedUpdate()
    {
        if (!closest) return;
        if (closest == active) return;

        if (active != null)
            active.ClosePrompt();

        active = closest;
        active.ShowPrompt();
    }


}
