using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private bool isJumping = false;
    public float jumpForce;
    private Vector3 move;
    public static Transform playerTransform;
    private GameObject SlingshotVectorObject;
    private SpriteRenderer slingshotVectorSprite;
    bool flipped = false;

    //accessing the animator
    public Animator animator;

    // Audio source for the running sound effect
    //private AudioSource audioSource;
    //public AudioClip runningSound;

    void Start()
    {
        SlingshotVectorObject = GameObject.Find("SlingshotForce");
        slingshotVectorSprite = SlingshotVectorObject.GetComponent<SpriteRenderer>();
        playerTransform = transform;

        // Get the AudioSource component attached to the player object
        //audioSource = GetComponent<AudioSource>();
        // Set the running sound effect to loop
        //audioSource.clip = runningSound;
        //audioSource.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the 'A' or 'D' keys are pressed
        //float horizontalInput = Input.GetAxisRaw("Horizontal");
        //move = new Vector3(horizontalInput, 0f, 0f);
        move = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        animator.SetFloat("Speed", Mathf.Abs(move.x));

        // Play the running sound effect if the player is moving horizontally and not jumping
        /*if (horizontalInput != 0 && !isJumping)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }*/

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    void FixedUpdate()
    {
        transform.position += move * Time.deltaTime * moveSpeed;
        CheckForFlipping();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject)
        {
            isJumping = false;
        }
    }

    private void CheckForFlipping()
    {
        bool movingLeft = move.x < 0;
        bool movingRight = move.x > 0;

        if (movingLeft)
        {
            if (!flipped)
            {
                flipped = true;
                FlipSprite();
            }
            transform.localScale = new Vector3(-1f, transform.localScale.y);
        }
        if (movingRight)
        {
            if (flipped)
            {
                flipped = false;
                FlipSprite();
            }
            transform.localScale = new Vector3(1f, transform.localScale.y);
        }
    }

    private void FlipSprite()
    {
        if (slingshotVectorSprite != null)
        {
            slingshotVectorSprite.flipX = !slingshotVectorSprite.flipX;
        }
    }
}
