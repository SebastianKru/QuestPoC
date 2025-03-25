using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class Keyboard : MonoBehaviour
{
    public GameObject keyboard;
    private GameObject textObject;
    private Text inputText;
    public GameObject Steps; //Parent objects which only has one active child
    public StepManager stepManager;

    private void OnEnable()
    {
        keyboard.SetActive(true);

        textObject = new GameObject("InputText");
        inputText = textObject.AddComponent<Text>();

        //Set Properties
        inputText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        inputText.text = "";
        inputText.color = Color.black;
        inputText.fontSize = 10; // Set the font size
        inputText.transform.position = new Vector3(0, 0.0f, 5.0f);

        // Create a Canvas and set it as the parent of the textObject
        GameObject canvasObject = new GameObject("Canvas");
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        textObject.transform.SetParent(canvas.transform);

        Transform annotations = stepManager.steps[stepManager.activeStepID].transform.Find("annotations");
        inputText.transform.SetParent(annotations);
    }

    void Start()
    {
        stepManager = GameObject.FindObjectOfType<StepManager>();
    }

    void Update()
    {
        // Check if any key is pressed
        if (keyboard.activeSelf && OVRInput.GetDown(OVRInput.Button.Any))
        {
            // Get the key that was pressed and convert it to a string
            string key = OVRInput.Get(OVRInput.RawButton.Any).ToString();

            // Append the pressed key to the Text component
            inputText.text += key;
        }
    }

    private void OnDisable()
    {
        keyboard.SetActive(false);
    }
}
