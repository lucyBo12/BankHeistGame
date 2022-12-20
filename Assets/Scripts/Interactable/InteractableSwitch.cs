using UnityEngine;

public class InteractableSwitch : Interactable
{
    [SerializeField] 
    private string onMessage, offMessage;
    [SerializeField]
    protected bool isActive;

    public override void Interact(Transform transform) {
        isActive = !isActive;
        ShowPrompt(isActive ? onMessage : offMessage);
        base.Interact(transform);
    }
}
