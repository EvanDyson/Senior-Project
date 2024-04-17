using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleportation : MonoBehaviour
{
    public Transform destination;
    public KeyCode teleportKey = KeyCode.T;
    private bool canTeleport = false;
    private LightFade fadeLight;
    private void Start()
    {
        fadeLight = GetComponent<LightFade>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canTeleport = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canTeleport = false;
        }
    }
   
    void Update()
    {
        if (canTeleport && Input.GetKeyDown(teleportKey))
        {
            TeleportPlayer();
            fadeLight.StartFade();
        }
    }

    void TeleportPlayer()
    {
        canTeleport = false;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = destination.position;
    }

}
