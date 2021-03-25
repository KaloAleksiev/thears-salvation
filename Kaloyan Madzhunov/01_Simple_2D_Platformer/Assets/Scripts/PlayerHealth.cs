using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public Health health;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Bandit")) {
            health.TakeDamage(Constants.BANDIT_DMG);
        } else if (collision.gameObject.CompareTag("Elemental")) {
            health.TakeDamage(Constants.ELEMENTAL_DMG);
        }

        if (health.currentHealth == 0) {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die() {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1);
        LevelManager.instance.Respawn(gameObject);
        health.ResetHealth();
    }
}
