using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePicker : MonoBehaviour {
    public int worth = 1;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Destroy(gameObject);
            PlayerManager.instance.IncreaseSoulOrbs(worth);
        }
    }
}
