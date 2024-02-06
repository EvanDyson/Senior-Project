using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 5f;
    private float jumpingPower = 8f;
    private int jumpsRemaining = 2;
    private bool isFacingRight = true;
    private Collider2D _collider;
    private Animator _animator;
    private Rigidbody2D _rigidbody;

    [SerializeField] private bool _active = true;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f; // Adjust the speed as needed
    [SerializeField] private AudioSource shootingAudioSource;
    [SerializeField] private AudioClip shootSound;

    private bool isShooting = false;
    public int lives = 4;
    public int shots = 4;
    public float shotReplenishDelay = 2f;
    [SerializeField] private Transform respawnPoint; // Drag the respawn point (e.g., player's starting position) here in the Inspector
    
    public GameObject BarV5_ProgressBar1, BarV5_ProgressBar2, BarV5_ProgressBar3, BarV5_ProgressBar4;
    public GameObject BarV1_ProgressBar1, BarV1_ProgressBar2, BarV1_ProgressBar3, BarV1_ProgressBar4;

   private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("deathline"))
        {
            lives -= 1;

            if (lives > 0)
            {
                Respawn();
            }
            else
            {
                GameOver();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            lives -= 1;

            if (lives > 0)
            {
                Respawn();
            }
            else
            {
                GameOver();
            }
        }

        if (collision.gameObject.CompareTag("BossEnemy"))
        {
            lives -= 1;

            if (lives > 0)
            {
                Respawn();
            }
            else
            {
                GameOver();
            }
        }
    }

    private void Respawn()
    {
        // Reset player's position to the respawn point
        transform.position = respawnPoint.position;
        shots = 4;
    }

    private void GameOver()
    {
        SceneManager.LoadScene(0);
    }

    private void Start() {
        shootingAudioSource = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();

        BarV5_ProgressBar1.gameObject.SetActive (true);
        BarV5_ProgressBar2.gameObject.SetActive (true);
        BarV5_ProgressBar3.gameObject.SetActive (true);
        BarV5_ProgressBar4.gameObject.SetActive (true);
    }

    public void TakeLife() {
        lives -= 1;

            if (lives > 0)
            {
                Respawn();
            }
            else
            {
                GameOver();
            }
    }
    void Update()
    {
        switch (lives) {
            case 4:
            BarV5_ProgressBar1.gameObject.SetActive (true);
            BarV5_ProgressBar2.gameObject.SetActive (true);
            BarV5_ProgressBar3.gameObject.SetActive (true);
            BarV5_ProgressBar4.gameObject.SetActive (true);
            break;

            case 3:
            BarV5_ProgressBar1.gameObject.SetActive (true);
            BarV5_ProgressBar2.gameObject.SetActive (true);
            BarV5_ProgressBar3.gameObject.SetActive (true);
            BarV5_ProgressBar4.gameObject.SetActive (false);
            break;

            case 2:
            BarV5_ProgressBar1.gameObject.SetActive (true);
            BarV5_ProgressBar2.gameObject.SetActive (true);
            BarV5_ProgressBar3.gameObject.SetActive (false);
            BarV5_ProgressBar4.gameObject.SetActive (false);
            break;

            case 1:
            BarV5_ProgressBar1.gameObject.SetActive (true);
            BarV5_ProgressBar2.gameObject.SetActive (false);
            BarV5_ProgressBar3.gameObject.SetActive (false);
            BarV5_ProgressBar4.gameObject.SetActive (false);
            break;

            case 0:
            BarV5_ProgressBar1.gameObject.SetActive (false);
            BarV5_ProgressBar2.gameObject.SetActive (false);
            BarV5_ProgressBar3.gameObject.SetActive (false);
            BarV5_ProgressBar4.gameObject.SetActive (false);
            break;
        }

        switch (shots) {
            case 4:
            BarV1_ProgressBar1.gameObject.SetActive (true);
            BarV1_ProgressBar2.gameObject.SetActive (true);
            BarV1_ProgressBar3.gameObject.SetActive (true);
            BarV1_ProgressBar4.gameObject.SetActive (true);
            break;

            case 3:
            BarV1_ProgressBar1.gameObject.SetActive (true);
            BarV1_ProgressBar2.gameObject.SetActive (true);
            BarV1_ProgressBar3.gameObject.SetActive (true);
            BarV1_ProgressBar4.gameObject.SetActive (false);
            break;

            case 2:
            BarV1_ProgressBar1.gameObject.SetActive (true);
            BarV1_ProgressBar2.gameObject.SetActive (true);
            BarV1_ProgressBar3.gameObject.SetActive (false);
            BarV1_ProgressBar4.gameObject.SetActive (false);
            break;

            case 1:
            BarV1_ProgressBar1.gameObject.SetActive (true);
            BarV1_ProgressBar2.gameObject.SetActive (false);
            BarV1_ProgressBar3.gameObject.SetActive (false);
            BarV1_ProgressBar4.gameObject.SetActive (false);
            break;

            case 0:
            BarV1_ProgressBar1.gameObject.SetActive (false);
            BarV1_ProgressBar2.gameObject.SetActive (false);
            BarV1_ProgressBar3.gameObject.SetActive (false);
            BarV1_ProgressBar4.gameObject.SetActive (false);
            break;
        }

        
        HandleMovement();
        if(!_active) {
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (jumpsRemaining > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                jumpsRemaining--;
            }
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetButtonDown("Fire1") && !isShooting)
        {
            StartCoroutine(ShootCoroutine());
        }

        Flip();
    }

    private IEnumerator ShootCoroutine()
    {
        isShooting = true;

        Shoot();

        // Wait for a short delay before allowing the player to shoot again
        yield return new WaitForSeconds(0.1f);

        isShooting = false;
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void HandleMovement()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);

        if (IsGrounded())
        {
            jumpsRemaining = 2; // Reset jumps when grounded
        }
    }

    private void Flip()
    {
        if (PauseMenu.GameIsPaused == false) {
            if (isFacingRight && rb.velocity.x < 0f || !isFacingRight && rb.velocity.x > 0f)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
        }
    }

    private void Shoot()
    {
        if (PauseMenu.GameIsPaused == false) {
            if (shots > 0) {
                if (shootingAudioSource != null && shootSound != null)
                {
                    shootingAudioSource.PlayOneShot(shootSound);
                }
                Vector3 mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

                Vector2 shootDirection = mousePosition - projectileSpawnPoint.position;
                shootDirection.Normalize();

                GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

                rb.velocity = shootDirection * projectileSpeed;
                shots--;
            }
        }
    }

    public void Die() {
        _active = false; 
        _collider.enabled = false;
    }
    
}


