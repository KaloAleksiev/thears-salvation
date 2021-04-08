using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePicker : MonoBehaviour {
    public int worth = 1;

    private bool isDestroyed = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<Player>() && !isDestroyed) {
            Destroy(gameObject);
            isDestroyed = true;
            PlayerManager.instance.player.addSoulOrbs.Invoke(worth);
        }
    }
}
