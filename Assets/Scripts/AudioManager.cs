using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    // [Header(“-----------Audio Source --------------”)]
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource SFXSource;

    // [Header(“-----------Audio Clip --------------”)]
    // public AudioClip music;
    public AudioClip jump;
    public AudioClip breakbox;
    public AudioClip pickup;
    public AudioClip sword;
    public AudioClip coin;
    public AudioClip dollmove;
    public AudioClip spidermove;
    public AudioClip foxmove;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // musicSource.clip = music;
        // musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}

