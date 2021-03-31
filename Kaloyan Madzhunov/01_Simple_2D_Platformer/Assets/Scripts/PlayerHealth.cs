using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public Player player;
    public Health health;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Bandit")) {
            Damage(Constants.BANDIT_DMG);
        } else if (collision.gameObject.CompareTag("Elemental")) {
            Damage(Constants.ELEMENTAL_DMG);
        }

        if (collision.gameObject.GetComponent<KnockBackDamage>()) {
            Damage(Constants.BASIC_SWORD_DMG);
            player.playerMovement.Knockback(collision.gameObject.transform); //knock back player from the collided object
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
