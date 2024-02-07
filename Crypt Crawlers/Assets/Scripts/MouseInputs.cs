using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputs : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //Left click
        if (Input.GetMouseButtonDown(0))
        { 
            Debug.Log("Mouse 0 - Left click");

            /*
                Gets the mouse position relative to where it is on the screen (also relative to screen resolution)
                (0, 0) at the bottom left
                (width, height) at the bottom right
             */
            Debug.Log("Mouse is at relative to the screen resolution: " + Input.mousePosition);
            /*
                ScreenToWorldPoint to get the world coordinates of something
                Gets the mouse position relative to where it is in the world
                    currently using camera position and camera starts at default Z = -10 that is why -10 is printed
            */
            Debug.Log("Mouse is at in the world: " + Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        //Right click
        if (Input.GetMouseButtonDown(1))
            Debug.Log("Mouse 1 - Right click");
        //Middle mouse button
        if (Input.GetMouseButtonDown(2))
            Debug.Log("Mouse 2 - Middle mouse button click");
        //second side button
        if (Input.GetMouseButtonDown(3))
            Debug.Log("Mouse 3 - Back side button click");
        //first side button?
        if (Input.GetMouseButtonDown(4))
            Debug.Log("Mouse 4 - Front side button click");
    }
}
