using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFireHitbox : MonoBehaviour
{
    public float damage = 5f;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Player hit by fire");
            other.gameObject.GetComponent<PlayerHealth>().health -= damage;
        }
    }
}
