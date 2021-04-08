using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public Animator animator;
    public LayerMask groundLayers;
    public Transform groundCheck;
    public Transform enemy;

    public Transform attackPoint;
    public float attackRange = 0.7f;
    public LayerMask enemyLayers;
    public float attackRate = 1f;
    public float detectionRange = 12f;
    
    float nextAttackTime = 0f;
    float attackDuration = 0.5f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    float distanceToPlayer;

    bool isFacingRight = false;
    bool wasFacingRight = false;

    Seeker seeker;
    Rigidbody2D rb;
    RaycastHit2D hit;

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
        //Play animation
        animator.SetTrigger("Attack");

        //Damage enemies
        StartCoroutine(StartAttacking());
    }

    IEnumerator StartAttacking()
    {
        yield return new WaitForSeconds(attackDuration);
        Collider2D hitEnemy = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayers);
        if (hitEnemy != null)
        {
            hitEnemy.GetComponent<PlayerHealth>().Damage(Constants.BANDIT_DMG);
        }
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
                    reachedEndOfPath = true;
                    return;
                }
                else
                {
                    reachedEndOfPath = false;
                }

                Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
                Vector2 force = direction * speed * Time.deltaTime;

                float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

                if (distance < nextWaypointDistance)
                {
                    currentWaypoint++;
                }

                if (hit.collider)
                {
                    if (distanceToPlayer < 3)
                    {
                        if (Time.time >= nextAttackTime)
                        {
                            Attack();
                            nextAttackTime = Time.time + 1f / attackRate;
                        }
                    }
                    else
                    {
                        if (rb.velocity.x > 0.4f)
                        {
                            enemy.localScale = new Vector3(-2f, 2f, 1f);
                            isFacingRight = true;
                            animator.SetInteger("AnimState", 2);
                        }
                        else if (rb.velocity.x < -0.4f)
                        {
                            enemy.localScale = new Vector3(2f, 2f, 1f);
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
                    if (distance < nextWaypointDistance)
                    {
                        currentWaypoint++;
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
                    enemy.localScale = new Vector3(2f, 2f, 1f);
                    isFacingRight = false;
                    wasFacingRight = true;
                }
                else
                {
                    enemy.localScale = new Vector3(-2f, 2f, 1f);
                    isFacingRight = true;
                    wasFacingRight = false;
                }
            }
        }
    }
}