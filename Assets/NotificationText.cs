using UnityEngine;
using TMPro;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class NotificationText : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float displayDuration = 3.0f;

    private Coroutine clearCoroutine;

    private NavMeshAgent agent;
    public string currentlevel;
    private NavMeshAvoidance playerbehaviourscript;
    Animator animator;
    

    private void Start()
    {
        animator = GetComponent<Animator>();
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

    private System.Collections.IEnumerator ClearAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        textMeshPro.text = "";
        textMeshPro.gameObject.SetActive(false);
    }

    public void failedlevel()
    {
        Vector3 velocity = agent.velocity;
        float agentvelocity = agent.velocity.magnitude;
        float speed = animator.GetFloat("Speed");

        if (animator.speed <= 0)
        {
            if (playerbehaviourscript.canMove == true)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(currentlevel);
            }
        }
    }
}
