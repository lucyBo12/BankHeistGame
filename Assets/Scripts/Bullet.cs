using UnityEngine;

public class Bullet : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        HitTarget(collision.gameObject);
        bool hitCharacter = collision.transform.CompareTag("Player") || collision.transform.CompareTag("Cop");
        transform.GetChild(0).gameObject.SetActive(hitCharacter);
        transform.GetChild(1).gameObject.SetActive(!hitCharacter);

        transform.GetChild(2).gameObject.SetActive(false);
        GetComponent<Rigidbody>().isKinematic = true;
        Invoke("Disable", 1f);
    }

    private void HitTarget(GameObject target) {
        switch (target.tag) { 
            case "RangeTarget":
                target.GetComponent<ShootingRangeTarget>().Hit();
                break;
                case "Player":
                    target.GetComponent<Character>().Damage(10);
                break;
                case "Cop":
                    target.GetComponent<Character>().Damage(10);
                break;
            default:
                return;
        }
    }

    private void Disable() {
        gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(true);
    }
}
