using UnityEngine;

public class SeekableObjectr : MonoBehaviour
{
    public int priority = 1; // Higher values mean higher priority
    public bool visited = false;

    private void OnTriggerEnter(Collider other)
    {
        if ( other.tag == "Player")
        {
            visited = true;
        }
    }
}

