using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class KnockBackEvent : UnityEvent<Transform> { }

public class Player : MonoBehaviour {
    public PlayerMovement playerMovement;
    public PlayerHealth playerHealth;

    public KnockBackEvent knockBack;
    public UnityEvent drainHealth;
    public UnityEvent respawn;

    public Health health;

    public void ResetPlayerHealth()
    {
        health.currentHealth = health.maxHealth;
        health.healthChange.Invoke();
    }
}
