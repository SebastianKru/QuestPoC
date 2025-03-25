using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 10f; // Adjust this value to control the rotation speed
    public Vector3 rotationAxis = Vector3.up; // Set the desired rotation axis (e.g., Vector3.up for Y-axis)

    private void Update()
    {
        // Rotate the GameObject around its own axis
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}
