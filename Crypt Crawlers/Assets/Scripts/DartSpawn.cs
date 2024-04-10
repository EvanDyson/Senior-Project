using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartSpawn : MonoBehaviour
{
    public GameObject projectile;
    public Transform trigger;
    public Transform spawnPoint;
    public float force;
    ArrowMovement script;
    private bool leftDir = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SpawnObject()
    {
        int direction = -1;
        
        // Calculate the offset based on the object's size
        if (trigger.position.x > spawnPoint.position.x)
        {
            leftDir = false;
        }

        float offsetX = GetComponent<BoxCollider2D>().bounds.size.x / 2f;
        Vector2 spawnPosition = new Vector2((spawnPoint.position.x + offsetX) - GetComponent<BoxCollider2D>().bounds.size.x, spawnPoint.position.y);

        if (!leftDir)
        {
            //spawns on right instead of left
            spawnPosition = new Vector2(spawnPoint.position.x + offsetX, spawnPoint.position.y);
            direction = 1;
        }


        GameObject copy = Instantiate(projectile, spawnPosition, spawnPoint.rotation);
        script = copy.GetComponent<ArrowMovement>();
        script.ChangeSpeed(force, direction);
        Destroy(copy, 1f);
    }
}
