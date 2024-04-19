using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class middleLeftPlatformTrigger : MonoBehaviour
{
    public GameObject Dragon;
    private DragonAI dragonScript;

    // Start is called before the first frame update
    void Start()
    {
        dragonScript = Dragon.GetComponent<DragonAI>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dragonScript.movementOverride = true;
            dragonScript.playerOnPlatform = true;
            dragonScript.animation.SetBool("canShoot", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dragonScript.movementOverride = false;
            dragonScript.playerOnPlatform = false;
            dragonScript.animation.SetBool("canShoot", false);
            dragonScript.animation.SetBool("fly", true);
        }
    }
}
