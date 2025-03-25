using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInteractionFeedback : MonoBehaviour
{
    public Material cardMat;
    public Material cardMatTouch;
    private MeshRenderer myRenderer;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<MeshRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Controller")
        {
            myRenderer.material = cardMatTouch;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Controller")
        {
            myRenderer.material = cardMat;
        }
    }
}
