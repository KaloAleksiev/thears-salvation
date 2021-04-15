using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    public Enemy enemy;
    private AudioSource audioSource;
    private float walkPitch;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        audioSource.clip = enemy.enemyData.FootstepSounds[index];
        audioSource.PlayOneShot(audioSource.clip);

        walkPitch = Random.Range(0.5f, 1.5f);
        // move picked sound to index 0 so it's not picked next time
        enemy.enemyData.FootstepSounds[index] = enemy.enemyData.FootstepSounds[0];
        enemy.enemyData.FootstepSounds[0] = audioSource.clip;
    }

    private void PlayPainSound()
    {
        int index = Random.Range(0, enemy.enemyData.PainSounds.Length);
        audioSource.clip = enemy.enemyData.PainSounds[index];
        audioSource.Play();
    }

    private void PlayDeathSound()
    {
        int index = Random.Range(0, enemy.enemyData.DeathSounds.Length);
        audioSource.clip = enemy.enemyData.DeathSounds[index];
        audioSource.Play();
    }

    private void PlaySwingSound()
    {
        int index = Random.Range(0, enemy.enemyData.SwingSounds.Length);
        audioSource.clip = enemy.enemyData.SwingSounds[index];
        audioSource.Play();
    }

    private void PlayHitSound()
    {
        int index = Random.Range(0, enemy.enemyData.HitSounds.Length);
        audioSource.clip = enemy.enemyData.HitSounds[index];
        audioSource.Play();
    }
}
