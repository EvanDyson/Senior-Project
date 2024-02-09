using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float speed = 5.0f;
    public float chaseRange = 10.0f;
    private Rigidbody2D rb;
    private GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= chaseRange)
            {
                Vector3 direction = (player.transform.position - transform.position).normalized;
                rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
            }
            else
            {
                rb.velocity = Vector2.zero; // Stop the enemy completely
            }
        }
    }
}*/

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float speed = 5.0f;
    public float chaseRange = 10.0f;
    public float patrolRange = 5f;
    private Vector3 initialPosition;
    private bool isPatrolling = true;
    private bool isChasing = false;

    void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange)
        {
            isChasing = true;
            isPatrolling = false;
        }
        else if (distanceToPlayer > chaseRange && isChasing)
        {
            isChasing = false;
            isPatrolling = true;
        }

        if (isChasing)
        {
            ChasePlayer();
        }
        else if (isPatrolling)
        {
            Patrol();
        }
    }

    private void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    private void Patrol()
    {
        transform.position += transform.right * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, initialPosition) >= patrolRange)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            speed *= -1f;
        }
    }
}