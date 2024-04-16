using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class ChangeRespawn : MonoBehaviour
{
    private PlayerHealth healthScript;
    private GameObject player;
    [SerializeField] public Transform respawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        healthScript = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.name == "Player")
        {
            Debug.Log("test");
            if (respawnPoint != null)
            {
                healthScript.respawnPoint = respawnPoint;
            }
            else
            {
                Debug.Log("New respawn point is null!");
            }
            Destroy(gameObject);
        }
        
    }
}
