using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    public int scoreCount = 0;
    public TMPro.TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        //var exsiting = FindObjectsByType<ScoreHandler>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        //foreach (ScoreHandler score in exsiting)
        //{
        //    if (score != this)
        //        Destroy(score.gameObject);
        //}
        //DontDestroyOnLoad(gameObject);
        //DontDestroyOnLoad(scoreText.transform.parent.gameObject);
        //print(scoreText.transform.parent.gameObject.name);

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = scoreCount.ToString();
    }
}
