using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    public Enemy enemy;
    public Animator animator;
    public Health health;
    public Element element;
    public GameObject[] swords;
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

        GameObject sword = swords[Random.Range(0, swords.Length)];
        Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
        Instantiate(sword, spawnPoint, Quaternion.identity);
    }
}
