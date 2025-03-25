using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateObjectAlongAxis : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float duration = 1.5f;
    [SerializeField] private float lerpSpeed = 1;
    [SerializeField] private float delay = 1;
    [SerializeField] private bool repeat = true;

    private float timer = 0f;
    private bool movingForward = true;
    Vector3 velocity = new Vector3(0f, 0f, 0f);

    private void Update()
    {
        // Calculate the normalized time for the animation
        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / duration);

        // Interpolate the position between start and end points
        if (movingForward)
        {
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, endPoint.localPosition, ref velocity, lerpSpeed);
        }
        else
        {
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, startPoint.localPosition, ref velocity, lerpSpeed);
        }

        // Check if the animation is complete
        if (t >= 1f)
        {
            if (timer < delay)
            {
                timer += Time.deltaTime;
            }
            else
            {
                // Reverse direction if repeating
                if (repeat)
                {
                    movingForward = !movingForward;
                    timer = 0f;
                }
                else
                {
                    timer = 0f;
                    transform.localPosition = startPoint.localPosition;
                }
            }

        }
    }
}



