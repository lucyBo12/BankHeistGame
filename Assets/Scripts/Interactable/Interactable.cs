using Unity.Netcode;
using Unity.Tutorials.Core.Editor;
using UnityEngine;

public class Interactable : NetworkBehaviour
{
    public bool canInteract = true;
    public bool destroyOnInteract = false;
    public bool hoverIdle = true;
    public string promptMessage = "Use";
    public Vector3 promptOffset;
    protected InteractPromt prompt => GetComponentInChildren<InteractPromt>();


    private void Start() {
        if (!hoverIdle) return;
        LeanTween.moveY(gameObject, transform.position.y + .2f, 1f).setLoopPingPong();
    }

    public virtual void Interact() { 
        if(destroyOnInteract)
            Destroy(gameObject);
    }

    public virtual void ShowPrompt() {
        if (promptMessage.IsNullOrEmpty()) return;
        else if (prompt == null) {
           InteractPromt.CreateNewPrompt(transform, promptOffset);
        }

        prompt.SetPrompt(promptMessage);
    }

    public virtual void ShowPrompt(string message) {
        if (promptMessage.IsNullOrEmpty()) return;
        else if (prompt == null) {
            InteractPromt.CreateNewPrompt(transform, promptOffset);
        }

        prompt.SetPrompt(message);
    }

    public virtual void ClosePrompt() {
        if (prompt == null) return;
        Destroy(prompt.gameObject);
    }

    public override bool Equals(object other)
    {
        if (other == null) return false;
        if(other.GetType() != typeof(Interactable)) 
            return false;

        Interactable interactable = (Interactable)other;
        return interactable.name.Equals(name) && 
            interactable.transform.position.Equals(transform.position);
    }

}
