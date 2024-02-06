using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementE : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private bool isJumping = false;
    public float jumpForce;

    //accessing the animator
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += move * Time.deltaTime * moveSpeed;

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject)
        {
            isJumping = false;
        }
    }

}
