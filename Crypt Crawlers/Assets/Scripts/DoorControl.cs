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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            startingCamera.enabled = false;
            bossFightCamera.enabled = true;
            cinCam.enabled = true;
            DoorControl.SetBool("closeDoor", true);
        }
    }
}
