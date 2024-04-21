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
    private GameObject wizardOnCloud;
    private GameObject wizard;
    private GameObject wizardText;

    void Start()
    {
        wizardOnCloud = GameObject.Find("wizardOnCloud");
        wizardOnCloud.SetActive(false);
        //DoorController.SetBool("closeDoor", false);
        wizard = GameObject.Find("wizard");
        wizard.SetActive(true);
        wizardText = GameObject.Find("MessagesInScene");
        wizardText.SetActive(true);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DoorController.SetBool("closeDoor", true);
            wizardOnCloud.SetActive(true);
            wizard.SetActive(false);
            wizardText.SetActive(false);
            startingCamera.enabled = false;
            bossFightCamera.enabled = true;
            cinCam.enabled = true;
        }
    }
}