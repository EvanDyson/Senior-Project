using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAI : MonoBehaviour
{
    public float health = 1000f;
    public float damage;
    public float speed;
    public float detectDistance;
    private GameObject player;
    private Rigidbody2D rb;
    private float playerDistance;
    private float spriteSize;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteSize = transform.localScale.x;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        if (playerDistance < detectDistance)
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
    }
    void FixedUpdate()
    {
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

    public void TakeDamage(float damage)
    {
        // Reduce health by the damage amount
        health -= damage;

        // Check if health is less than or equal to 0
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
        // Call a function to handle the blinking effect
        BlinkRed();

    }

    private void BlinkRed()
    {
        // Change the sprite color to red
        spriteRenderer.color = Color.red;

        // Invoke a method to reset the sprite color after a short delay
        Invoke("ResetSpriteColor", 0.2f);

        // You can also hide the object in the editor
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetSelectedRenderState(gameObject.GetComponent<Renderer>(), UnityEditor.EditorSelectedRenderState.Hidden);
#endif
    }

    private void ResetSpriteColor()
    {
        // Reset the sprite color to its original color
        spriteRenderer.color = Color.white;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().health -= damage;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectDistance);
    }
}
