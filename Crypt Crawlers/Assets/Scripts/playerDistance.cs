using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDistance : MonoBehaviour
{
    public GameObject player;
    public Animator animator;
    public float detectDistance;
    private float player_Distance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player_Distance = Vector2.Distance(transform.position, player.transform.position);
        
        if (player_Distance < detectDistance )
        {
            animator.SetBool("playerClose", true);
        }
        else
        {
            animator.SetBool("playerClose", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectDistance);
    }
}
