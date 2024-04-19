using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tippyTopTrigger : MonoBehaviour
{
    public GameObject Dragon;
    private DragonAI dragonScript;
    private float defaultDistance;

    void Start()
    {
        dragonScript = Dragon.GetComponent<DragonAI>();
        defaultDistance = dragonScript.shootDistance;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dragonScript.shootDistance = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dragonScript.shootDistance = defaultDistance;
        }
    }
}
