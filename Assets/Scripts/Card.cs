using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Vector3 initialPosition;
    public Quaternion initialRotation;
    public bool disabledByUser = false;

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public void OnDeleteButtonPressed()
    {
        this.gameObject.SetActive(false);
        disabledByUser = true;
    }

    public void ResetPositionAndRotation()
    {
        this.transform.position = initialPosition;
        this.transform.rotation = initialRotation;
    }
}
