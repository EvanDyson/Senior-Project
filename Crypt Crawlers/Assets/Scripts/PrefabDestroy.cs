using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabDestroy : MonoBehaviour
{
    // Time before the prefab is destroyed
    public float destroyTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        // Invoke the DestroyPrefab method after destroyTime seconds
        Invoke("DestroyPrefab", destroyTime);
    }

    // Method to destroy the prefab
    void DestroyPrefab()
    {
        Destroy(gameObject);
    }
}
