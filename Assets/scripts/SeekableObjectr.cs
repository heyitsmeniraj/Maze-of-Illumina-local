using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class SeekableObjectr : MonoBehaviour
{
    public int priority = 1; // Higher values mean higher priority
    public bool visited = false;
    bool placed = false;
    public float CheckWallRadius = 2;
    public LayerMask wallMask;
    public float wallOffset = 0.1f;

    private void Update()
    {
        if (placed) return;
        CheckPosition();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( other.tag == "Player")
        {
            visited = true;
        }
    }

    void CheckPosition()
    {
        List<RaycastHit> hits = new List<RaycastHit>();
        List<Vector3> dirs = new List<Vector3>();
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, CheckWallRadius, wallMask))
        {
            hits.Add(hit);
            dirs.Add(transform.forward);
        }
        if (Physics.Raycast(transform.position, -transform.forward, out hit, CheckWallRadius, wallMask))
        {
            hits.Add(hit);
            dirs.Add(-transform.forward);

        }
        if (Physics.Raycast(transform.position, transform.right, out hit, CheckWallRadius, wallMask))
        {
            hits.Add(hit);
            dirs.Add(transform.right);

        }
        if (Physics.Raycast(transform.position, -transform.right, out hit, CheckWallRadius, wallMask))
        {
            hits.Add(hit);
            dirs.Add(-transform.right);

        }

        float hitDistance = 10000;
        Vector3 hitDirection = Vector3.zero;
        for (int i= 0; i < hits.Count; i++)
        {
            if (hits[i].distance < hitDistance)
            {
                hitDistance = hits[i].distance;
                hitDirection = dirs[i];
            }
        }

        transform.position += hitDirection.normalized * (hitDistance - wallOffset);
        placed = true;
    }
}

//RaycastHit[] hits = Physics.SphereCastAll(objectPos, CheckWallRadius, Vector3.up, 1, wallMask);
//if (hits.Length > 0) 
//{
//    foreach (RaycastHit hit in hits) { print(hit.distance); }
//}