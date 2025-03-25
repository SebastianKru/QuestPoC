using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LookAtTargetBehaviour : MonoBehaviour
{
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("HMD").GetComponent<Transform>();


    }

    // Update is called once per frame
    void Update()
    {
        // transform.LookAt(target);
    }
}
