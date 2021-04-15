using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour {
    public PlayerData playerData;
    public PlayerMovement playerMovement;
    public PlayerHealth playerHealth;
    public PlayerCombat playerCombat;
    public Health health;

    //movement events
    public KnockBackEvent knockBack;

    //health events
    public UnityEvent drainHealth;
    public UnityEvent resetPlayerHealth;
    public EnablePlayerColliderUnityEvent enablePlayerCollider;
    public UnityEvent setPlayerHealth;

    //manager events
    public UnityEvent respawn;
    public SetSoulOrbsEvent addSoulOrbs;

    //upgrades events
    public UpgradeUnityEvent healthBoost;
    public UpgradeUnityEvent ultraDefense;
    public UpgradeUnityEvent damageEnhance;

    //animations events
    public UnityEvent playJumpAnimation;
    public UnityEvent playAttackAnimation;
    public UnityEvent playHurtAnimation;
    public UnityEvent playDeathAnimation;
    public UnityEvent playRecoverAnimation;

    //animator events
    public SetIntegerAnimatorEvent setIntegerAnimator;
    public SetFloatAnimatorEvent setFloatAnimator;
    public SetBoolAnimatorEvent setBoolAnimator;

    //sound events
    public UnityEvent playJumpSound;
    public UnityEvent playLandingSound;
    public UnityEvent playFootstepSound;
    public UnityEvent playPainSound;
    public UnityEvent playDeathSound;
    public UnityEvent playSpawnSound;
}

[System.Serializable] public class KnockBackEvent : UnityEvent<Transform> { }
[System.Serializable] public class SetSoulOrbsEvent : UnityEvent<int> { }
[System.Serializable] public class SetIntegerAnimatorEvent : UnityEvent<string, int> { }
[System.Serializable] public class SetFloatAnimatorEvent : UnityEvent<string, float> { }
[System.Serializable] public class SetBoolAnimatorEvent : UnityEvent<string, bool> { }
[System.Serializable] public class EnablePlayerColliderUnityEvent : UnityEvent<bool> { }
[System.Serializable] public class UpgradeUnityEvent : UnityEvent<double> { }
