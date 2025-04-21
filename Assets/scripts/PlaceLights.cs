using System;
using UnityEngine;

public class PlaceLights : MonoBehaviour
{
    bool finishedPlacing = false;
    public GameObject seekLightPrefab;
    public Collider[] MazeArea;
    public Collider ExcludeArea;
    public float offsetZ = 1f;

    Vector3 gizmoPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            finishedPlacing = true;        
        }

        if (Input.GetMouseButtonDown(0))
        {
            PlaceSeekLight();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(gizmoPos - Vector3.up * 1, CheckWallRadius);
    }

    public void PlaceSeekLight()
    {
        if (!finishedPlacing)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.transform.position.z;// - offsetZ;
            Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
            objectPos.y = 1;
            objectPos.z -= offsetZ;

            gizmoPos = objectPos;

            if (ExcludeArea.bounds.Contains(objectPos))
            {
                print("Cannot place light here");

            }
            else
            {
                foreach (Collider mazeCollider in MazeArea)
                {
                    if (mazeCollider.bounds.Contains(objectPos))
                    {

                        Instantiate(seekLightPrefab, objectPos, Quaternion.identity);
                        return;
                    }
                    else
                    {
                        print("Cannot place light outside maze area");
                    }
                }
            }
        }
    }
}
