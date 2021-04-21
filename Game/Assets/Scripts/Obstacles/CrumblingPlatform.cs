using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CrumblingPlatform : MonoBehaviour {
    public float resetTime = 2;
    public float disappearTime = 1;
    AudioSource asTrigger;
    AudioSource asDisappear;
    AudioSource asAppear;
    AudioSource[] audioSources;

    private void Start()
    {
        audioSources = GetComponents<AudioSource>();
        asTrigger = audioSources[0];
        asDisappear = audioSources[1];
        asAppear = audioSources[2];
    }

    public void StartTimer()
    {
        StartCoroutine("StartIdleBeforeDisappear");
    }

    public IEnumerator StartIdleBeforeDisappear() {
        asTrigger.Play();
        yield return new WaitForSeconds(disappearTime);

        yield return StartCoroutine(BreakPlatform(gameObject));
    }

    private IEnumerator BreakPlatform(GameObject platform) 
    {
        asDisappear.Play();
        platform.GetComponent<TilemapCollider2D>().enabled = false;
        platform.GetComponent<TilemapRenderer>().enabled = false;

        yield return new WaitForSeconds(resetTime);

        asAppear.Play();
        platform.GetComponent<TilemapCollider2D>().enabled = true;
        platform.GetComponent<TilemapRenderer>().enabled = true;
    }
}
