using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public bool isHoveringOverUIButton = false;

    public void ControllerRayEnteringUI()
    {
        isHoveringOverUIButton = true;
    }

    public void ControllerRayLeavingUI()
    {
        isHoveringOverUIButton = false;
    }

    public void ToggleDrawing(bool active)
    {

    }
}
