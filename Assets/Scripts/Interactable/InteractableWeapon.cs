using UnityEngine;

public class InteractableWeapon : Interactable
{
    [SerializeField] private Weapon weapon;

    public override void Interact(Transform user)
    {
        //InventoryManager.Instance.Assign(weapon);
        base.Interact(user);
    }

}
