using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DoorControl : MonoBehaviour
{
    public Camera bossFightCamera;
    public Camera startingCamera;
    public CinemachineVirtualCamera cinCam;
    public Animator DoorController;

    void Start()
    {
        DoorController.SetBool("closeDoor", false);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DoorController.SetBool("closeDoor", true);
            startingCamera.enabled = false;
            bossFightCamera.enabled = true;
            cinCam.enabled = true;
        }
    }
}