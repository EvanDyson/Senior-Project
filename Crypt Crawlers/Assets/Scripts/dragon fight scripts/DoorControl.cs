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
    public GameObject wizardOnCloud;

    void Start()
    {
        wizardOnCloud = GameObject.Find("wizardOnCloud");
        wizardOnCloud.SetActive(false);
        //DoorController.SetBool("closeDoor", false);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DoorController.SetBool("closeDoor", true);
            wizardOnCloud.SetActive(true);
            startingCamera.enabled = false;
            bossFightCamera.enabled = true;
            cinCam.enabled = true;
        }
    }
}