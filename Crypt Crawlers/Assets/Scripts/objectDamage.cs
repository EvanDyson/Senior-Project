using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectDamage : MonoBehaviour
{
    public float damage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Breakable"))
        {
            Destroy(other.gameObject);
        }
    }
}
