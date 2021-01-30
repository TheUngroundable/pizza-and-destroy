using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip closeSound;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayOpenSound()
    {
        audioSource.PlayOneShot(openSound, 0.5f);
    }
    public void PlayCloseSound()
    {
        audioSource.PlayOneShot(closeSound, 0.5f);
    }
}
