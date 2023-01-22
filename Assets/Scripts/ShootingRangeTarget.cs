using UnityEngine;

public class ShootingRangeTarget : MonoBehaviour
{
    public bool isCivillian;

    public void Hit()
    {
        LeanTween.rotateX(gameObject, -45, 0.2f);
        LeanTween.color(gameObject, Color.clear, 0.2f);
        Destroy(gameObject, 0.5f);
        GetComponent<Rigidbody>().isKinematic = false;
    }

    public void Move(Vector3 start, float distance, float time)
    {
        transform.position = new Vector3(start.x, start.y + 1.65f, start.z);
        LeanTween.moveX(gameObject, start.x + distance, time);
        Destroy(gameObject, time);
    }

}
