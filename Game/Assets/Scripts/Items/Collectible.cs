﻿using UnityEngine;

public class Collectible : MonoBehaviour {
    public SoulOrb soulOrb;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound)
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = sound;
        audioSource.Play();
    }
}
