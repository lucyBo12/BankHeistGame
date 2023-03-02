using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Room : MonoBehaviour
{
    public Transform[] coverPoints; 
    [SerializeField] private bool showGizmo;
    public List<GameObject> inhabitants = new List<GameObject>();
    public List<GameObject> cops => inhabitants.Where(x => x.CompareTag("Cop")).ToList();
    public List<GameObject> players => inhabitants.Where(x => x.CompareTag("Player")).ToList();
    public Transform[] alarms;
    public Transform[] exitPoint;
    public NPCGoal[] goals;
    public Color gizmoColor = new Color(0, 1, 0, 0.5f);
    public LayerMask coverLayer;
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


    [ContextMenu("AssignCoverPoints")]
    private void AssignCoverPoints()
    {
        RaycastHit[] hits = Physics.BoxCastAll(transform.position, coll.size, Vector3.up, Quaternion.identity, 1f, coverLayer);
        coverPoints = new Transform[hits.Length];
        for(int i = 0; i < hits.Length; i++)
        {
            coverPoints[i] = hits[i].transform; 
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player") && !other.CompareTag("Cop"))
            return;

        if (inhabitants.Contains(other.gameObject))
            return;

        inhabitants.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Cop"))
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
