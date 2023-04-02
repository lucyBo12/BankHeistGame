
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
        Debug.Log($"I: 1 [{(closest ? closest.name : "null")}]");
        if (!active) return;

        Debug.Log("I: 2");
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
