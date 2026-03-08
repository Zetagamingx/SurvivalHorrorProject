using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public SoundData[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSources;

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }    
    }

    public void PlayMusic(string name)
    {
        SoundData musicSound = Array.Find(musicSounds, x => x.name == name);

        if (musicSound == null)
        {
            Debug.Log("Music clip not Found");
        }

        else
        {
            musicSource.clip = musicSound.clip;
            musicSource.Play();
        }
    }

    public void PlaySfx(string name)
    {
        SoundData sfxSound = Array.Find(sfxSounds, x => x.name == name);

        if (sfxSound == null)
        {
            Debug.Log("Sfx clip not Found");
        }

        else
        {            
            musicSource.PlayOneShot(sfxSound.clip);
        }
    }
}
