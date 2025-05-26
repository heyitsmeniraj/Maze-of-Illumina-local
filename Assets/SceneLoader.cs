using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SceneLoader : MonoBehaviour, IPointerEnterHandler
{
    public string loadLevel;
    public AudioSource audioSource;

    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.Play();
    }


    public void LoadLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(loadLevel);
    }
}
