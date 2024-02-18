using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Image HealthBar;
    [SerializeField] private Transform respawnPoint; // drag respawn point here in inspector

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (health > maxHealth)
            health = maxHealth;
        if (health <= 0)
            Respawn();

        HealthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("deathline")) {
            Respawn();
        }
    }

    private void Respawn() {
        // reset player position to respawn point
        transform.position = respawnPoint.position;
        if (health <= 0) {
            health = maxHealth;
        }
    }
}
