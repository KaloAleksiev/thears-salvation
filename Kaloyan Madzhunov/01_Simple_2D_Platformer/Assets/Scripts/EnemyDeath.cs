using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public int deathClicks = 1;
    public int clicksCounter = 0;

    private void OnMouseDown() {
        if (++clicksCounter == deathClicks) {
            Destroy(gameObject);
        }
    }
}
