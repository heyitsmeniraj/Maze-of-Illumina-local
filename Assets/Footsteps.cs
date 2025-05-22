 using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class Footsteps : MonoBehaviour
{
    public AudioClip[] footstepSounds;
    private AudioSource audioSource;
    private Animator animator;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // use aniamtion speed to determine movement and the walking/running speeds
        float speed = animator.GetFloat("Speed");
        // are we moving? Is there a sound source?
        if (speed > 0.1f && audioSource != null)
        {
            //pick a clip at random
            AudioClip clip = footstepSounds[Random.Range(0, footstepSounds.Length)];

            // am i already playing a footstep?
            if (!audioSource.isPlaying)
            {
                // are we running?
                if (speed > 2.1f)
                    audioSource.pitch = 1f;
                // we are walking then
                else audioSource.pitch = .6f;

                audioSource.clip = clip;
                audioSource.Play();
            }
        }
    }

    // // Called by animation event
    // public void PlayFootstep()
    // {
    //     if (footstepSounds.Length == 0) return;

    //     AudioClip clip = footstepSounds[Random.Range(0, footstepSounds.Length)];
    //     audioSource.PlayOneShot(clip);

    //     Debug.Log("Footstep event triggered");
    // }
}