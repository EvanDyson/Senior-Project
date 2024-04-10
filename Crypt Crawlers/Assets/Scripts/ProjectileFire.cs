using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFire : MonoBehaviour
{
    public Camera mainCam;
    private Vector3 mousePos;
    public GameObject projectile;
    public Transform spawnPoint;
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;

    //for force vector
    private float forceTimer;
    public float timeBetweenIncreasing;
    private GameObject forceVector;
    private float force;
    // Start is called before the first frame update

    //Used to update projectilespeed
    ProjectileMovement script;
    void Start()
    {
        forceVector = GameObject.Find("SlingshotForce");
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        forceTimer += Time.deltaTime;
        if (!canFire) {
            

            timer += Time.deltaTime;
            if(timer > timeBetweenFiring )
            {
                canFire = true;
                timer = 0;
            }
        }
        if (Input.GetMouseButtonUp(1) && canFire)
        {
            //reset the force vector
            forceVector.transform.localScale = new Vector3(0, transform.localScale.y, transform.localScale.z);
            GameObject copy = Instantiate(projectile, spawnPoint.position, Quaternion.identity);
            script = copy.GetComponent<ProjectileMovement>();
            script.ChangeSpeed(force);
            force = 0;
            canFire = false;

            Destroy(copy, 2.0f);
        }


        if (Input.GetMouseButton(1) && force < 10.0 && forceTimer > timeBetweenIncreasing && canFire)
        {
            forceVector.transform.localScale += new Vector3(2, 0, 0);
            force += 4.0f;
            forceTimer = 0;
            
        }
    }
}
