using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orcDamage : MonoBehaviour
{
    public float damage;
    private Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
        }
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.transform.position.y < transform.position.y)
            {
                other.gameObject.GetComponent<PlayerHealth>().health -= damage;
            }
            
        }
    }
}
