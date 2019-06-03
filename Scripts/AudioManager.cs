using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("AudioManager is NULL!!");
            }

            return _instance;
        }
    }

    public AudioSource voiceOverAudioSource;
    public AudioSource musicAudioSource;

    private void Awake()
    {
        _instance = this;
    }

    public void PlayVoiceOver(AudioClip clipToPlay)
    {
        voiceOverAudioSource.clip = clipToPlay;
        voiceOverAudioSource.Play();
    }

    public void PlayBackgroundMusic()
    {
        musicAudioSource.Play();    
    }



}
