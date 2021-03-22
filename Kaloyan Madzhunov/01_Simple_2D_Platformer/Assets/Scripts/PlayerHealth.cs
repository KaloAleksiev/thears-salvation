using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Bandit")) {
            LevelManager.instance.TakeDamage(25);
        } else if (collision.gameObject.CompareTag("Elemental")) {
            LevelManager.instance.TakeDamage(50);
        }

        if (LevelManager.instance.currentHealth <= 0) {
            Destroy(gameObject);
            LevelManager.instance.Respawn();
        }
    }
}
