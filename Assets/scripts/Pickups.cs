using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag== "Player")
        {
            ScoreHandler scoreHandler = collision.collider.gameObject.GetComponent<ScoreHandler>();
            scoreHandler.scoreCount++;
            Destroy(gameObject);
        }
    }
}
