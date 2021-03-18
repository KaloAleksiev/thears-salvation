using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour {
    public int deathClicks = 1;
    public int clicksCounter = 0;
    public GameObject soulOrbPrefab;

    private void OnMouseDown() {
        if (++clicksCounter == deathClicks) {
            if (gameObject.CompareTag("Elemental")) {
                Instantiate(soulOrbPrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
