using Unity.Netcode;
using UnityEngine;

public class Interactable : NetworkBehaviour
{
    public bool canInteract = true;
    public bool destroyOnInteract = false;
    public bool hoverIdle = true;


    private void Start() {
        LeanTween.moveY(gameObject, transform.position.y + .2f, 1f).setLoopPingPong();
    }

    public virtual void Interact() { 
        
    }

}
