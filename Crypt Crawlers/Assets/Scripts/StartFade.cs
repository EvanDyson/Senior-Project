using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFade : MonoBehaviour
{
    // Start is called before the first frame update
    private LightFade startFade;
    void Start()
    {
        startFade = GetComponent<LightFade>();
        startFade.envokeTime = 0.3f;
    }

    
    
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            startFade.StartFade();
            Destroy(gameObject, 2f);
        }
        
    }
}
