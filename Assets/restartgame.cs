using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restartgame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void restartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("menu");
    }
}
