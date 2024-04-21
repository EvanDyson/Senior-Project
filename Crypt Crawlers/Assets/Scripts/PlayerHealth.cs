using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    private PlayerMovement playerMovement;
    public float health;
    public float maxHealth;
    public UnityEngine.UI.Image HealthBar;
    public UnityEngine.UI.Image HealthBarOutline;
    public UnityEngine.UI.Image HealthBarStatus;
    public UnityEngine.UI.Image StatusEffectBar;
    public Sprite SpiderSlowdown;
    public Sprite FirstHealthBarOutline;
    public Sprite SecondHealthBarOutline;
    private float playerSpeed;
    [SerializeField] public Transform respawnPoint; // drag respawn point here in inspector
    private bool imageChanged;
    public float regenTimer;
    public float delayTime;
    public bool regenHealth;
    private bool bossScene;

    private float effectTimer = 2.5f;
    private float currentTime;
    private bool isSlowed = false;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerSpeed = playerMovement.moveSpeed;
        maxHealth = health;
        imageChanged = false;
        regenHealth = false;
        delayTime = 10f;
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Boss_Fight")
            bossScene = true;
        else
            bossScene = false;

        Debug.Log("Boss scene is currently" + bossScene);
    }

    // Update is called once per frame
    void Update()
    {
        // Out of bounds check to make sure the play
        if (transform.position.y <= -9)
        {
            Respawn();
        }
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

        if (health < 50 && !bossScene)
        {
            regenTimer += Time.deltaTime;

            if (regenTimer >= delayTime)
            {
                regenHealth = true;
            }

            if (regenHealth == true && health <= maxHealth)
            {
                health += 10;
                regenHealth = false;
                regenTimer = 0f;
            }
        }

        if (isSlowed)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                playerMovement.moveSpeed = playerSpeed;
                StatusEffectBar.sprite = FirstHealthBarOutline;
                isSlowed = false;
            }
        }
    }
    
    public void Respawn()
    {
        // reset player position to respawn point
        transform.position = respawnPoint.position;
        playerMovement.moveSpeed = playerSpeed;
        StatusEffectBar.sprite = FirstHealthBarOutline;
        isSlowed = false;
        health = maxHealth;

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "shooting web(Clone)")
        {
            if (!isSlowed)
            {
                StatusEffectBar.sprite = SpiderSlowdown;
                playerMovement.moveSpeed = 1.5f;
                isSlowed = true;
                currentTime = effectTimer;
            }
            else
            {
                currentTime = effectTimer;
            }
            
        }
    }
}