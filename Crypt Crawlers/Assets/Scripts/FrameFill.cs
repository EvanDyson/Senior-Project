using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FrameFill : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEngine.UI.Image frameFill;
    public void fillFrame(float percentage)
    {
        frameFill.fillAmount = percentage;
    }
}
