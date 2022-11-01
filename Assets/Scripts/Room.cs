using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Room : MonoBehaviour
{
    public List<GameObject> inhabitants = new List<GameObject>();
    public Color gizmoColor = new Color(0, 1, 0, 0.5f);
    public BoxCollider coll => GetComponent<BoxCollider>();

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("A");
        if(!other.CompareTag("Player") && !other.CompareTag("NPC"))
            return;

        Debug.Log("A");
        if (inhabitants.Contains(other.gameObject))
            return;

        Debug.Log("C");
        inhabitants.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("NPC"))
            return;

        if (!inhabitants.Contains(other.gameObject))
            return;

        inhabitants.Remove(other.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawCube(transform.position + coll.center, coll.size);
    }
}
