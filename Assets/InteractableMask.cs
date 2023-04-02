using UnityEngine;

public class InteractableMask : Interactable
{

    public override void Interact(Transform user)
    {
        var character = user.GetComponent<Character>();
        if (character is null) return;

        character.SetMask(MaskID());
    }

    private int MaskID() {
        for (int i = 0; i < transform.childCount; i++) { 
            if(transform.GetChild(i).gameObject.activeSelf)
                return i;
        }

        return 0;
    }
}
