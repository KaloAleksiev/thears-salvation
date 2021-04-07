using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblingPlatform : MonoBehaviour {
    /*private void Update() {
        StartCoroutine("StartIdleBeforeDisappear");
    }

    IEnumerator StartIdleBeforeDisappear() {
        for (float elapsedTime = 0f; elapsedTime < 2; elapsedTime += 1) {
            yield return new WaitForSeconds(1);
        }

        yield return StartCoroutine(BreakPlatform(gameObject, 2f));
    }

    private IEnumerator BreakPlatform(GameObject platform, float resetTime) {
        platform.GetComponent<BoxCollider2D>().enabled = false;
        platform.GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(resetTime);

        platform.GetComponent<BoxCollider2D>().enabled = true;
        platform.GetComponent<SpriteRenderer>().enabled = true;
    }*/
}
