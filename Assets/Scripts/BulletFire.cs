using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletFire : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 30f;
    public float lifetime = 3f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // if(isFiring)
      //  {
       //     Fire();
      //  }
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(),
            bulletSpawn.parent.GetComponent<Collider>());
        
        bullet.transform.position = bulletSpawn.position;
        Vector3 rotation = bullet.transform.rotation.eulerAngles;
        bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);
        StartCoroutine(DestroyBullet(bullet, lifetime));
    }

    private IEnumerator DestroyBullet(GameObject bullet, float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(bullet);
    }
}
