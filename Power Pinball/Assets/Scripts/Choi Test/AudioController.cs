using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioClips
{
    SpaceGun,
    Footsteps,
    Impact
}

public class AudioController : MonoBehaviour
{
    // All the SFX clips to be used.
    [SerializeField] private AudioClip spaceGun;
    [SerializeField] private AudioClip footsteps;
    [SerializeField] private AudioClip impact;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Plays the specified audio clip, handling each appropriately based on 
    /// the type.
    /// </summary>
    /// <param name="audioClip">
    /// The audio clip to play.
    /// </param>
    public void PlayAudio(AudioClips audioClip)
    {
        switch (audioClip)
        {
            case AudioClips.SpaceGun:
                audioSource.PlayOneShot(spaceGun, 0.25f);
                break;
            // Footsteps audio should only play when the character is moving.
            // Use Play() because then Stop() can be called, without affecting
            // other SFX clips played using PlayOneShot().
            case AudioClips.Footsteps:
                audioSource.clip = footsteps;
                audioSource.volume = 1;
                if (!audioSource.isPlaying)
                    audioSource.Play();
                break;
            case AudioClips.Impact:
                audioSource.PlayOneShot(impact);
                break;
        }
    }

    /// <summary>
    /// Specifically used to stop the footsteps SFX.
    /// </summary>
    public void StopAudio()
    {
        audioSource.Stop();
    }
}
