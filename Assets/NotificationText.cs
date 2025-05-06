using UnityEngine;
using TMPro;
public class NotificationText : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float displayDuration = 3.0f;

    private Coroutine clearCoroutine;

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
}
