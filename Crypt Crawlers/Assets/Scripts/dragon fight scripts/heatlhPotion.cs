using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heatlhPotion : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    public float healingAmt = 0f;
    private PlayerHealth playerHealth;
    public float despawnTimer = 3.0f;
    private float currTime;
    void Start()
    {
        player = GameObject.Find("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        currTime = 0f;
        despawnTimer += Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        currTime = Time.time;
        if (currTime > despawnTimer)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            if ((playerHealth.health + healingAmt) <= 100f)
            {
                playerHealth.health += healingAmt;
            }
            else
            {
                playerHealth.health = 100f;
            }
            Destroy(gameObject);
        }
        
    }
}
