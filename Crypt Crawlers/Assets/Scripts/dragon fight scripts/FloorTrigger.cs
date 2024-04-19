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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("On the floor");
            dragonScript.movementOverride = false;
            dragonScript.rb.gravityScale = 1f;
            dragonScript.animation.SetBool("canShoot", true);
            dragonScript.playerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dragonScript.playerInTrigger = false;
            dragonScript.rb.gravityScale = 0f;
        }
    }
}
