using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    public GameObject pointA;
    public GameObject pointB;
    public float speed;
    public float chaseDistance;
    private float spriteSize;

    private Rigidbody2D rb;
    private Transform currentTarget;
    private float playerDistance;

    // Start is called before the first frame update
    void Start()
    {
        spriteSize = transform.localScale.x;
        rb = GetComponent<Rigidbody2D>();
        currentTarget = pointB.transform;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        playerDistance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (playerDistance < chaseDistance)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

            if (Mathf.Abs(angle) < 90)
            {
                transform.localScale = new Vector3(spriteSize, transform.localScale.y);
            }
            else if (Mathf.Abs(angle) > 90)
            {
                transform.localScale = new Vector3(-spriteSize, transform.localScale.y);
            }
        }
        else
        {
            //Debug.Log("skele " + transform.position);
            //Debug.Log("current target " + currentTarget.position);

            if (playerDistance > chaseDistance)
            {
                if (transform.position.x > currentTarget.position.x)
                {
                    rb.velocity = new Vector3(-speed, 0, 1);
                }
                else if (transform.position.x < currentTarget.position.x)
                {
                    rb.velocity = new Vector3(speed, 0, 1);
                }
            }

            //going to target on the right pointB
            if (currentTarget == pointB.transform)
            {
                rb.velocity = new Vector2(speed, 0);
            }
            else
            {
                //going to target on the left pointA
                rb.velocity = new Vector2(-speed, 0);
            }

            if (Vector2.Distance(transform.position, currentTarget.position) < 1f && currentTarget == pointB.transform)
            {
                currentTarget = pointA.transform;
            }
            if (Vector2.Distance(transform.position, currentTarget.position) < 1f && currentTarget == pointA.transform)
            {
                currentTarget = pointB.transform;
            }
        }
        CheckForFlipping();
    }

    private void CheckForFlipping()
    {
        bool movingLeft = rb.velocity.x < 0;
        bool movingRight = rb.velocity.x > 0;

        if (movingLeft)
        {
            transform.localScale = new Vector3(-spriteSize, transform.localScale.y);
        }
        if (movingRight)
        {
            transform.localScale = new Vector3(spriteSize, transform.localScale.y);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
}
