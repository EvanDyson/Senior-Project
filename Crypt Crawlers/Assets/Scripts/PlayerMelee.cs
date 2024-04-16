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
    private EnemyHealth enemyHealth;
    private DragonAI dragonAI_;
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

                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    if (enemiesToDamage[i].gameObject.tag == "Enemy")
                    {
                        Debug.Log("meleeing" + enemiesToDamage[i].gameObject.name);
                        enemyHealth = enemiesToDamage[i].gameObject.GetComponent<EnemyHealth>();
                        enemyHealth.TakeDamage(damage);
                    }
                    if (enemiesToDamage[i].gameObject.tag == "Boss")
                    {
                        Debug.Log("meleeing" + enemiesToDamage[i].gameObject.name);
                        dragonAI_ = enemiesToDamage[i].gameObject.GetComponent<DragonAI>();
                        dragonAI_.TakeDamage(damage);
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
