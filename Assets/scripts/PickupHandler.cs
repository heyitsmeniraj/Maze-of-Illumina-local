using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickupHandler : MonoBehaviour
{
    AudioSource audioSource;
    public ScoreHandler scoreHandler;
    public string SceneName;
    SceneManager sceneManager;
    // Start is called before the first frame update
    void Start()
    {
        scoreHandler = FindFirstObjectByType<ScoreHandler>();
        sceneManager = GetComponent<SceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            NavMeshAvoidance playerBehaviour = null;
            collision.gameObject.TryGetComponent<NavMeshAvoidance>(out playerBehaviour);
            if (playerBehaviour != null)
            {
                // Set the emblem part to be visible
                playerBehaviour.emblemPartVisible = true;
            }
            if (scoreHandler != null)
            {
                //ScoreHandler scoreHandler = collision.collider.gameObject.GetComponent<ScoreHandler>();
                scoreHandler.scoreCount++;
                Destroy(gameObject, 0.5f);
                UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
            }
        }
    }
}
