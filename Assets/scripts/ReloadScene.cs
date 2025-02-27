using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class ReloadScene : MonoBehaviour
{
    public string reloadScene;
    public float delay = 1;
    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(LoadSceneCR());
    }
    IEnumerator LoadSceneCR()
    {
        yield return new WaitForSeconds(delay);
        UnityEngine.SceneManagement.SceneManager.LoadScene(reloadScene);
    }
    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(LoadSceneCR());
    }
}