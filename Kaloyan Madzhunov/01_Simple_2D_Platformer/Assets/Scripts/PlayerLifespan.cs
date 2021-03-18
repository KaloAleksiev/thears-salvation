using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifespan : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Bandit") || collision.gameObject.CompareTag("Elemental")) {
            Destroy(gameObject);
            LevelManager.instance.Respawn();
        }
    }
}
