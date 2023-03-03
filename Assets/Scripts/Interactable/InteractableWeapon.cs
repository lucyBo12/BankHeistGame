using UnityEngine;

public class InteractableWeapon : Interactable
{
    [SerializeField] private Weapon weapon;
    public AudioSource pickUp;
    

    public override void Interact(Transform user)
    {
        var character = user.GetComponent<Character>();
        if (!character) return;

        character.inventoryManager.Assign(weapon);
        base.Interact(user);
        pickUp.Play();
    }

}
