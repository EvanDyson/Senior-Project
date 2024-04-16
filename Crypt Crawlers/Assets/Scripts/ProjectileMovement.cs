using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    private float force;
    public float rotationSpeed;
    private EnemyHealth damage;
    private DragonAI dragonAI_;
    public float attackPower = 25f;
    // Start is called before the first frame update
    void Start()
    {
        
        //make sure player is not effected by collision
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime, Space.Self);
    }
    public void ChangeSpeed(float newSpeed)
    {
        force = newSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("X" + collision.gameObject.tag + "Y");
        if ( collision.gameObject.tag == "Breakable")
        {
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            damage = collision.gameObject.GetComponent<EnemyHealth>();
            damage.TakeDamage(attackPower);
        }
        else if (collision.gameObject.tag == "Boss")
        {
            dragonAI_ = collision.gameObject.GetComponent<DragonAI>();
            dragonAI_.TakeDamage(attackPower);
        }
        Destroy(gameObject, 0.1f);
    }

}
