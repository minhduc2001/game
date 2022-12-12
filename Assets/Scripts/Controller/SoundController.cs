using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    public SoundMode[] sounds;
   
    public AudioMixerGroup mixerGroupMusic;
    public AudioMixerGroup mixerGroupSFX;

    private void Awake()
    {
        foreach(SoundMode sound in sounds )
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            sound.source = audioSource;
            if (sound.name == "Theme")
            {
                sound.source.outputAudioMixerGroup = mixerGroupMusic;
            } else
            {
                sound.source.outputAudioMixerGroup = mixerGroupSFX;
            }
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Play("Theme");
    }

    public void Play(string name)
    {
        SoundMode sound = Array.Find<SoundMode>(sounds, s => s.name == name);

        sound.source.Play();
    }

    public void Pause(string name)
    {
        SoundMode sound = Array.Find<SoundMode>(sounds, s => s.name == name);

        sound.source.Pause();
    }


}
