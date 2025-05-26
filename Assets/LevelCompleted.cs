using System.Collections;
using UnityEngine;

public class LevelCompleted : MonoBehaviour
{
    public ScoreHandler scoreHandler;
    public GameObject tutorial;
    public GameObject lava;
    public GameObject earth;

    private void Start()
    {
        StartCoroutine(WaitForScoreHandler());
    }

    IEnumerator WaitForScoreHandler()
    {
        while (scoreHandler == null)
        {
            scoreHandler = Object.FindAnyObjectByType<ScoreHandler>();
            yield return null;        
        }

        yield return null;

        levelcompleted();
    }
    public void levelcompleted()
    {
        Debug.Log("levelcompleted was called");
        if (scoreHandler.scoreCount == 1)
        {
            tutorial.SetActive(true);
        }
        else if (scoreHandler.scoreCount == 2)
        {
            lava.SetActive(true);
            tutorial.SetActive(true);
        }
        else if(scoreHandler.scoreCount == 3) 
        { 
            earth.SetActive(true);
            tutorial.SetActive(true);
            lava.SetActive(true);
        }
    }
}
