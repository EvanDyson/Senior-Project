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

    //accessing the animator
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        move = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);

        animator.SetFloat("Speed", Mathf.Abs(move.x));

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
            transform.localScale = new Vector3(-1f, transform.localScale.y);
        }
        if (movingRight)
        {
            transform.localScale = new Vector3(1f, transform.localScale.y);
        }
    }
}
