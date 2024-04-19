using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DoorClose : MonoBehaviour
{
    public Camera bossFightCamera;
    public Camera startingCamera;
    public CinemachineVirtualCamera cinCam;
    public Animator DoorControl;

    void Start()
    {
        DoorControl.SetBool("closeDoor", false);
    }

        private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DoorControl.SetBool("closeDoor", true);
            startingCamera.enabled = false;
            bossFightCamera.enabled = true;
            cinCam.enabled = true;
        }
    }
}