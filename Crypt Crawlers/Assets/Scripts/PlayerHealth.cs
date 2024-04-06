using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class PlayerHealth : MonoBehaviour
{

    private PlayerMovement playerMovement;
    public float health;
    public float maxHealth;
    public UnityEngine.UI.Image HealthBar;
    public UnityEngine.UI.Image HealthBarOutline;
    public Sprite FirstHealthBarOutline;
    public Sprite SecondHealthBarOutline;
    private float playerSpeed;
    [SerializeField] private Transform respawnPoint; // drag respawn point here in inspector
    private bool imageChanged;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerSpeed = playerMovement.moveSpeed;
        maxHealth = health;
        imageChanged = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (health > maxHealth)
            health = maxHealth;
        if (health <= 0)
            Respawn();

        HealthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
        if (health <= 10 && imageChanged == false)
        {
            HealthBarOutline.sprite = SecondHealthBarOutline;
            imageChanged = true;
        }
        else if (health > 10 && imageChanged == true)
        {
            HealthBarOutline.sprite = FirstHealthBarOutline;
            imageChanged = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "shooting web(Clone)")
        {
            StartCoroutine(SlowDown(2.0f));
        }
        if (collision.CompareTag("deathline"))
        {
            Respawn();
        }
    }

    IEnumerator SlowDown(float duration)
    {
        playerMovement.moveSpeed = 1.5f;
        yield return new WaitForSeconds(duration);
        playerMovement.moveSpeed = playerSpeed;
    }

    private void Respawn()
    {
        // reset player position to respawn point
        transform.position = respawnPoint.position;
        playerMovement.moveSpeed = playerSpeed;
        health = maxHealth;

    }
}