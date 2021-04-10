using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Player player;
    public Health health;
    public PolygonCollider2D playerCollider;
    public float invincibilityTime;
    public int fadeBlinkTimes = 3;

    private void Start()
    {
        SetMaxHealth();
        ResetPlayerHealth();

        player.drainHealth.AddListener(DrainHealth);
        player.enablePlayerCollider.AddListener(EnablePlayerCollider);
        player.resetPlayerHealth.AddListener(ResetPlayerHealth);
        player.setPlayerHealth.AddListener(SetPlayerHealth);
    }

    private void OnDestroy()
    {
        player.drainHealth.RemoveListener(DrainHealth);
        player.enablePlayerCollider.RemoveListener(EnablePlayerCollider);
        player.resetPlayerHealth.RemoveListener(ResetPlayerHealth);
        player.setPlayerHealth.RemoveListener(SetPlayerHealth);
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

    public void Damage(double damage) {
        // damage the character only if it is alive
        if (health.currentHealth > 0) {
            health.TakeDamage(damage * player.playerData.DefenseMultiplier);
            player.playerData.CurrentHealth = health.currentHealth;

            if (health.currentHealth == 0) {
                Die();
            }
        }
    }

    private void SetMaxHealth() {
        health.maxHealth = player.playerData.MaxHealth;
        health.setMaxHealth.Invoke();
    }

    private void SetCurrentHealth() {
        health.currentHealth = player.playerData.CurrentHealth;
        health.setCurrentHealth.Invoke();
    }

    private void SetPlayerHealth() {
        SetMaxHealth();
        SetCurrentHealth();
    }

    private void ResetPlayerHealth() {
        player.playerData.CurrentHealth = player.playerData.MaxHealth;
        SetCurrentHealth();
    }

    private IEnumerator BecomeInvincible() {
        // make the character become invincible only if it is alive
        if (health.currentHealth > 0) {
            // disable player collider
            EnablePlayerCollider(false);

            // make the character blink to indicate it is invincible
            StartCoroutine(FadeBlink());

            // wait for 'invincibilityTime' seconds, a.k.a become invincible for 'invincibilityTime'
            yield return new WaitForSeconds(invincibilityTime);

            // enable player collider
            EnablePlayerCollider(true);
        }
    }
    
    private IEnumerator FadeBlink() {
        // make the character blink 'fadeBlinkTimes' times for 'invincibilityTime' seconds
        for (int i = 0; i < fadeBlinkTimes; i++) {
            player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
            yield return new WaitForSeconds(invincibilityTime / (fadeBlinkTimes * 2));
            player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibilityTime / (fadeBlinkTimes * 2));
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

