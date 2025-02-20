using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject scoreHandlerPrefab;
    [SerializeField] GameObject scoreCanvasPrefab;
    GameObject scoreCanvas;
    ScoreHandler scoreHandler;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var canvi = GameObject.FindGameObjectsWithTag("ScoreCanvas");
        if (canvi.Length > 1)
        {
            foreach (GameObject canv in canvi)
            {
                Destroy(canv.gameObject);
                scoreCanvas = Instantiate(scoreCanvasPrefab);

            }
        }
        else if (canvi.Length == 0)
        {
            scoreCanvas = Instantiate(scoreCanvasPrefab);
        }
        else if (canvi.Length == 1)
        {
            scoreCanvas = canvi[0];

        }
        var exsiting = FindObjectsByType<ScoreHandler>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        if (exsiting.Length > 1)
        {
            foreach (ScoreHandler score in exsiting)
            {
                Destroy(score.gameObject);
            }
        }
        else if (exsiting.Length == 0)
        { 
            GameObject go = Instantiate(scoreHandlerPrefab);
            scoreHandler = go.GetComponent<ScoreHandler>();
            scoreHandler.scoreText = scoreCanvas.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        }
        else if (canvi.Length == 1)
        {
            scoreHandler = exsiting[0];

        }
        DontDestroyOnLoad(scoreCanvas);
        DontDestroyOnLoad(scoreHandler.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
