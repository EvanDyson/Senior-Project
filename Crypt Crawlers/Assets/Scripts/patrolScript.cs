using System.Collections.Generic;
using UnityEngine;

public class patrolScript : MonoBehaviour
{
    public List<Transform> points;
    public float speed = 2.0f;
    private int destPoint = 0;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
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

        // Move towards the next point
        transform.position = Vector3.MoveTowards(transform.position, points[destPoint].position, step);

        if (Vector3.Distance(transform.position, points[destPoint].position) < 0.5f)
        {
            GotoNextPoint();
        }
    }

    void FixedUpdate()
    {
        
        if (transform.position.x < points[destPoint].position.x)
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
            Gizmos.DrawLine(points[i - 1].transform.position, points[i].transform.position);
        }
    }
}
