using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {
    public int maxHealth;
    public int currentHealth;
    public UnityEvent healthChange;

    private void Start() {
        ResetHealth();
    }

    public void ResetHealth() {
        currentHealth = maxHealth;
        healthChange.Invoke();
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        healthChange.Invoke();
    }
}
