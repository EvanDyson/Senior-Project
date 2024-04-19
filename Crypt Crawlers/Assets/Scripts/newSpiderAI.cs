using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class NewSpiderAI : MonoBehaviour
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

        if (Vector2.Distance(player.position, transform.position) < chaseRange)
        {
            // Chase the player
            transform.position = Vector2.MoveTowards(transform.position, player.position, step);
            facePlayer();
        }
        else
        {
            // Move towards the next point
            transform.position = Vector2.MoveTowards(transform.position, points[destPoint].position, step);
            facePoint();

            if (Vector2.Distance(transform.position, points[destPoint].position) < 0.5f)
            {
                GotoNextPoint();
            }
        }
    }

    void facePlayer()
    {
        Vector2 vectorToTarget = player.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 10);
    }

    void facePoint()
    {
        Vector2 vectorToTarget = points[destPoint].position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 10);
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

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
