using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Room : MonoBehaviour
{
    public Transform[] coverPoints; 
    [SerializeField] private bool showGizmo;
    public List<GameObject> inhabitants = new List<GameObject>();
    public Color gizmoColor = new Color(0, 1, 0, 0.5f);
    public BoxCollider coll => GetComponent<BoxCollider>();

    private void Start()
    {
        GameManager.Rooms.Add(this); 
    }

    public bool HasInhibtant(GameObject other)
    {
        foreach (GameObject G in inhabitants)
        {
            if(G.name.Equals(other.name))
                return true;
        }
        return false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player") && !other.CompareTag("NPC"))
            return;

        if (inhabitants.Contains(other.gameObject))
            return;

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
        if (!showGizmo) return; 
        Gizmos.color = gizmoColor;
        Gizmos.DrawCube(transform.position + coll.center, coll.size);
    }

    
}
