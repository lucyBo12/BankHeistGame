using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Weapon activeWeapon;
    [SerializeField] private Transform primary, sidearm;
    [SerializeField] private RigBuilder rig;

    private void Start()
    {
        GameManager.Input.Player.Fire.performed += evt => FireWeapon();
        activeWeapon = sidearm.GetChild(0).GetComponent<Weapon>();
    }

    private void FireWeapon() { 
        if(!activeWeapon) return;
        activeWeapon.Fire();
    }

    public void Assign(Weapon weapon) {
        var slot = weapon.weaponType == Weapon.WeaponType.primary ? primary : sidearm;
        var child = slot.GetChild(0);
        if (child.CompareTag("Weapon")) { 
            Destroy(child.gameObject);
        }

        var obj = Instantiate(weapon.gameObject, slot);
        obj.transform.SetAsFirstSibling();

        int index = weapon.weaponType == Weapon.WeaponType.primary ? 1 : 2;
        int reverse = index == 1 ? 1 : 2;

        rig.layers[index].active = true;
        rig.layers[reverse].active = false;

        primary.gameObject.SetActive(index == 1);
        sidearm.gameObject.SetActive(index == 2);
    }

    public Weapon.WeaponType ActiveWeaponType() {
        return activeWeapon ? activeWeapon.weaponType : Weapon.WeaponType.sidearm;
    }
}
