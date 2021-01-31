using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHelper : MonoBehaviour
{
    public static AudioClip GetRandomAudioClip(AudioClip[] audioClips)
    {
        return audioClips[(int)(Random.Range(0f, 10.0f) % audioClips.Length)];
    }
}
