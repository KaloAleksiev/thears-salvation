using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {
    public int maxHealth { get; set; }
    public int currentHealth { get; set; }

    public HealthBar healthBar;

    public HealthManager(int _maxHealth) {
        maxHealth = _maxHealth;
        currentHealth = _maxHealth;
    }

    private void Start() {
        ResetHealthBar();
    }

    public void ResetHealthBar() {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
