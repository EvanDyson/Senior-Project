using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject defaultPotion;
    public GameObject superPotion;
    public float spawnTimer = 10.0f;
    private float currTime = 0f;
    private int potionRoll = 0;

    void Start()
    {
        spawnTimer += Time.time;
    }
        void Update()
    {
        currTime = Time.time;
        if (currTime > spawnTimer)
        {
            potionRoll = Random.Range(0, 10);
            if (potionRoll < 7) {
                Instantiate(defaultPotion, transform.position, transform.rotation);
            }
            else
            {
                Instantiate(superPotion, transform.position, transform.rotation);
            }
            spawnTimer += Time.time;
        }
    }
}
