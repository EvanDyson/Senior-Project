using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class middleRightPlatformTrigger : MonoBehaviour
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
            //Debug.Log("Player on right platform");
            dragonScript.movementOverride = true;
            dragonScript.rightPlatform = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player leaving right platform");
            dragonScript.movementOverride = false;
            dragonScript.rightPlatform = false;
        }
        if (other.CompareTag("Boss"))
        {
            dragonScript.movingToPlatform = false;
            dragonScript.animation.SetBool("canShoot", true);
            dragonScript.transform.rotation = Quaternion.Euler(0,0,0);
        }
    }
}
