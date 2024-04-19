using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class middleLeftPlatformTriggerDragon : MonoBehaviour
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
        if (other.CompareTag("Boss"))
        {
            dragonScript.movingToPlatform = false;
            dragonScript.dragonInTrigger = true;
            dragonScript.animation.SetBool("canShoot", true);
            //dragonScript.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Boss"))
        {
            dragonScript.dragonInTrigger = false;
            dragonScript.animation.SetBool("canShoot", false);
        }
    }
}
