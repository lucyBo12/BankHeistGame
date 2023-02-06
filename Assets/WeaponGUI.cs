using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class WeaponGUI : MonoBehaviour
{
    public static WeaponGUI Instance;
    private void Awake() => Instance = this;
    public Image primaryIcon;
    public Image secondaryIcon;


    private void Start()
    {
        primaryIcon.sprite = null;
        secondaryIcon.sprite = null;
    }


    public void UpdateWeapon(Weapon weapon)
    {
        Image icon = weapon.weaponType==Weapon.WeaponType.primary ? primaryIcon : secondaryIcon;
        icon.sprite = weapon.icon;
    }


}
