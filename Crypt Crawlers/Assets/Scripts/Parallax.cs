using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Parallax : MonoBehaviour
{
    // Start is called before the first frame update
    private float width, startpos;
    public GameObject cam;
    public float parallaxEffect;
    private float ypos;
    void Start()
    {
        ypos = transform.position.y;
        startpos = transform.position.x;
        width = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dis = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + dis, ypos, transform.position.z);

        if (temp > startpos + width) startpos += width;
        else if (temp < startpos - width) startpos -= width;
    }
}
   
