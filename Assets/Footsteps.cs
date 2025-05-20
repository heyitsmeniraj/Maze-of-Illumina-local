 using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioClip[] footstepSounds;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Called by animation event
    public void PlayFootstep()
    {
        if (footstepSounds.Length == 0) return;

        AudioClip clip = footstepSounds[Random.Range(0, footstepSounds.Length)];
        audioSource.PlayOneShot(clip);

        Debug.Log("Footstep event triggered");
    }
}