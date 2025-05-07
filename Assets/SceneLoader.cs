using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string loadLevel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void LoadLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(loadLevel);
    }
}
