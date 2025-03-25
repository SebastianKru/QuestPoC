using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pen : MonoBehaviour
{
    //Pen properties
    [Header("Pen Properties")]
    public Transform tip;
    public Material drawingMaterial;
    public Material tipMaterial;
    //[Range(0.01f, 0.1f)]
    //public float penWidth = 0.01f;
    public Color[] penColors;
    public float[] penWidth;

    //Interaction Properties
    [Header("Hands & Grabbable")]
    public OVRGrabber rightHand;
    public OVRGrabber leftHand;
    public OVRGrabbable grabbable;

    private ToolManager toolManager;

    //current variables
    private LineRenderer currentDrawing;
    //private List<Vector3> positions = new List<Vector3>();
    private int index;
    private int currentColorIndex;
    private int currentpenWidthIndex;

    public GameObject ColorIndicator;
    public GameObject Steps;
    public StepManager stepManager;


    private void Start()
    {
        currentColorIndex = 0;
        currentpenWidthIndex = 0;
        tipMaterial.color = penColors[currentColorIndex];
        ColorIndicator.transform.localScale = new Vector3(
                penWidth[currentpenWidthIndex],
                ColorIndicator.transform.localScale.y,
                penWidth[currentpenWidthIndex]
                );
        this.transform.position = rightHand.transform.position;
        this.transform.rotation = rightHand.transform.rotation;
        toolManager = transform.parent.GetComponent<ToolManager>();

    }

    private void Update()
    {
        this.transform.position = rightHand.transform.position;
        this.transform.rotation = rightHand.transform.rotation;
        Quaternion rotation = Quaternion.Euler(0, 45, 0); // 45 degrees rotation around the y-axis
        this.transform.rotation = rightHand.transform.rotation * rotation;

        bool isGrabbed = grabbable.isGrabbed;

        if (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) >= 0.9f
            && !toolManager.isHoveringOverUIButton)

        {
            Draw();
            Debug.Log("Drawing method called!");

        }
        else if (currentDrawing != null) //clear if it is still drawing but not grabbed 
        {
            currentDrawing = null;
        }
        else if (OVRInput.GetDown(OVRInput.RawButton.A)) //Change color with A button 
        {
            SwitchColor();
        }
        else if (OVRInput.GetDown(OVRInput.RawButton.B)) //Switch pen width with B
        {
            SwitchPenWidth();
        }
    }

    private void Draw()
    {
        if (currentDrawing == null) //check if it isnt already drawing 
        {
            index = 0;
            currentDrawing = new GameObject().AddComponent<LineRenderer>(); //create Game object for drawing with properties below
            currentDrawing.material = drawingMaterial;
            currentDrawing.startColor = currentDrawing.endColor = penColors[currentColorIndex];
            currentDrawing.startWidth = currentDrawing.endWidth = penWidth[currentpenWidthIndex];
            currentDrawing.positionCount = 1;
            currentDrawing.SetPosition(0, tip.position); //Set drawing at tip position 
                                                         // make object child of delete node 

            currentDrawing.transform.SetParent(stepManager.steps[stepManager.activeStepID].annotationParent.transform);
            stepManager.steps[stepManager.activeStepID].annotations.Add(currentDrawing.gameObject);
        }
        else //if drawing and distance bigger than penWidth go to next position 
        {
            var currentPos = currentDrawing.GetPosition(index);
            if (Vector3.Distance(currentPos, tip.position) > 0.005f)
            {
                index++;
                currentDrawing.positionCount = index + 1;
                currentDrawing.SetPosition(index, tip.position);
            }
        }
    }

    private void SwitchColor()
    {
        if (currentColorIndex == penColors.Length - 1) //reset if upper bound is reached
        {
            currentColorIndex = 0;
        }
        else // change color
        {
            currentColorIndex++;
        }
        tipMaterial.color = penColors[currentColorIndex];
    }

    private void SwitchPenWidth() // change tip size of the pen 
    {
        if (currentpenWidthIndex == penWidth.Length - 1)
            currentpenWidthIndex = 0;
        else
        {
            currentpenWidthIndex++;
        }
        ColorIndicator.transform.localScale = new Vector3(penWidth[currentpenWidthIndex], 0.007f, penWidth[currentpenWidthIndex]);
    }
}
