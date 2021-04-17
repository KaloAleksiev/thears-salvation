using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAudio : MonoBehaviour {
    public Player player;

    private float m_WalkPitch;
    private float m_JumpPitch;
    private float m_LandPitch;
    private float m_PainPitch;
    private float m_DeathPitch;
    private float m_SpawnPitch;

    private AudioSource asFootsteps;
    private AudioSource asJump;
    private AudioSource asLand;
    private AudioSource asPain;
    private AudioSource asDeath;
    private AudioSource asSpawn;
    private AudioSource asSwing;
    private AudioSource asHit;
    private AudioSource asEffect;
    private AudioSource asPickUp;
    private AudioSource m_AudioSource;
    private AudioSource[] audioSources;

    // Use this for initialization
    private void Start() {
        audioSources = GetComponents<AudioSource>();
        asFootsteps = audioSources[0];
        asJump = audioSources[1];
        asLand = audioSources[2];
        asPain = audioSources[3];
        asDeath = audioSources[4];
        asSpawn = audioSources[5];
        asSwing = audioSources[6];
        asHit = audioSources[7];
        asEffect = audioSources[8];
        asPickUp = audioSources[9];
        m_AudioSource = audioSources[10];

        m_WalkPitch = Random.Range(0.5f, 1.5f);
        m_JumpPitch = Random.Range(0.5f, 1.5f);
        m_LandPitch = Random.Range(0.25f, 1.25f);
        m_PainPitch = Random.Range(0.5f, 1.5f);
        m_DeathPitch = Random.Range(0.5f, 1.5f);
        m_SpawnPitch = Random.Range(0.5f, 1.5f);

        player.playFootstepSound.AddListener(PlayFootStepAudio);
        player.playJumpSound.AddListener(() => PlayRandomSoundOf(player.playerData.JumpSounds, asJump));
        player.playLandingSound.AddListener(() => PlayRandomSoundOf(player.playerData.LandSounds, asLand));
        player.playPainSound.AddListener(() => PlayRandomSoundOf(player.playerData.PainSounds, asPain));
        player.playDeathSound.AddListener(() => PlayRandomSoundOf(player.playerData.DeathSounds, asDeath));
        player.playSpawnSound.AddListener(() => PlayRandomSoundOf(player.playerData.SpawnSounds, asSpawn));

        player.playSwordSwingSound.AddListener(() => PlayRandomSoundOf(player.playerData.ActiveSword.SwingSounds, asSwing));
        player.playSwordHitSound.AddListener(() => PlayRandomSoundOf(player.playerData.ActiveSword.HitSounds, asHit));
        player.playSpecialEffectSound.AddListener(() => PlayRandomSoundOf(player.playerData.ActiveSword.SpecialEffectSounds, asEffect));
        player.playSwordPickUpSound.AddListener(() => PlayRandomSoundOf(player.playerData.ActiveSword.PickUpSounds, asPickUp));
    }

    private void OnDestroy() {
        player.playFootstepSound.RemoveListener(PlayFootStepAudio);
        player.playJumpSound.RemoveListener(() => PlayRandomSoundOf(player.playerData.JumpSounds, asJump));
        player.playLandingSound.RemoveListener(() => PlayRandomSoundOf(player.playerData.LandSounds, asLand));
        player.playPainSound.RemoveListener(() => PlayRandomSoundOf(player.playerData.PainSounds, asPain));
        player.playDeathSound.RemoveListener(() => PlayRandomSoundOf(player.playerData.DeathSounds, asDeath));
        player.playSpawnSound.RemoveListener(() => PlayRandomSoundOf(player.playerData.SpawnSounds, asSpawn));

        player.playSwordSwingSound.RemoveListener(() => PlayRandomSoundOf(player.playerData.ActiveSword.SwingSounds, asSwing));
        player.playSwordHitSound.RemoveListener(() => PlayRandomSoundOf(player.playerData.ActiveSword.HitSounds, asHit));
        player.playSpecialEffectSound.AddListener(() => PlayRandomSoundOf(player.playerData.ActiveSword.SpecialEffectSounds, asEffect));
        player.playSwordPickUpSound.AddListener(() => PlayRandomSoundOf(player.playerData.ActiveSword.PickUpSounds, asPickUp));
    }

    private void PlayRandomSoundOf(AudioClip[] soundsArray, AudioSource audioSource) {
        int index = Random.Range(0, soundsArray.Length);
        audioSource.clip = soundsArray[index];
        audioSource.Play();
    }

    private void PlayFootStepAudio() {
        // pick & play a random footstep sound from the array,
        // excluding sound at index 0
        int n = Random.Range(1, player.playerData.FootstepSounds.Length);
        asFootsteps.clip = player.playerData.FootstepSounds[n];
        //m_AudioSource.pitch = m_WalkPitch;

        asFootsteps.PlayOneShot(asFootsteps.clip);

        m_WalkPitch = Random.Range(0.5f, 1.5f);
        // move picked sound to index 0 so it's not picked next time
        player.playerData.FootstepSounds[n] = player.playerData.FootstepSounds[0];
        player.playerData.FootstepSounds[0] = asFootsteps.clip;
    }

    private void PlayJumpSound() {
        int index = Random.Range(0, player.playerData.JumpSounds.Length);
        asJump.clip = player.playerData.JumpSounds[index];
        //m_AudioSource.pitch = m_JumpPitch;

        asJump.Play();

        m_JumpPitch = Random.Range(0.5f, 1.5f);
    }

    private void PlayLandingSound() {
        int index = Random.Range(0, player.playerData.LandSounds.Length);
        asLand.clip = player.playerData.LandSounds[index];
        //m_AudioSource.pitch = m_LandPitch;

        asLand.Play();

        m_LandPitch = Random.Range(0.25f, 1.25f);
    }

    private void PlayPainSound() {
        int index = Random.Range(0, player.playerData.PainSounds.Length);
        asPain.clip = player.playerData.PainSounds[index];
        asPain.pitch = m_PainPitch;

        asPain.Play();

        m_PainPitch = Random.Range(0.5f, 1.5f);
    }

    private void PlayDeathSound() {
        int index = Random.Range(0, player.playerData.DeathSounds.Length);
        asDeath.clip = player.playerData.DeathSounds[index];
        asDeath.pitch = m_DeathPitch;

        asDeath.Play();

        m_DeathPitch = Random.Range(0.5f, 1.5f);
    }

    private void PlaySpawnSound() {
        int index = Random.Range(0, player.playerData.SpawnSounds.Length);
        asSpawn.clip = player.playerData.SpawnSounds[index];
        asSpawn.pitch = m_SpawnPitch;

        asSpawn.Play();

        m_SpawnPitch = Random.Range(0.5f, 1.5f);
    }
}