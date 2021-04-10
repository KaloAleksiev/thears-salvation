using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Player player;
    public Transform attackPoint;
    public float attackRange = 0.7f;
    public LayerMask enemyLayers;

    public float attackRate = 2.5f;
    float nextAttackTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        //Play animation
        player.playAttackAnimation.Invoke();

        //Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage enemies
        if (hitEnemies.Length != 0)
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

                if (enemyHealth) {
                    enemyHealth.GetDamaged(player.playerData.Damage);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
