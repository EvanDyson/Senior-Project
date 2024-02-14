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

    //private Animator animation;
    private Rigidbody2D rb;
    private Transform currentTarget;
    private float playerDistance;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //animation = GetComponent<Animator>();
        currentTarget = pointB.transform;
        //animation.SetBool("isRunning", true);
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        // use rotation for spider to angle it towards player
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Vector2 point = currentTarget.position - transform.position;

        if (playerDistance < chaseDistance)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
        else
        {
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

            if (Vector2.Distance(transform.position, currentTarget.position) < 0.5f && currentTarget == pointB.transform)
            {
                //flip();
                currentTarget = pointA.transform;
            }
            if (Vector2.Distance(transform.position, currentTarget.position) < 0.5f && currentTarget == pointA.transform)
            {
                //flip();
                currentTarget = pointB.transform;
            }
        }
    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}
