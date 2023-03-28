using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class InventoryManager : NetworkBehaviour
{
    public Weapon activeWeapon;
    [SerializeField] private Transform primary, sidearm;
    [SerializeField] private RigBuilder rig;

    public AudioSource gunSound;

    private void Start()
    {
        if (IsOwner) {
            GameManager.Input.Player.Fire.performed += evt => FireWeapon();
        }
        Weapon weapon = sidearm.GetChild(0).GetComponent<Weapon>();
        activeWeapon = weapon;
        WeaponGUI.Instance.UpdateWeapon(weapon);
        gunSound = weapon.fireSound;
    }

    private void FireWeapon() { 
        if(!activeWeapon) return;
        activeWeapon.Fire();
        gunSound.Play();
    }

    public void Assign(Weapon weapon) {
        WeaponGUI.Instance.UpdateWeapon(weapon);
        var slot = weapon.weaponType == Weapon.WeaponType.primary ? primary : sidearm;
        var child = slot.GetChild(0);
        if (child.CompareTag("Weapon")) { 
            Destroy(child.gameObject);
        }

        var obj = Instantiate(weapon.gameObject, slot);
        obj.transform.SetAsFirstSibling();

        int index = weapon.weaponType == Weapon.WeaponType.primary ? 1 : 0;
        int reverse = index == 1 ? 0 : 1;

        rig.layers[index].active = true;
        rig.layers[reverse].active = false;

        primary.gameObject.SetActive(index == 1);
        sidearm.gameObject.SetActive(index == 2);
        gunSound = weapon.fireSound;
    }

    public Weapon.WeaponType ActiveWeaponType() {
        return activeWeapon ? activeWeapon.weaponType : Weapon.WeaponType.sidearm;
    }
}
