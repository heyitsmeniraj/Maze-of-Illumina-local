using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class QuickAccessMenu : MonoBehaviour
{
    public GameObject restartUI;
    public GameObject quitUI;

    private bool isMenuActive = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isMenuActive = !isMenuActive)
            {
                restartUI.SetActive(true);
                quitUI.SetActive(true);
            }
            else
            {
                restartUI.SetActive(false);
                quitUI.SetActive(false);
            }
        }
    }
}
