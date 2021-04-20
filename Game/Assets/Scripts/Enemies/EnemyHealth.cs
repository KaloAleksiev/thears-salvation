using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    public Enemy enemy;
    public Animator animator;
    public Health health;
    public Element element;
    public GameObject[] items;
    public float disableTime = 2;

    private void Start() {
        SetMaxHealth();
    }

    public void GetDamaged(double damage)
    {
        if (health.currentHealth > 0)
        {
            health.TakeDamage(damage);

            if (health.currentHealth == 0)
            {
                enemy.playDeathSound.Invoke();
                Die();
            } else
            {
                enemy.playPainSound.Invoke();
            }
        }
    }

    private void SetMaxHealth() {
        health.maxHealth = enemy.enemyData.Health;
    }

    private void Die() {
        animator.SetInteger("AnimState", 3);
        animator.SetTrigger("Death");
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false;
        // disable colliders in children
        Collider2D[] childrenColliders = GetComponentsInChildren<Collider2D>();
        foreach (Collider2D collider in childrenColliders) {
            collider.enabled = false;
        }
        health.sliderChange.Invoke(false);
        StartCoroutine(EnableEnemy(false));
        //Destroy(gameObject);
    }

    public IEnumerator EnableEnemy(bool appear)
    {
        yield return new WaitForSeconds(disableTime);
        gameObject.GetComponent<SpriteRenderer>().enabled = appear;

        if (items.Length == 4)
        {
            GameObject item = items[Random.Range(0, items.Length)];
            Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            Instantiate(item, spawnPoint, Quaternion.identity);
        }
        else if (items.Length == 1)
        {
            GameObject item = items[0];
            Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
            Instantiate(item, spawnPoint, Quaternion.identity);
        }
    }
}
