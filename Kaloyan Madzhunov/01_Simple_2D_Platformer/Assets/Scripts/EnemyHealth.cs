using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthManager {
    public EnemyHealth(int _maxHealth) : base(_maxHealth) { }

    public GameObject itemPrefab;

    private void OnMouseDown() {
        TakeDamage(Constants.BASIC_SWORD_DMG);

        if (currentHealth <= 0) {
            Die();
        }
    }

    private void Die() {
        Instantiate(itemPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
