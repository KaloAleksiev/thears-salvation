using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TimedPlatform : MonoBehaviour
{
    public float disabledTime;
    public float enabledTime;

    AudioSource appear;
    AudioSource disappear;
    AudioSource[] audioSources;
    
    void Start()
    {
        StartCoroutine("StayOn");
        audioSources = GetComponents<AudioSource>();
        appear = audioSources[0];
        disappear = audioSources[1];
    }


    public IEnumerator StayOn()
    {
        yield return new WaitForSeconds(enabledTime);
        yield return StartCoroutine("BreakPlatform");
    }

    private IEnumerator BreakPlatform()
    {
        disappear.Play();
        GetComponent<TilemapCollider2D>().enabled = false;
        GetComponent<TilemapRenderer>().enabled = false;

        yield return new WaitForSeconds(disabledTime);

        appear.Play();
        GetComponent<TilemapCollider2D>().enabled = true;
        GetComponent<TilemapRenderer>().enabled = true;

        StartCoroutine("StayOn");
    }
}
