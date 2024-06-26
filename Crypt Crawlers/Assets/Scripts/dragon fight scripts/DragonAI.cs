using System.Collections;
using System.Collections.Generic;
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
    public Rigidbody2D rb;
    private float playerDistance;
    private float spriteSize;
    private SpriteRenderer spriteRenderer;
    public Animator animation;
    public UnityEngine.UI.Image dragonHealthBar;
    public Canvas HealthBar;

    private bool shootingUp;
    private bool shootingSide;
    private bool shootingDown;
    public bool dragonOnPlatform;
    public bool playerOnPlatform;
    public bool movementOverride;
    public bool playerOnGround;

    public GameObject Dragon;
    private DragonAI dragonScript;

    // Start is called before the first frame update
    void Start()
    {
        dragonScript = Dragon.GetComponent<DragonAI>();
        spriteSize = transform.localScale.x;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        animation = GetComponent<Animator>();
        maxHealth = health;
        shootingUp = false;
        shootingSide = false;
        shootingDown = false;
        movementOverride = false;
        playerOnPlatform = false;
        dragonOnPlatform = false;
        playerOnGround = false;
    }

    // Update is called once per frame
    void Update()
    {
        //dragon health bar filling
        dragonHealthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);

        if (health <= 0)
        {
            death();
        }
        else
        {

            //player distance calculation
            playerDistance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (this.transform.position == new Vector3(-19.7f, 1.37f, 0f))
            {
                dragonOnPlatform = true;
            }
            else
            {
                dragonOnPlatform = false;
            }
            if (playerDistance < detectDistance)
            {
                // on the ground, too far to shoot, chase player
                if (playerDistance > shootDistance && playerOnGround && !playerOnPlatform && !dragonOnPlatform && !movementOverride)
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
                    //player within detect distance
                    if (playerDistance < detectDistance)
                    {
                        //fly to player
                        animation.SetBool("fly", true);
                        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
                    }
                }
                //chase the player while not on the ground or on the platform
                if (playerDistance > shootDistance && !playerOnGround && !dragonOnPlatform && !playerOnPlatform && !movementOverride)
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
                }
                //shooting at player on the ground
                if (playerDistance < shootDistance && !dragonOnPlatform && playerOnGround && !playerOnPlatform && !movementOverride)
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
                //player is on platform and dragon is flying to platform
                if (movementOverride && !dragonOnPlatform && playerOnPlatform && !playerOnGround)
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
                    rb.gravityScale = 0f;
                    transform.position = Vector2.MoveTowards(this.transform.position, new Vector3(-19.7f, 1.37f, 0f), speed * Time.deltaTime);
                }
                //dragon is on platform and player is on platform
                if (movementOverride && dragonOnPlatform && playerOnPlatform && !playerOnGround)
                {
                    animation.SetBool("fly", false);
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
            HealthBar.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        if (playerOnRight)
        {
            transform.localScale = new Vector3(spriteSize, transform.localScale.y);
            HealthBar.transform.rotation = Quaternion.Euler(0, 0, 0);
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
        if (health > 0)
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

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().health -= damage;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootDistance);
    }

    private void death()
    {
        animation.SetBool("shootUp", false);
        animation.SetBool("shootDown", false);
        animation.SetBool("shoot", false);
        animation.SetBool("fly", false);
        animation.SetBool("canShoot", false);
        animation.SetBool("isDead", true);
        damage = 0;
        HealthBar.enabled = false;
        dragonScript.enabled = false;
    }
}
