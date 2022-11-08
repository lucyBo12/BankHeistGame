using UnityEngine;

public class ShootingRangeTarget : MonoBehaviour
{

    private void Start()
    {
        if (!ShootingRangeOne.Instance)
        {
            Destroy(gameObject);
            return;
        }

        Physics.Raycast(transform.position, Vector3.left, out RaycastHit hit1);
        Physics.Raycast(transform.position, Vector3.right, out RaycastHit hit2);
        var left = hit1.distance;
        var right = hit2.distance;

        var destination = left > right ? transform.position.x + left : transform.position.x + right;
        var time = left > right ? left / 2 : right / 2;
        LeanTween.moveX(gameObject, destination, time);

        Destroy(gameObject, time);
    }



}
