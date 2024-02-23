using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartTrigger : MonoBehaviour
{
    private DartSpawn script;
    public GameObject dartSpawner;
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("entered");
        if (other.gameObject.CompareTag("Player"))
        {
            // Call the SpawnObject function when the trigger condition is met
            script = dartSpawner.GetComponent<DartSpawn>();
            Debug.Log("entered");
            script.SpawnObject();
        }
    }
}
