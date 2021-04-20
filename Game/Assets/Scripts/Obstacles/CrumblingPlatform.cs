using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CrumblingPlatform : MonoBehaviour {
    public float resetTime = 2;
    public float disappearTime = 1;

    public void StartTimer()
    {
        StartCoroutine("StartIdleBeforeDisappear");
    }

    public IEnumerator StartIdleBeforeDisappear() {
        yield return new WaitForSeconds(disappearTime);

        yield return StartCoroutine(BreakPlatform(gameObject));
    }

    private IEnumerator BreakPlatform(GameObject platform) 
    {
        platform.GetComponent<TilemapCollider2D>().enabled = false;
        platform.GetComponent<TilemapRenderer>().enabled = false;

        yield return new WaitForSeconds(resetTime);

        platform.GetComponent<TilemapCollider2D>().enabled = true;
        platform.GetComponent<TilemapRenderer>().enabled = true;
    }
}
