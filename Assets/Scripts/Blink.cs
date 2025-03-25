using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    /*public Color startcolor;
    public Color endcolor;
    [Range(0, 10)]
    public float speed;
   
    Renderer ren;  

    void awake()
    {
        ren = GetComponent<MeshRenderer>(); 
    }

    private void Update()
    {
        ren.material.color = Color.Lerp(startcolor, endcolor, Mathf.PingPong(Time.time * speed, 1)); 
    }*/

    public float speed;
    public Color startColor;
    public Color endColor;
    float startTime;
    public bool repeatable = false; 

    void Start()
    {
        startTime = Time.time; 
    }

    private void Update()
    {
        if (!repeatable)
        {
            float t = (Time.time - startTime) * speed;
            GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, t);
        }
        else
        {
            float t = (Mathf.Sin(Time.time - startTime) * speed);
            GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, t);
        }
        
    }
}
