using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public Health health;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Bandit")) {
            Damage(Constants.BANDIT_DMG);
        } else if (collision.gameObject.CompareTag("Elemental")) {
            Damage(Constants.ELEMENTAL_DMG);
        }        
    }

    public void Damage(int damage) {
        health.TakeDamage(damage);

        if (health.currentHealth == 0) {
            Die();
        }
    }

    private void Die() {
        StartCoroutine(PlayerManager.instance.Respawn());
    }
}
