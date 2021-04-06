using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    public Animator animator;
    public Health health;
    public GameObject itemPrefab;
    public GameObject healthIndicator;
    public float disableTime = 3;

    private void OnMouseDown() {
        health.TakeDamage(Constants.BASIC_SWORD_DMG);

        if (health.currentHealth == 0) {
            Die();
        }
    }

    public void GetDamaged(int damage)
    {
        health.TakeDamage(damage);

        if (health.currentHealth == 0)
        {
            Die();
        }
    }

    private void Die() {
        animator.SetTrigger("Death");
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false;
        health.sliderChange.Invoke(false);
        StartCoroutine(EnableEnemy(false));
        //Destroy(gameObject);
        //Instantiate(itemPrefab, transform.position, Quaternion.identity);
    }

    public IEnumerator EnableEnemy(bool appear)
    {
        yield return new WaitForSeconds(disableTime);
        gameObject.GetComponent<SpriteRenderer>().enabled = appear;
    }
}
