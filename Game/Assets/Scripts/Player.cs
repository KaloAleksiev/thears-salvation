using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class KnockBackEvent : UnityEvent<Transform> { }
[System.Serializable]
public class SetSoulOrbsEvent : UnityEvent<int> { }

public class Player : MonoBehaviour {
    public PlayerMovement playerMovement;
    public PlayerHealth playerHealth;
    public PlayerCombat playerCombat;
    public Health health;

    public KnockBackEvent knockBack;
    public UnityEvent drainHealth;
    public UnityEvent respawn;
    public SetSoulOrbsEvent addSoulOrbs;

    public UnityEvent playDeathAnim;
    public UnityEvent playRecoverAnim;

    public void ResetPlayerHealth()
    {
        health.currentHealth = health.maxHealth;
        health.healthChange.Invoke();
    }
}
