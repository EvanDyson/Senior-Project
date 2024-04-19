using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ExitControl : MonoBehaviour
{
    public Camera bossFightCamera;
    public Camera exitCamera;
    public CinemachineVirtualCamera cinCam;
    public Animator ExitDoor;
    public GameObject Dragon;
    private DragonAI dragonScript;

    void Start()
    {
        dragonScript = Dragon.GetComponent<DragonAI>();
        ExitDoor.SetBool("openExit", false);
    }

    void Update()
    {
        if (dragonScript.health <= 0)
        {
            ExitDoor.SetBool("openExit", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bossFightCamera.enabled = false; ;
            cinCam.enabled = false;
            exitCamera.enabled = true;
        }
    }
}