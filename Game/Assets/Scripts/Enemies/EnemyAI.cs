using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Enemy enemy;
    public Transform target;
    public float speed;
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

    [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
    [SerializeField] private float m_StepInterval;
    private float m_NextStep;
    private float m_StepCycle;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);

        m_StepCycle = 0f;
        m_NextStep = m_StepCycle / 2f;
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

    private void ProgressStepCycle(float speed)
    {
        if (rb.velocity.sqrMagnitude > 0)
        {
            m_StepCycle += (rb.velocity.magnitude + (speed * m_RunstepLenghten)) * Time.fixedDeltaTime;
        }

        if (!(m_StepCycle > m_NextStep))
        {
            return;
        }

        m_NextStep = m_StepCycle + m_StepInterval;

        enemy.playFootstepSound.Invoke();
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
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            if (hitEnemies.Length > 0)
            {
                foreach (Collider2D hitEnemy in hitEnemies)
                {
                    PlayerHealth playerHealth = hitEnemy.GetComponent<PlayerHealth>();
                    if (playerHealth)
                    {
                        enemy.playHitSound.Invoke();
                        playerHealth.Damage(enemy.enemyData.Damage);
                    }
                }
            }
            else
            {
                enemy.playSwingSound.Invoke();
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
                            ProgressStepCycle(speed / 100);
                        }
                        else if (rb.velocity.x < -0.4f)
                        {
                            enemyTransform.localScale = new Vector3(size, size, 1f);
                            isFacingRight = false;
                            animator.SetInteger("AnimState", 2);
                            ProgressStepCycle(speed / 100);
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