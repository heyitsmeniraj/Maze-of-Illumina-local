using UnityEngine;

public class BGM : MonoBehaviour
{
    private static BGM instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps it alive across scenes
        }
        else
        {
            Destroy(gameObject); // Prevents duplicates
        }
    }
}
