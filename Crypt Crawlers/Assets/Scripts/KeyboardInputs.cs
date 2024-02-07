using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputs : MonoBehaviour
{
    //accessing the animator
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        //need to reset the trigger so animation doesnt keep playing
        animator.ResetTrigger("AttackButtonPressed");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space Bar pressed");
            //trigger the animation
            animator.SetTrigger("AttackButtonPressed");
        }

        if (Input.GetKeyDown(KeyCode.F))
            Debug.Log("F key pressed");

        if (Input.GetKeyDown(KeyCode.Return))
            Debug.Log("Enter/Return pressed");
    }
}
