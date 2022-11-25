using System.Threading.Tasks;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Gun")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform muzzle;
    [SerializeField] private int2 ammo = new int2(12, 32);

    [Header("Config")]
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private float reloadTime = 1f;
    public bool canFire { get; private set; }
    public WeaponType weaponType;
    public enum WeaponType { 
        primary, sidearm
    }


    public virtual async void Fire() {
        Instantiate(bullet, muzzle);
        ammo[0] -= 1;
        canFire = false;
        await Wait(fireRate);
        canFire = true;
    }

    public virtual async void Reload() {
        await Wait(reloadTime);
    }

    protected virtual async Task Wait(float time) {
        await Task.Delay(Mathf.CeilToInt(1000 * time));
    }
}
