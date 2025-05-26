using UnityEngine;
using UnityEngine.Video;
using System.Collections;
using UnityEngine.SceneManagement;

public class QuitGameOnVideoOver : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public float delayBeforeQuit = 2f; // Seconds to wait after video ends
    public string Scenetobeloaded;
    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        Debug.Log("Video finished. Waiting before quitting...");
        StartCoroutine(QuitAfterDelay());
    }

    IEnumerator QuitAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeQuit);
        Debug.Log("Quitting now.");
        UnityEngine.SceneManagement.SceneManager.LoadScene(Scenetobeloaded);
    }
}
