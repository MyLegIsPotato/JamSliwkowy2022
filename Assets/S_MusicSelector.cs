using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MusicSelector : MonoBehaviour
{
    private AudioSource speaker;

    public AudioClip defaultMusic;
    public AudioClip bossMusic;
    public AudioClip caveMusic;

    public void Start()
    {
        speaker = GetComponent<AudioSource>();
        speaker.clip = defaultMusic;
    }

    public void PlayBossMusic()
    {
        speaker.clip = bossMusic;
        speaker.Play();
    }

    public void PlayCaveMusic()
    {
        speaker.clip = caveMusic;
        speaker.Play();
    }

}
