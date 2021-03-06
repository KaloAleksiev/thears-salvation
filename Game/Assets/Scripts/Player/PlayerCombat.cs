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
                nextAttackTime = Time.time + 1f / (float)(attackRate * player.playerData.ActiveSword.AttackSpeedModifier);
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
                    player.playSwordHitSound.Invoke();
                    DealDamage(enemyHealth);
                }
                else
                {
                    player.playSwordSwingSound.Invoke();
                }
            }
        } else
        {
            player.playSwordSwingSound.Invoke();
        }
    }

    public void DealDamage(EnemyHealth enemyHealth)
    {
        Element enemyElement = enemyHealth.element;
        Element playerElement = player.playerData.ActiveSword.Element;
        if (enemyElement && playerElement)
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
            if (enemyHealth.health.currentHealth > 0)
            {
                enemyHealth.GetDamaged(10 * player.playerData.DamageMultiplier);
                player.playSpecialEffectSound.Invoke();
            }
        }
    }

    IEnumerator ReduceSpeed(EnemyHealth enemyHealth)
    {
        double originalSpeed = enemyHealth.enemy.enemyAI.speed;
        double slowedSpeed = enemyHealth.enemy.enemyAI.speed * player.playerData.SlowModifier;
        enemyHealth.enemy.enemyAI.speed = slowedSpeed;
        //player.playSpecialEffectSound.Invoke();
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
