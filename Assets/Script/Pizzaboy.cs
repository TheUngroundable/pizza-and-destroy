using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizzaboy : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] waiting;
    public AudioClip[] startedGame;

    public bool isPizzaboyWaiting;
    public bool gameStarted;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isPizzaboyWaiting = true;
        gameStarted = false;
        InvokeRepeating("PlayPizzaboyWaiting", 0.1f, 2f);
    }

    void Update()
    {
        if (!isPizzaboyWaiting)
        {
            CancelInvoke("PlayPizzaboyWaiting");
        } else if(!gameStarted){
            PlayPizzaboyStartGame();
            gameStarted = true;
        }
    }

    void PlayPizzaboyStartGame(){
        AudioClip audioClip = AudioHelper.GetRandomAudioClip(waiting);
        PlaySound(audioClip);
    }

    void PlayPizzaboyWaiting()
    {
        AudioClip audioClip = AudioHelper.GetRandomAudioClip(waiting);
        PlaySound(audioClip);
    }

    void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip, 0.7F);
    }
}
