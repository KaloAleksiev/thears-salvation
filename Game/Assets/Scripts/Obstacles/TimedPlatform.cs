using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TimedPlatform : MonoBehaviour
{
    public float disabledTime;
    public float enabledTime;
    public Player player;
    
    void Start()
    {
        StartCoroutine("StayOn");
    }

    public IEnumerator StayOn()
    {
        yield return new WaitForSeconds(enabledTime);

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
