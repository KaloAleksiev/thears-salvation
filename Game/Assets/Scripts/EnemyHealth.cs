using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    public Animator animator;
    public Health health;
    public GameObject[] swords;
    public float disableTime = 3;

    public void GetDamaged(double damage)
    {
        health.TakeDamage(damage);

        if (health.currentHealth == 0)
        {
            Die();
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

        GameObject sword = swords[Random.Range(0, swords.Length)];
        Instantiate(sword, transform.position, Quaternion.identity);
    }

    public IEnumerator EnableEnemy(bool appear)
    {
        yield return new WaitForSeconds(disableTime);
        gameObject.GetComponent<SpriteRenderer>().enabled = appear;
    }
}
