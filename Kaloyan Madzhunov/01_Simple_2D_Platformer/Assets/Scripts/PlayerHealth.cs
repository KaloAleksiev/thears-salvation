using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthManager {
    public PlayerHealth() : base(Constants.PLAYER_HP) { }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Bandit")) {
            TakeDamage(Constants.BANDIT_DMG);
        } else if (collision.gameObject.CompareTag("Elemental")) {
            TakeDamage(Constants.ELEMENTAL_DMG);
        }

        if (currentHealth <= 0) {
            ResetHealthBar();
            Destroy(gameObject);
            LevelManager.instance.Respawn();
        }
    }
}
