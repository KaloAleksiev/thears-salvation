using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public Health health;
    public EnemyHealth enemyHealth;
    public EnemyAI enemyAI;
    public EnemyData enemyData;

    public UnityEvent playFootstepSound;
    public UnityEvent playPainSound;
    public UnityEvent playDeathSound;
    public UnityEvent playSwingSound;
    public UnityEvent playHitSound;
}
