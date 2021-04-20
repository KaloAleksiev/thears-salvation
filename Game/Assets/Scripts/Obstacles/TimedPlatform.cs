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

    private float passedTime = 0;
    private bool crRunning = false;
    public Stopwatch sw = new Stopwatch();

    public float PassedTime { get => passedTime; set => passedTime = value; }
    
    void Start()
    {
        StartCoroutine("StayOn");
    }

    void Update()
    {
        if (crRunning)
        {
            PassedTime = (float)sw.Elapsed.TotalSeconds;
        }
    }

    public IEnumerator StayOn()
    {
        sw.Start();
        crRunning = true;
        yield return new WaitForSeconds(enabledTime);

        sw.Stop();
        crRunning = false;
        PassedTime = 0;
        yield return StartCoroutine("BreakPlatform");
    }

    private IEnumerator BreakPlatform()
    {
        GetComponent<TilemapCollider2D>().enabled = false;
        GetComponent<TilemapRenderer>().enabled = false;

        yield return new WaitForSeconds(disabledTime);

        GetComponent<TilemapCollider2D>().enabled = true;
        GetComponent<TilemapRenderer>().enabled = true;

        StartCoroutine("StayOn");
    }
}
