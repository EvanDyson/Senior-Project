using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject defaultPotion;
    public GameObject superPotion;
    public float spawnTimer = 10.0f;
    private float currTime;
    private int potionRoll = 0;

    void Start()
    {
        currTime = 0f;
    }

    void Update()
    {
        currTime += Time.deltaTime;

        if (currTime >= spawnTimer)
        {
            potionRoll = Random.Range(0, 10);
            if (potionRoll < 7) {
                Instantiate(defaultPotion, transform.position, transform.rotation);
            }
            else
            {
                Instantiate(superPotion, transform.position, transform.rotation);
            }
            currTime = 0f;
        }
    }
}
