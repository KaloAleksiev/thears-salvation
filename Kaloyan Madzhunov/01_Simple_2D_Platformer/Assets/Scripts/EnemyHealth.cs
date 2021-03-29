using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    public Health health;
    public GameObject itemPrefab;

    private void OnMouseDown() {
        health.TakeDamage(Constants.BASIC_SWORD_DMG);

        if (health.currentHealth == 0) {
            Die();
        }
    }

    private void Die() {
        Destroy(gameObject);
        Instantiate(itemPrefab, transform.position, Quaternion.identity);
    }
}
