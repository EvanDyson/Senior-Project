using System.Collections.Generic;
using UnityEngine;

public class NewEnemyAI : MonoBehaviour
{
    public List<Transform> points;
    public float speed = 2.0f;
    public float chaseRange;
    private int destPoint = 0;
    private Transform player;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        GotoNextPoint();
    }

    void GotoNextPoint()
    {
        if (points.Count == 0)
            return;

        destPoint = (destPoint + 1) % points.Count;
    }

    void Update()
    {
        float step = speed * Time.deltaTime;

        if (Vector3.Distance(player.position, transform.position) < chaseRange)
        {
            // Chase the player
            transform.position = Vector3.MoveTowards(transform.position, player.position, step);
        }
        else
        {
            // Move towards the next point
            transform.position = Vector3.MoveTowards(transform.position, points[destPoint].position, step);

            if (Vector3.Distance(transform.position, points[destPoint].position) < 0.5f)
            {
                GotoNextPoint();
            }
        }
    }

    void FixedUpdate()
    {
        // Flip the sprite based on direction
        if (Vector3.Distance(player.position, transform.position) < chaseRange)
        {
            if (transform.position.x < player.position.x)
            {
                spriteRenderer.flipX = false; // moving right
            }
            else if (transform.position.x > player.position.x)
            {
                spriteRenderer.flipX = true; // moving left
            }
        }
        else if (transform.position.x < points[destPoint].position.x)
        {
            spriteRenderer.flipX = false; // moving right
        }
        else if (transform.position.x > points[destPoint].position.x)
        {
            spriteRenderer.flipX = true; // moving left
        }
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < points.Count; i++)
        {
            Gizmos.DrawWireSphere(points[i].transform.position, 0.5f);
        }
        for (int i = 1; i < points.Count; i++)
        {
            Gizmos.DrawLine(points[i-1].transform.position, points[i].transform.position);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
