using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleportation : MonoBehaviour
{
    public Transform destination;
    public KeyCode teleportKey = KeyCode.T;
    public float teleportCooldown = 1f;
    public Text popupText;
    private bool canTeleport = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            popupText.enabled = true;
            popupText.color = Color.white;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            popupText.enabled = false;
        }
    }

    void Update()
    {
        if (canTeleport && Input.GetKeyDown(teleportKey))
        {
            TeleportPlayer();
            StartCoroutine(TeleportCooldown());
        }
    }

    void TeleportPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = destination.position;
    }

    IEnumerator TeleportCooldown()
    {
        canTeleport = false;
        yield return new WaitForSeconds(teleportCooldown);
        canTeleport = true;
    }
}
