using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSpiderAI : MonoBehaviour
{
    public float shootDistance;
    private GameObject player;
    private Rigidbody2D rb;
    private float playerDistance;

    public GameObject web;
    public Transform webPos;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        if (player == null )
        {
            Debug.Log("Object of name 'Player' not found");
        }
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector2.Distance(transform.position, player.transform.position);

        if (playerDistance < shootDistance)
        {
            timer += Time.deltaTime;

            facePlayer();

            if (timer > 2)
            {
                timer = 0;
                shoot();
            }
        }
    }

    void facePlayer()
    {
        Vector3 vectorToTarget = player.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 10);
    }
    void shoot()
    {
        Instantiate(web, webPos.position, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootDistance);
    }
}
