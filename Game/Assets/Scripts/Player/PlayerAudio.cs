using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAudio : MonoBehaviour {
    public Player player;

    private AudioSource m_AudioSource;
    private float m_WalkPitch;
    private float m_JumpPitch;
    private float m_LandPitch;
    private float m_PainPitch;
    private float m_DeathPitch;
    private float m_SpawnPitch;

    // Use this for initialization
    private void Start() {
        m_AudioSource = GetComponent<AudioSource>();
        m_WalkPitch = Random.Range(0.5f, 1.5f);
        m_JumpPitch = Random.Range(0.5f, 1.5f);
        m_LandPitch = Random.Range(0.25f, 1.25f);
        m_PainPitch = Random.Range(0.5f, 1.5f);
        m_DeathPitch = Random.Range(0.5f, 1.5f);
        m_SpawnPitch = Random.Range(0.5f, 1.5f);

        player.playFootstepSound.AddListener(PlayFootStepAudio);
        player.playJumpSound.AddListener(() => PlayRandomSoundOf(player.playerData.JumpSounds));
        player.playLandingSound.AddListener(() => PlayRandomSoundOf(player.playerData.LandSounds));
        player.playPainSound.AddListener(() => PlayRandomSoundOf(player.playerData.PainSounds));
        player.playDeathSound.AddListener(() => PlayRandomSoundOf(player.playerData.DeathSounds));
        player.playSpawnSound.AddListener(() => PlayRandomSoundOf(player.playerData.SpawnSounds));

        player.playSwordSwingSound.AddListener(() => PlayRandomSoundOf(player.playerData.ActiveSword.SwingSounds));
        player.playSwordHitSound.AddListener(() => PlayRandomSoundOf(player.playerData.ActiveSword.HitSounds));
        player.playSpecialEffectSound.AddListener(() => PlayRandomSoundOf(player.playerData.ActiveSword.SpecialEffectSounds));
        player.playSwordPickUpSound.AddListener(() => PlayRandomSoundOf(player.playerData.ActiveSword.PickUpSounds));
    }

    private void OnDestroy() {
        player.playFootstepSound.RemoveListener(PlayFootStepAudio);
        player.playJumpSound.RemoveListener(() => PlayRandomSoundOf(player.playerData.JumpSounds));
        player.playLandingSound.RemoveListener(() => PlayRandomSoundOf(player.playerData.LandSounds));
        player.playPainSound.RemoveListener(() => PlayRandomSoundOf(player.playerData.PainSounds));
        player.playDeathSound.RemoveListener(() => PlayRandomSoundOf(player.playerData.DeathSounds));
        player.playSpawnSound.RemoveListener(() => PlayRandomSoundOf(player.playerData.SpawnSounds));

        player.playSwordSwingSound.RemoveListener(() => PlayRandomSoundOf(player.playerData.ActiveSword.SwingSounds));
        player.playSwordHitSound.RemoveListener(() => PlayRandomSoundOf(player.playerData.ActiveSword.HitSounds));
        player.playSpecialEffectSound.AddListener(() => PlayRandomSoundOf(player.playerData.ActiveSword.SpecialEffectSounds));
        player.playSwordPickUpSound.AddListener(() => PlayRandomSoundOf(player.playerData.ActiveSword.PickUpSounds));
    }

    private void PlayRandomSoundOf(AudioClip[] soundsArray) {
        int index = Random.Range(0, soundsArray.Length);
        m_AudioSource.clip = soundsArray[index];

        m_AudioSource.Play();
    }

    private void PlayFootStepAudio() {
        // pick & play a random footstep sound from the array,
        // excluding sound at index 0
        int n = Random.Range(1, player.playerData.FootstepSounds.Length);
        m_AudioSource.clip = player.playerData.FootstepSounds[n];
        //m_AudioSource.pitch = m_WalkPitch;

        m_AudioSource.PlayOneShot(m_AudioSource.clip);

        m_WalkPitch = Random.Range(0.5f, 1.5f);
        // move picked sound to index 0 so it's not picked next time
        player.playerData.FootstepSounds[n] = player.playerData.FootstepSounds[0];
        player.playerData.FootstepSounds[0] = m_AudioSource.clip;
    }

    private void PlayJumpSound() {
        int index = Random.Range(0, player.playerData.JumpSounds.Length);
        m_AudioSource.clip = player.playerData.JumpSounds[index];
        //m_AudioSource.pitch = m_JumpPitch;

        m_AudioSource.Play();

        m_JumpPitch = Random.Range(0.5f, 1.5f);
    }

    private void PlayLandingSound() {
        int index = Random.Range(0, player.playerData.LandSounds.Length);
        m_AudioSource.clip = player.playerData.LandSounds[index];
        //m_AudioSource.pitch = m_LandPitch;

        m_AudioSource.Play();

        m_LandPitch = Random.Range(0.25f, 1.25f);
    }

    private void PlayPainSound() {
        int index = Random.Range(0, player.playerData.PainSounds.Length);
        m_AudioSource.clip = player.playerData.PainSounds[index];
        m_AudioSource.pitch = m_PainPitch;

        m_AudioSource.Play();

        m_PainPitch = Random.Range(0.5f, 1.5f);
    }

    private void PlayDeathSound() {
        int index = Random.Range(0, player.playerData.DeathSounds.Length);
        m_AudioSource.clip = player.playerData.DeathSounds[index];
        m_AudioSource.pitch = m_DeathPitch;

        m_AudioSource.Play();

        m_DeathPitch = Random.Range(0.5f, 1.5f);
    }

    private void PlaySpawnSound() {
        int index = Random.Range(0, player.playerData.SpawnSounds.Length);
        m_AudioSource.clip = player.playerData.SpawnSounds[index];
        m_AudioSource.pitch = m_SpawnPitch;

        m_AudioSource.Play();

        m_SpawnPitch = Random.Range(0.5f, 1.5f);
    }
}