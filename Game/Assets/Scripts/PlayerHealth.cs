using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Player player;
    public Health health;
    public PolygonCollider2D playerCollider;
    public float invincibilityTime;

    private void Start()
    {
        player.drainHealth.AddListener(DrainHealth);
        player.enablePlayerCollider.AddListener(EnablePlayerCollider);
    }

    private void OnDestroy()
    {
        player.drainHealth.RemoveListener(DrainHealth);
        player.enablePlayerCollider.RemoveListener(EnablePlayerCollider);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bandit"))
        {
            Damage(Constants.BANDIT_DMG);
        }
        else if (collision.CompareTag("Elemental"))
        {
            Damage(Constants.ELEMENTAL_DMG);
        }

        if (collision.GetComponent<KnockBackDamage>())
        {
            Damage(Constants.BASIC_SWORD_DMG);
            StartCoroutine(BecomeInvincible());
            player.knockBack.Invoke(collision.transform); //knock back player from the collided object
        }
    }

    public void Damage(int damage) {
        if (health.currentHealth > 0) {
            health.TakeDamage(damage);

            if (health.currentHealth == 0) {
                Die();
            }
        }
    }

    private IEnumerator BecomeInvincible() {
        // disable player collider
        EnablePlayerCollider(false);

        // wait for invincibilityTime seconds, a.k.a become invincible for invincibilityTime
        yield return new WaitForSeconds(invincibilityTime);

        // enable player collider only if the character is alive
        // if the character is dead, the collider must stay disabled so that the corpse cannot be hit
        if (health.currentHealth > 0) {
            EnablePlayerCollider(true);
        }
    }

    private void EnablePlayerCollider(bool isEnabled) {
        playerCollider.enabled = isEnabled;
    }

    private void DrainHealth()
    {
        Damage(health.currentHealth);
    }

    private void Die()
    {
        player.playDeathAnimation.Invoke();
        player.respawn.Invoke();
    }
}

