using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    public Enemy enemy;
    private AudioSource asFootsteps;
    private AudioSource asPain;
    private AudioSource asDeath;
    private AudioSource asSwing;
    private AudioSource asHit;
    private AudioSource[] audioSources;
    private float walkPitch;

    // Start is called before the first frame update
    void Start()
    {
        audioSources = GetComponents<AudioSource>();
        asFootsteps = audioSources[0];
        asPain = audioSources[1];
        asDeath = audioSources[2];
        asSwing = audioSources[3];
        asHit = audioSources[4];
        walkPitch = Random.Range(0.5f, 1.5f);

        enemy.playFootstepSound.AddListener(PlayFootstepSound);
        enemy.playPainSound.AddListener(PlayPainSound);
        enemy.playDeathSound.AddListener(PlayDeathSound);
        enemy.playSwingSound.AddListener(PlaySwingSound);
        enemy.playHitSound.AddListener(PlayHitSound);
    }

    private void OnDestroy()
    {
        enemy.playFootstepSound.RemoveListener(PlayFootstepSound);
        enemy.playPainSound.RemoveListener(PlayPainSound);
        enemy.playDeathSound.RemoveListener(PlayDeathSound);
        enemy.playSwingSound.RemoveListener(PlaySwingSound);
        enemy.playHitSound.RemoveListener(PlayHitSound);
    }

    private void PlayFootstepSound()
    {
        // pick & play a random footstep sound from the array,
        // excluding sound at index 0
        int index = Random.Range(1, enemy.enemyData.FootstepSounds.Length);
        asFootsteps.clip = enemy.enemyData.FootstepSounds[index];
        asFootsteps.PlayOneShot(asFootsteps.clip);

        walkPitch = Random.Range(0.5f, 1.5f);
        // move picked sound to index 0 so it's not picked next time
        enemy.enemyData.FootstepSounds[index] = enemy.enemyData.FootstepSounds[0];
        enemy.enemyData.FootstepSounds[0] = asFootsteps.clip;
    }

    private void PlayPainSound()
    {
        int index = Random.Range(0, enemy.enemyData.PainSounds.Length);
        asPain.clip = enemy.enemyData.PainSounds[index];
        asPain.Play();
    }

    private void PlayDeathSound()
    {
        int index = Random.Range(0, enemy.enemyData.DeathSounds.Length);
        asDeath.clip = enemy.enemyData.DeathSounds[index];
        asDeath.Play();
    }

    private void PlaySwingSound()
    {
        int index = Random.Range(0, enemy.enemyData.SwingSounds.Length);
        asSwing.clip = enemy.enemyData.SwingSounds[index];
        asSwing.Play();
    }

    private void PlayHitSound()
    {
        int index = Random.Range(0, enemy.enemyData.HitSounds.Length);
        asHit.clip = enemy.enemyData.HitSounds[index];
        asHit.Play();
    }
}
