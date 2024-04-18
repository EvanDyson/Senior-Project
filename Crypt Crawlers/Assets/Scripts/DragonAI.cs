using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class DragonAI : MonoBehaviour
{
    public float health;
    private float maxHealth;
    public float damage;
    public float speed;
    public float detectDistance;
    public float shootDistance;
    private GameObject player;
    private Rigidbody2D rb;
    private float playerDistance;
    private float spriteSize;
    private SpriteRenderer spriteRenderer;
    public Animator animation;
    public UnityEngine.UI.Image dragonHealthBar;

    private bool shootingUp;
    private bool shootingSide;
    private bool shootingDown;

    // Start is called before the first frame update
    void Start()
    {
        spriteSize = transform.localScale.x;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        animation = GetComponent<Animator>();
        maxHealth = health;
        shootingUp = false;
        shootingSide = false;
        shootingDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        dragonHealthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);

        playerDistance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (playerDistance > shootDistance)
        {
            if (shootingUp)
            {
                shootingUp = false;
                animation.SetBool("shootUp", false);
            }
            else if (shootingSide)
            {
                shootingSide = false;
                animation.SetBool("shoot", false);
            }
            else if (shootingDown)
            {
                shootingDown = false;
                animation.SetBool("shootDown", false);
            }

            animation.SetBool("fly", true);

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
        else if (playerDistance < shootDistance)
        {
            animation.SetBool("fly", false);
            if (angle < 0)
            {
                shootingDown = true;
                animation.SetBool("shootDown", true);
            }
            else
            {
                shootingDown = false;
                animation.SetBool("shootDown", false);
            }

            if ((angle > 0 && angle < 45) || (angle < 180 && angle > 135))
            {
                shootingSide = true;
                animation.SetBool("shoot", true);
            }
            else
            {
                shootingSide = false;
                animation.SetBool("shoot", false);
            }

            if (angle > 45 && angle < 135)
            {
                shootingUp = true;
                animation.SetBool("shootUp", true);
            }
            else
            {
                shootingUp = false;
                animation.SetBool("shootUp", false);
            }
        }
    }
    void FixedUpdate()
    {
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        CheckForFlipping(angle);
    }

    private void CheckForFlipping(float angle)
    {
        bool playerOnLeft = angle > 90;
        bool playerOnRight = angle < 90;

        if (playerOnLeft)
        {
            transform.localScale = new Vector3(-spriteSize, transform.localScale.y);
        }
        if (playerOnRight)
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
            animation.SetBool("isDead", true);
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
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootDistance);
    }
}
