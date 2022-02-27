using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEAudioSource : MonoBehaviour
{
    [SerializeField] private AudioClip spaceGun;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio()
    {
        audioSource.PlayOneShot(spaceGun);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
