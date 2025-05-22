using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    ScoreHandler scoreHandler;
    public string level;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Endgame()
    {
        if (scoreHandler.scoreCount == 3)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(level);
        }
    }
}
