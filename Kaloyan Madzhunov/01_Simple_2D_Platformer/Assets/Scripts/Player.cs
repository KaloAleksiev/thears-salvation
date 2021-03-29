using UnityEngine;

public class Player : MonoBehaviour {
    public PlayerMovement playerMovement;
    public PlayerHealth playerHealth;

    public Health health;

    public void ResetPlayerHealth() {
        health.currentHealth = health.maxHealth;
        health.healthChange.Invoke();
    }
}
