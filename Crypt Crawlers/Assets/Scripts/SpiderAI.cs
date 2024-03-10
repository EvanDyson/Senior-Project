using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAI : MonoBehaviour
{
    public int health;
    public GameObject player;
    public List<Transform> points;
    public float speed;
    public float chaseDistance;
    //public Animator animator;
    private Rigidbody2D rb;
    //private Animator animation;
    private float playerDistance;

    Transform nextPoint;
    int pointNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nextPoint = points[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        playerDistance = Vector2.Distance(transform.position, player.transform.position);

        if (playerDistance < chaseDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            facePlayer();
        }
        else
        {
            Vector2 directionToGo = (nextPoint.position - transform.position).normalized;
            float distanceToPoint = Vector2.Distance(nextPoint.position, transform.position);

            // needs (* Time.deltaTime)? VVV
            rb.velocity = directionToGo * speed;
            if (distanceToPoint <= 0.5f)
            {
                pointNum++;
                if (pointNum >= points.Count)
                {
                    pointNum = 0;
                }
                nextPoint = points[pointNum];
            }
        }
    }

    void facePlayer()
    {
        Vector3 vectorToTarget = player.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 10);
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < points.Count; i++)
        {
            Gizmos.DrawWireSphere(points[i].position, 0.5f);
        }
        for (int i = 0; i < points.Count - 1; i++)
        {
            Gizmos.DrawLine(points[i].position, points[i + 1].position);
        }
        Gizmos.DrawLine(points[points.Count-1].position, points[0].position);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
}
