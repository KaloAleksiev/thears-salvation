using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    public Enemy enemy;
    public Animator animator;
    public Health health;
    public Element element;
    public GameObject[] items;
    public float disableTime = 2;

    public void GetDamaged(double damage)
    {
        if (health.currentHealth > 0)
        {
            health.TakeDamage(damage);

            if (health.currentHealth == 0)
            {
                Die();
            }
        }
    }

    private void Die() {
        animator.SetInteger("AnimState", 3);
        animator.SetTrigger("Death");
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false;
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
