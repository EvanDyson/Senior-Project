using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrigger : MonoBehaviour
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
            dragonScript.playerOnGround = true;
            dragonScript.rb.gravityScale = 1f;
            dragonScript.animation.SetBool("canShoot", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dragonScript.playerOnGround = false;
            dragonScript.rb.gravityScale = 0f;
            dragonScript.animation.SetBool("canShoot", false);
        }
    }
}
