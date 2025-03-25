using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spawner : MonoBehaviour
{
    public GameObject annoPrefab;
    public GameObject annoPreview;

    private GameObject annotation;

    public GameObject Steps; //Parent objects which only has one active child
    public StepManager stepManager;
    private ToolManager toolManager;

    [Header("Hands & Grabbable")]
    public OVRGrabber rightHand;
    public OVRGrabber leftHand;
    public OVRGrabbable grabbable;

    [Header("Marker Adjustments")]
    public float pos_offset_x;
    public float pos_offset_y;
    public float pos_offset_z;

    public float rot_offset_x;
    public float rot_offset_y;
    public float rot_offset_z;

    private bool AnnotationHasBeenSpawned = false;

    private bool initialized = false;
    private void Start()
    {
        stepManager = GameObject.FindObjectOfType<StepManager>();
        toolManager = transform.parent.GetComponent<ToolManager>();
    }

    // Update is called once per frame
    void Update()
    {

        // if no preview is shown and the user is not pressing the grab button
        // the preview will be spawned 
        if (!initialized
            && OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger) < 0.1f)
        {
            annoPreview.SetActive(true);
            annoPreview.transform.SetParent(rightHand.transform);
            annoPreview.transform.localRotation = Quaternion.Euler(rot_offset_x, rot_offset_y, rot_offset_z);
            annoPreview.transform.localPosition = new Vector3(pos_offset_x, pos_offset_y, pos_offset_z);
            initialized = true;
        }


        // if the preview is active and the user is pressing the grab button, the preview is disabled
        if (initialized
            && OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger) > 0.1f)
        {
            annoPreview.SetActive(false);
            annoPreview.transform.SetParent(this.transform);
            initialized = false;
        }

        // if the user presses the trigger button, the arrow will be spawned  and follow the controller 
        else if (initialized
            && !AnnotationHasBeenSpawned
            && !toolManager.isHoveringOverUIButton
            && OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) >= 0.9f
            )
        {
            annoPreview.SetActive(false);
            annoPreview.transform.SetParent(this.transform);

            annotation = Instantiate(annoPrefab, rightHand.transform.position, rightHand.transform.rotation);
            annotation.transform.SetParent(rightHand.transform);
            annotation.transform.localRotation = Quaternion.Euler(rot_offset_x, rot_offset_y, rot_offset_z);
            annotation.transform.localPosition = new Vector3(pos_offset_x, pos_offset_y, pos_offset_z);
            AnnotationHasBeenSpawned = true;
        }


        // once the user unpresses the trigger button, the arrow will be disattached from the user hand
        else if (initialized
            && AnnotationHasBeenSpawned
            && OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) < 0.9f

            )
        {
            annotation.transform.SetParent(null);
            annotation.name = "step " + stepManager.activeStepID + " annotation";
            stepManager.steps[stepManager.activeStepID].annotations.Add(annotation);

            annotation = null; // Reset the currentSpawned
            AnnotationHasBeenSpawned = false;
            initialized = false;
        }


        //Button B 
        //if (OVRInput.GetDown(OVRInput.Button.Two))
        //{
        //    currentSpawned = Instantiate(SpawnPrefab_animated, rightHand.transform.position, rightHand.transform.rotation);
        //    currentSpawned.transform.SetParent(rightHand.transform);
        //    currentSpawned.transform.localRotation = Quaternion.Euler(rot_offset_x, rot_offset_y, rot_offset_z); // rotate the object accordingly
        //    currentSpawned.transform.localPosition = new Vector3(pos_offset_x, pos_offset_y, pos_offset_z);
        //}

        //else if (OVRInput.GetUp(OVRInput.Button.Two))
        //{
        //    currentSpawned.transform.SetParent(null);
        //    currentSpawned.transform.SetParent(stepManager.steps[stepManager.activeStepID].annotationParent.transform);
        //    currentSpawned = null; // Reset the currentSpawned);
        //}

    }

    public void disableAnnotation()
    {
        if (initialized)
        {
            annoPreview.SetActive(false);
            annoPreview.transform.SetParent(this.transform);
            initialized = false;
        }

    }
}
