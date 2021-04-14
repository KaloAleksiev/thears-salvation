using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAudio : MonoBehaviour {
    public Player player;

    [SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
    [SerializeField] private AudioClip[] m_JumpSounds;           // the sound played when character leaves the ground.
    [SerializeField] private AudioClip[] m_LandSounds;           // the sound played when character touches back on ground.
    [SerializeField] private AudioClip[] m_PainSounds;           // the sound played when character is hurt.
    [SerializeField] private AudioClip m_DeathSound;           // the sound played when character dies.
    [SerializeField] private AudioClip[] m_SpawnSounds;           // the sound played when character respawns.

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
        /*m_PainPitch = Random.Range(0.5f, 1.5f);
        m_DeathPitch = Random.Range(0.5f, 1.5f);
        m_SpawnPitch = Random.Range(0.5f, 1.5f);*/

        player.playJumpSound.AddListener(PlayJumpSound);
        player.playLandingSound.AddListener(PlayLandingSound);
        player.playFootstepSound.AddListener(PlayFootStepAudio);
        player.playPainSound.AddListener(PlayPainSound);
        player.playDeathSound.AddListener(PlayDeathSound);
        player.playSpawnSound.AddListener(PlaySpawnSound);
    }

    private void OnDestroy() {
        player.playJumpSound.RemoveListener(PlayJumpSound);
        player.playLandingSound.RemoveListener(PlayLandingSound);
        player.playFootstepSound.RemoveListener(PlayFootStepAudio);
        player.playPainSound.RemoveListener(PlayPainSound);
        player.playDeathSound.RemoveListener(PlayDeathSound);
        player.playSpawnSound.RemoveListener(PlaySpawnSound);
    }

    private void PlayJumpSound() {
        int index = Random.Range(0, m_JumpSounds.Length);
        m_AudioSource.clip = m_JumpSounds[index];
        //m_AudioSource.pitch = m_JumpPitch;

        m_AudioSource.Play();

        m_JumpPitch = Random.Range(0.5f, 1.5f);
    }

    private void PlayLandingSound(float m_StepCycle, float m_NextStep) {
        int index = Random.Range(0, m_LandSounds.Length);
        m_AudioSource.clip = m_LandSounds[index];
        //m_AudioSource.pitch = m_LandPitch;

        m_AudioSource.Play();

        m_LandPitch = Random.Range(0.25f, 1.25f);
        m_NextStep = m_StepCycle + .5f;
    }

    private void PlayFootStepAudio() {
        // pick & play a random footstep sound from the array,
        // excluding sound at index 0
        int n = Random.Range(1, m_FootstepSounds.Length);
        m_AudioSource.clip = m_FootstepSounds[n];
        //m_AudioSource.pitch = m_WalkPitch;

        m_AudioSource.PlayOneShot(m_AudioSource.clip);

        m_WalkPitch = Random.Range(0.5f, 1.5f);
        // move picked sound to index 0 so it's not picked next time
        m_FootstepSounds[n] = m_FootstepSounds[0];
        m_FootstepSounds[0] = m_AudioSource.clip;
    }

    private void PlayPainSound() {
        int index = Random.Range(0, m_PainSounds.Length);
        m_AudioSource.clip = m_PainSounds[index];
        //m_AudioSource.pitch = m_PainPitch;

        m_AudioSource.Play();

        //m_PainPitch = Random.Range(0.5f, 1.5f);
    }

    private void PlayDeathSound() {
        m_AudioSource.clip = m_DeathSound;
        //m_AudioSource.pitch = m_DeathPitch;

        m_AudioSource.Play();

        //m_DeathPitch = Random.Range(0.5f, 1.5f);
    }

    private void PlaySpawnSound() {
        int index = Random.Range(0, m_SpawnSounds.Length);
        m_AudioSource.clip = m_SpawnSounds[index];
        //m_AudioSource.pitch = m_SpawnPitch;

        m_AudioSource.Play();

        //m_SpawnPitch = Random.Range(0.5f, 1.5f);
    }
}