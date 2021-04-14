using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Player player;
    public Transform attackPoint;
    public float attackRange = 0.7f;
    public LayerMask enemyLayers;

    public float attackRate = 2;
    float nextAttackTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / (attackRate * player.playerData.ActiveSword.AttackSpeedModifier);
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

                if (enemyHealth) 
                {
                    DealDamage(enemyHealth);
                }
            }
        }
    }

    public void DealDamage(EnemyHealth enemyHealth)
    {
        Element enemyElement = enemyHealth.element;
        if (enemyElement)
        {
            if (enemyElement == player.playerData.ActiveSword.Element.WeakerElement)
            {
                enemyHealth.GetDamaged(player.playerData.ActiveSword.Damage * player.playerData.DamageMultiplier * player.playerData.ActiveSword.Element.DamageIncreaseModifier);
                DoSpecialEffects(enemyHealth);
            }
            else if (enemyElement == player.playerData.ActiveSword.Element.StrongerElement)
            {
                enemyHealth.GetDamaged(player.playerData.ActiveSword.Damage * player.playerData.DamageMultiplier * player.playerData.ActiveSword.Element.DamageDecreaseModifier);
                DoSpecialEffects(enemyHealth);
            }
            else
            {
                enemyHealth.GetDamaged(player.playerData.ActiveSword.Damage * player.playerData.DamageMultiplier);
                DoSpecialEffects(enemyHealth);
            }
        }
        else
        {
            enemyHealth.GetDamaged(player.playerData.ActiveSword.Damage * player.playerData.DamageMultiplier);
            DoSpecialEffects(enemyHealth);
        }
    }

    public void DoSpecialEffects(EnemyHealth enemyHealth)
    {
        if (player.playerData.ActiveSword.name == "Fire Sword")
        {
            StartCoroutine(BurnEnemies(enemyHealth));
        }
        else if (player.playerData.ActiveSword.name == "Water Sword")
        {
            StartCoroutine(ReduceSpeed(enemyHealth));
        }
    }

    IEnumerator BurnEnemies(EnemyHealth enemyHealth)
    {
        for (int i = 0; i < player.playerData.BurnTicks; i++)
        {
            yield return new WaitForSeconds(1);
            enemyHealth.GetDamaged(10 * player.playerData.DamageMultiplier);
        }
    }

    IEnumerator ReduceSpeed(EnemyHealth enemyHealth)
    {
        float originalSpeed = enemyHealth.enemy.enemyAI.speed;
        float slowedSpeed = enemyHealth.enemy.enemyAI.speed * player.playerData.SlowModifier;
        enemyHealth.enemy.enemyAI.speed = slowedSpeed;
        yield return new WaitForSeconds(3);
        enemyHealth.enemy.enemyAI.speed = originalSpeed;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
