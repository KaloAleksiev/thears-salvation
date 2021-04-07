using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Player player;
    public Health health;

    private void Start()
    {
        player.drainHealth.AddListener(DrainHealth);
    }

    private void OnDestroy()
    {
        player.drainHealth.RemoveListener(DrainHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bandit"))
        {
            Damage(Constants.BANDIT_DMG);
        }
        else if (collision.gameObject.CompareTag("Elemental"))
        {
            Damage(Constants.ELEMENTAL_DMG);
        }

        if (collision.gameObject.GetComponent<KnockBackDamage>())
        {
            Damage(Constants.BASIC_SWORD_DMG);
            player.knockBack.Invoke(collision.gameObject.transform); //knock back player from the collided object
        }
    }

    private void Damage(int damage)
    {
        health.TakeDamage(damage);

        if (health.currentHealth == 0)
        {
            Die();
        }
    }

    private void DrainHealth()
    {
        Damage(health.currentHealth);
    }

    private void Die()
    {
        player.respawn.Invoke();
    }
}

