using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instaKill : MonoBehaviour
{
    public GameObject Dragon;
    private DragonAI dragonScript;

    // Start is called before the first frame update
    void Start()
    {
        dragonScript = Dragon.GetComponent<DragonAI>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ammo"))
        {
            dragonScript.health = 0f;
        }
    }
}
