using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float force;
    private int direction = -1;
    public float damage = 50f;
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        

        //rb.velocity = new Vector2(-1, 1).normalized * force;

        rb.velocity = new Vector2(direction, 0) * force;
        Vector3 currentRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, currentRotation.z + -45f);
    }

    // Update is called once per frame
    
    //direction to have the dart move 0 for left 1 for right
    public void ChangeSpeed(float newSpeed, int dir)
    {
        direction = dir;
        force = newSpeed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().health -= damage;
            Debug.Log("buggy");
            Destroy(gameObject,0.1f);
        }
        
    }

}
