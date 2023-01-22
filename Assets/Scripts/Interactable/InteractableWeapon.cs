using UnityEngine;

public class InteractableWeapon : Interactable
{
    [SerializeField] private Weapon weapon;

    public override void Interact(Transform user)
    {
        var character = user.GetComponent<Character>();
        if (!character) return;

        character.inventoryManager.Assign(weapon);
        base.Interact(user);
    }

}
