using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAudio : MonoBehaviour {
    public Player player;

    private float m_WalkPitch;
    private float m_JumpPitch;
    private float m_LandPitch;

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

        m_WalkPitch = Random.Range(0.8f, 1.2f);
        m_JumpPitch = Random.Range(0.8f, 1.2f);
        m_LandPitch = Random.Range(0.8f, 1.2f);

        player.playFootstepSound.AddListener(PlayFootStepAudio);
        player.playJumpSound.AddListener(PlayJumpSound);
        player.playLandingSound.AddListener(PlayLandingSound);

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
        player.playJumpSound.RemoveListener(PlayJumpSound);
        player.playLandingSound.RemoveListener(PlayLandingSound);

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
        asFootsteps.pitch = m_WalkPitch;

        asFootsteps.PlayOneShot(asFootsteps.clip);

        m_WalkPitch = Random.Range(0.8f, 1.2f);
        // move picked sound to index 0 so it's not picked next time
        player.playerData.FootstepSounds[n] = player.playerData.FootstepSounds[0];
        player.playerData.FootstepSounds[0] = asFootsteps.clip;
    }

    private void PlayJumpSound() {
        int index = Random.Range(0, player.playerData.JumpSounds.Length);
        asJump.clip = player.playerData.JumpSounds[index];
        asJump.pitch = m_JumpPitch;

        asJump.Play();

        m_JumpPitch = Random.Range(0.8f, 1.2f);
    }

    private void PlayLandingSound() {
        int index = Random.Range(0, player.playerData.LandSounds.Length);
        asLand.clip = player.playerData.LandSounds[index];
        asLand.pitch = m_LandPitch;

        asLand.Play();

        m_LandPitch = Random.Range(0.8f, 1.2f);
    }
}