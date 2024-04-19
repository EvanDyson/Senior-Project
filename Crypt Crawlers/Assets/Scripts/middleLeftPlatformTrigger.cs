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
            //Debug.Log("Player on left platform");
            dragonScript.movementOverride = true;
            dragonScript.leftPlatform = true;
            dragonScript.inTrigger = true;
        }
        if (other.CompareTag("Boss"))
        {
            dragonScript.movingToPlatform = false;
            dragonScript.animation.SetBool("canShoot", true);
            dragonScript.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player leaving right platform");
            dragonScript.movementOverride = false;
            dragonScript.leftPlatform = false;
            dragonScript.inTrigger = false;
        }
    }
}
