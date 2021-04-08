using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour {
    public PlayerData playerData;
    public PlayerMovement playerMovement;
    public PlayerHealth playerHealth;
    public PlayerCombat playerCombat;
    public PlayerAnimations playerAnimations;
    public Health health;

    public KnockBackEvent knockBack;
    public UnityEvent drainHealth;
    public UnityEvent respawn;
    public SetSoulOrbsEvent addSoulOrbs;

    //animations
    public UnityEvent playJumpAnimation;
    public UnityEvent playAttackAnimation;
    public UnityEvent playHurtAnimation;
    public UnityEvent playDeathAnimation;
    public UnityEvent playRecoverAnimation;

    //animator
    public SetIntegerAnimatorEvent setIntegerAnimator;
    public SetFloatAnimatorEvent setFloatAnimator;
    public SetBoolAnimatorEvent setBoolAnimator;

    public void ResetPlayerHealth()
    {
        health.currentHealth = health.maxHealth;
        health.healthChange.Invoke();
    }
}

[System.Serializable] public class KnockBackEvent : UnityEvent<Transform> { }
[System.Serializable] public class SetSoulOrbsEvent : UnityEvent<int> { }
[System.Serializable] public class SetIntegerAnimatorEvent : UnityEvent<string, int> { }
[System.Serializable] public class SetFloatAnimatorEvent : UnityEvent<string, float> { }
[System.Serializable] public class SetBoolAnimatorEvent : UnityEvent<string, bool> { }
