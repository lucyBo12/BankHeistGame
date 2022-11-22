using UnityEngine;

public class ShootingRangeTarget : MonoBehaviour
{
    public bool isCivillian;

    public void Move(Vector3 start, float distance, float time)
    {
        transform.position = new Vector3(start.x, start.y + 0.5f, start.z);
        LeanTween.moveX(gameObject, start.x + distance, time);
        Destroy(gameObject, time);
    }

}
