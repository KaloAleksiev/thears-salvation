using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Enemy enemy;
    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public Animator animator;
    public LayerMask groundLayers;
    public Transform groundCheck;
    public Transform enemyTransform;

    public Transform attackPoint;
    public float attackRange = 0.7f;
    public LayerMask enemyLayers;
    public float attackRate = 1f;
    public float detectionRange = 12f;
    public float startAttackRange;
    public float attackDuration;

    public float size;

    float nextAttackTime = 0f;

    Path path;
    int currentWaypoint = 0;
    float distanceToPlayer;

    bool isFacingRight = false;
    bool wasFacingRight = false;

    Seeker seeker;
    Rigidbody2D rb;
    RaycastHit2D hit;

    Vector2 direction;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void Attack()
    {
        animator.SetBool("IsAttacking", true);
        animator.SetTrigger("Attack");
        StartCoroutine(StartAttacking());
    }

    IEnumerator StartAttacking()
    {
        yield return new WaitForSeconds(attackDuration);
        if (animator.GetInteger("AnimState") != 3)
        {
            Collider2D hitEnemy = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayers);
            if (hitEnemy != null)
            {
                PlayerHealth playerHealth = hitEnemy.GetComponent<PlayerHealth>();

                if (playerHealth)
                {
                    playerHealth.Damage(enemy.enemyData.Damage);
                }
            }
        }
        animator.SetBool("IsAttacking", false);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void Update()
    {
        hit = Physics2D.Raycast(groundCheck.position, -transform.up, 1f, groundLayers);
    }

    void FixedUpdate()
    {
        if (animator.GetInteger("AnimState") != 3)
        {
            distanceToPlayer = Vector2.Distance(rb.position, target.position);

            if (distanceToPlayer < detectionRange)
            {
                if (path == null)
                {
                    return;
                }

                if (currentWaypoint >= path.vectorPath.Count)
                {
                    return;
                }

                direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
                Vector2 force = direction * speed * Time.deltaTime;

                float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

                if (distance < nextWaypointDistance)
                {
                    currentWaypoint++;
                }

                if (hit.collider)
                {
                    if (distanceToPlayer < startAttackRange && target.GetComponent<Health>().currentHealth > 0)
                    {
                        animator.SetInteger("AnimState", 1);
                        if (direction.x > 0.01 && !isFacingRight)
                        {
                            Flip();
                        }
                        
                        if (direction.x < -0.01 && isFacingRight)
                        {
                            Flip();
                        }

                        if (Time.time >= nextAttackTime)
                        {
                            Attack();
                            nextAttackTime = Time.time + 1f / attackRate;
                        }
                    }
                    else
                    {
                        animator.SetInteger("AnimState", 2);
                        if (rb.velocity.x > 0.4f)
                        {
                            enemyTransform.localScale = new Vector3(-size, size, 1f);
                            isFacingRight = true;
                            animator.SetInteger("AnimState", 2);
                        }
                        else if (rb.velocity.x < -0.4f)
                        {
                            enemyTransform.localScale = new Vector3(size, size, 1f);
                            isFacingRight = false;
                            animator.SetInteger("AnimState", 2);
                        }
                        else if (rb.velocity.x > -0.4f && rb.velocity.x < 0.4f)
                        {
                            animator.SetInteger("AnimState", 1);
                        }

                        rb.AddForce(force);
                    }
                }
                else
                {
                    rb.velocity = Vector2.zero;
                    animator.SetInteger("AnimState", 1);
                    if (direction.x > 0 && !isFacingRight)
                    {
                        Flip();
                    }
                    else if (direction.x < 0 && isFacingRight)
                    {
                        Flip();
                    }
                }
            }
            else
            {
                rb.velocity = Vector2.zero;
                animator.SetInteger("AnimState", 1);
                if (isFacingRight && wasFacingRight)
                {
                    enemyTransform.localScale = new Vector3(size, size, 1f);
                    isFacingRight = false;
                    wasFacingRight = true;
                }
                else
                {
                    enemyTransform.localScale = new Vector3(-size, size, 1f);
                    isFacingRight = true;
                    wasFacingRight = false;
                }
            }
        }
    }
}