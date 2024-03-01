using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    public int damage;
    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemy;
    public float startTimeBtwAttack;

    private float timeBtwAttack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                //Debug.Log("Mouse 0 - Left click");

                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    if (enemiesToDamage[i].CompareTag("Skeleton"))
                    {
                        enemiesToDamage[i].GetComponent<EnemyAI>().health -= damage;
                    }
                    if (enemiesToDamage[i].CompareTag("Spider"))
                    {
                        enemiesToDamage[i].GetComponent<SpiderAI>().health -= damage;
                    }
                }
            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
