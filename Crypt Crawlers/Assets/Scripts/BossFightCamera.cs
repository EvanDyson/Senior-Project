using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightCamera : MonoBehaviour
{
    /*
    public Transform player; // The player's transform
    public Vector3 offset; // The offset at which the camera follows the player
    public float followSpeed = 0.125f; // The speed at which the camera catches up
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSpeed);
        transform.position = smoothedPosition;
    }*/

    public Transform player; // The player's transform
    public Vector3 offset; // The offset at which the camera follows the player
    public float followSpeed = 0.025f; // The speed at which the camera catches up
    public float threshold = 2.0f; // The distance the player needs to move before the camera starts following

    private Vector3 lastPlayerPosition;

    void Start()
    {
        lastPlayerPosition = player.position;
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(lastPlayerPosition, player.position);

        if (distance > threshold)
        {
            Vector3 desiredPosition = player.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSpeed);
            transform.position = smoothedPosition;

            lastPlayerPosition = player.position;
        }
    }
}