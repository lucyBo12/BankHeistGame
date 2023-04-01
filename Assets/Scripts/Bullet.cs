using UnityEngine;

public class Bullet : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        HitTarget(collision.gameObject);
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
        GetComponent<Rigidbody>().isKinematic = true;
        Invoke("Disable", 1f);
    }

    private void HitTarget(GameObject target) {
        switch (target.tag) { 
            case "RangeTarget":
                target.GetComponent<ShootingRangeTarget>().Hit();
                break;
                case "Player":
                    target.GetComponent<Character>().Damage(5);
                break;
                case "Cop":
                    target.GetComponent<Character>().Damage(5);
                break;
            default:
                return;
        }
    }

    private void Disable() {
        gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
    }
}
