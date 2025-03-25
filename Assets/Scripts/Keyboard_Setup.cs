using UnityEngine;
using UnityEngine.UI;

public class KeyboardSetup : MonoBehaviour
{
    public GameObject keyboardPrefab; // The OVR keyboard prefab

    private void Start()
    {
        // Create a new Canvas
        GameObject canvasObject = new GameObject("Canvas");
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;

        // Set the position, size, and scale of the Canvas
        canvasObject.transform.position = new Vector3(0.0f, 0.5f, 2.0f);
        canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(100.0f, 100.0f);
        canvasObject.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

        // Create a new Input Field as a child of the Canvas
        GameObject inputFieldObject = new GameObject("InputField");
        inputFieldObject.transform.SetParent(canvasObject.transform);
        InputField inputField = inputFieldObject.AddComponent<InputField>();

        // Create a new Text object as a child of the Input Field
        GameObject textObject = new GameObject("Text");
        textObject.transform.SetParent(inputFieldObject.transform);
        Text text = textObject.AddComponent<Text>();

        // Set the properties of the Text component
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.text = "";
        text.color = Color.black;
        text.fontSize = 14;

        // Set the Text component of the Input Field to the new Text
        inputField.textComponent = text;

        // Instantiate the keyboard prefab
        GameObject keyboard = Instantiate(keyboardPrefab);

        // Set the Text Commit Field of the OVRVirtualKeyboard to the new Input Field
        keyboard.GetComponent<OVRVirtualKeyboard>().TextCommitField = inputField;
    }
}
