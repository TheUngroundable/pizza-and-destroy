using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizzaboy : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] waiting;

    public bool isPizzaboyWaiting;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isPizzaboyWaiting = true;
        InvokeRepeating("PlayPizzaboyWaiting", 0.1f, 2f);
    }

    void Update()
    {
        if (!isPizzaboyWaiting)
        {
            CancelInvoke("PlayPizzaboyWaiting");
        }
    }

    void PlayPizzaboyWaiting()
    {
        AudioClip audioClip = AudioHelper.GetRandomAudioClip(waiting);
        PlaySound(audioClip);
    }

    void PlaySound(AudioClip audioClip)
    {
        Debug.Log(audioClip);
        audioSource.PlayOneShot(audioClip, 0.7F);
    }
}
