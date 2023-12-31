using Unity.Netcode;
using UnityEngine;

public class Interactable : NetworkBehaviour
{
    public bool canInteract = true;
    public bool destroyOnInteract = false;
    public bool hoverIdle = true;
    public string promptMessage = "Use";
    public Vector3 promptOffset = new Vector3(0, 90, 20);
    protected InteractPromt prompt => GetComponentInChildren<InteractPromt>();


    private void Start() {
        if (!hoverIdle) return;
        LeanTween.moveY(gameObject, transform.position.y + .2f, 1f).setLoopPingPong();
    }

    public virtual void Interact(Transform user) {
        if (!destroyOnInteract) return;

        user.GetComponent<InteractableAgent>().interactables.Remove(this);
        Destroy(gameObject);
    }

    public virtual void ShowPrompt() {
        if (string.IsNullOrEmpty(promptMessage)) return;
        if (prompt == null) {
            InteractPromt.CreateNewPrompt(transform, promptOffset);
        }

        prompt.SetPrompt(promptMessage);
    }

    public virtual void ShowPrompt(string message) {
        if (string.IsNullOrEmpty(promptMessage)) return;
        if (prompt == null) {
            InteractPromt.CreateNewPrompt(transform, promptOffset);
        }

        prompt.SetPrompt(message);
    }

    private void OnTriggerEnter(Collider other)
    {
        var agent = other.GetComponent<InteractableAgent>();
        if (!agent) return;

        agent.interactables.Add(this);
    }

    private void OnTriggerExit(Collider other)
    {
        var agent = other.GetComponent<InteractableAgent>();
        if (!agent) return;
        agent.interactables.Remove(this);
        ClosePrompt();
    }

    public virtual void ClosePrompt() {
        if (prompt == null) return;
        Destroy(prompt.gameObject);
    }

}
