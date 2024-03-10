using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public int health;
    public GameObject player;
    public GameObject pointA;
    public GameObject pointB;
    public float speed;
    public float chaseDistance;
    public Animator animator;

    //private Animator animation;
    private Rigidbody2D rb;
    private Transform currentTarget;
    private float playerDistance;
    //public AudioSource SkeleFootsteps;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentTarget = pointB.transform;
        //SkeleFootsteps.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        playerDistance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        if (playerDistance < chaseDistance)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            
            if (Mathf.Abs(angle) < 90)
            {
                transform.localScale = new Vector3(1f, transform.localScale.y);
            }
            else if (Mathf.Abs(angle) > 90)
            {
                transform.localScale = new Vector3(-1f, transform.localScale.y);
            }
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
                currentTarget = pointA.transform;
            }
            if (Vector2.Distance(transform.position, currentTarget.position) < 0.5f && currentTarget == pointA.transform)
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
            transform.localScale = new Vector3(-1f, transform.localScale.y);
        }
        if (movingRight)
        {
            transform.localScale = new Vector3(1f, transform.localScale.y);
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
