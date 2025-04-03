using System;
using UnityEngine;

public class PlaceLights : MonoBehaviour
{
    bool finishedPlacing = false;
    public GameObject seekLightPrefab;
    public Collider[] MazeArea;
    public Collider ExcludeArea;
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

    public void PlaceSeekLight()
    {
        if (!finishedPlacing)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10;
            Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
            objectPos.y = 1;

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
