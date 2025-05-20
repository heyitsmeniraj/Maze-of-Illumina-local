using UnityEngine;
using TMPro;
using System.Collections;

public class NotificationText : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float displayDuration = 3.0f;

    private Coroutine clearCoroutine;

    private NavMeshAvoidance playerbehaviourscript;
    
    public float inactiveTime = 10f;
    public float inactiveTimer = 0f;

    private void Start()
    {
        playerbehaviourscript = FindFirstObjectByType<NavMeshAvoidance>();
    }

    void Update()
    {
        // Check if the player is inactive then add more time to the timer
        if (playerbehaviourscript.canMove && playerbehaviourscript.GetCurrentSpeed() == 0)
            inactiveTimer += Time.deltaTime;

        // if any button is pressed then reset the timer, only if the player was not moving yet   
        if (!playerbehaviourscript.canMove && Input.anyKey)
        {
            inactiveTimer = 0f;
        }

        // check if player is ianactive and restart the level if so
        failedlevel();
    }

    public void ShowMessage(string message)
    {
        if (clearCoroutine != null)
        {
            StopCoroutine(clearCoroutine);
        }

        textMeshPro.text = message;
        textMeshPro.gameObject.SetActive(true);
        
        clearCoroutine = StartCoroutine(ClearAfterDelay());
    }

    private IEnumerator ClearAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        textMeshPro.text = "";
        textMeshPro.gameObject.SetActive(false);
    }

    public void failedlevel()
    {
        // Check if the player is in the moving state
        if (playerbehaviourscript.canMove == true)
        {
            // check if the player is inactive i.e. not maving and it has been ten seconds 
            if (inactiveTimer >= (inactiveTime - displayDuration) && playerbehaviourscript.GetCurrentSpeed() == 0)
            {
                //show message and restart the level with the current delay
                ShowMessage("You are inactive or stuck, Restarting level...");
                StartCoroutine(LoadSceneWithDelay());
            }
        }
    }

    IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        UnityEngine.SceneManagement.SceneManager.LoadScene(
                    UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
