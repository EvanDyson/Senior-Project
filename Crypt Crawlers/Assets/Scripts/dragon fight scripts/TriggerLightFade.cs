using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TriggerLightFade : MonoBehaviour
{
    private GameObject light; // Reference to the Global Light 2D object
    private Light2D globalLight;
    private float targetIntensity = 1f; // Target intensity for the light
    private float intensityChangeSpeed = 1f; // Speed at which the intensity changes
    public float envokeTime = 0.5f;

    void Start()
    {
        // Find the Global Light 2D object by name
        light = GameObject.Find("Global Light 2D");
        globalLight = light.GetComponent<Light2D>();


        
    }
    public void StartFade()
    {
        // Set the initial intensity to 0
        globalLight.intensity = 0f;

        // Call the method to gradually increase intensity after 0.3 seconds
        Invoke("IncreaseIntensity", envokeTime);
    }

    void IncreaseIntensity()
    {
        // Gradually increase the intensity towards the target intensity
        globalLight.intensity = Mathf.MoveTowards(globalLight.intensity, targetIntensity, intensityChangeSpeed * Time.deltaTime);

        // If the intensity hasn't reached the target yet, schedule another call to this method
        if (globalLight.intensity < targetIntensity)
        {
            Invoke("IncreaseIntensity", Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartFade();
        Destroy(gameObject, 1.0f);
    }
}
