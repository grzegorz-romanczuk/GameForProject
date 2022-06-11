using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundtrack : MonoBehaviour
{
    public List<AudioClip> musicClips;
    private AudioSource audioSource;
    private int currentMusic = 0;
    private float nextMusic = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentMusic = Random.Range(0, musicClips.Count - 1);
        audioSource.clip = musicClips[currentMusic];
        nextMusic = Time.time + audioSource.clip.length + 2f;
        audioSource.Play();

    }

    private void Update()
    {
        if(Time.time > nextMusic)
        {
            currentMusic++;
            if (currentMusic == musicClips.Count) currentMusic = 0;
            audioSource.clip = musicClips[currentMusic];
            nextMusic = Time.time + audioSource.clip.length + 2f;
            audioSource.Play();
        }
    }


}
