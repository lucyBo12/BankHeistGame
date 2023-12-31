using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Gun")]
    [SerializeField] private Transform target;
    [SerializeField] private Transform muzzle;
    [SerializeField] public AmmoClip clip;
    [SerializeField] private GameObject bulletPrefab;

    [Header("Config")]
    public Sprite icon;
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private float reloadTime = 1f;
    [SerializeField] private float range = 20f;
    private LayerMask layer = 7;
    public bool canFire { get; private set; }
    public WeaponType weaponType;
    public enum WeaponType { 
        primary, sidearm
    }

    public AudioSource fireSound;


    private void Start() => canFire = true;

    public virtual async void Fire() {
        if (!canFire) return;
        if (clip.ammo == 0) {
            if (clip.quantity == 0) return;
            Reload();
            return;
        }

        var bullet = ObjectPool.Get(ObjectPool.BulletPool);
        bullet.SetActive(true);
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>());
        bullet.transform.position = muzzle.position;
        bullet.transform.rotation = muzzle.rotation;
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 2000, ForceMode.Force);
        
        clip.ammo -= 1;
        canFire = false;
        await Wait(fireRate);
        canFire = true;
    }

    public virtual async void Reload() {
        canFire = false;
        await Wait(reloadTime);
        clip.Reload();
        canFire = true;
    }

    private void OnDrawGizmos()
    {
        if (!target) return;
        Gizmos.DrawLine(muzzle.position, muzzle.position + muzzle.forward);
    }

    protected virtual async Task Wait(float time) {
        await Task.Delay(Mathf.CeilToInt(1000 * time));
    }

    [System.Serializable]
    public struct AmmoClip {
        public int ammo;
        public int clip;
        public int quantity;

        public void Reload() {
            var r = (clip - ammo) < quantity ? (clip - ammo) : quantity;
            quantity -= r;
            ammo += r;
        }
    }
}
