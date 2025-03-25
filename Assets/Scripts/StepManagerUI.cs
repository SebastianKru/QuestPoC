using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;
using static OVRPlugin;

public class StepManagerUI : MonoBehaviour
{
    public ScrollRect scrollRect;
    public Transform stepContainer;
    public GameObject step_UI_Prefab;
    public List<GameObject> ListOfStepsInMainMenu = new List<GameObject>();

    private float verticalScrollPerListItem = 0.077f;
    public bool stepMenuIsVisible = true;
    public bool toolMenuIsVisible = true;
    public GameObject stepMenu;
    public GameObject toolMenu;

    private void Start()
    {
        scrollRect.verticalNormalizedPosition = 1f;
    }

    public void DeleteElementsOfStepList()
    {
#if (UNITY_EDITOR)
        foreach (GameObject step in ListOfStepsInMainMenu)
        {
            DestroyImmediate(step);
        }

#endif
    }

    public void CreateElementOfStepList(int i, Step step)
    {
#if (UNITY_EDITOR)
        ListOfStepsInMainMenu[i] = Instantiate(step_UI_Prefab, stepContainer);
        ListOfStepsInMainMenu[i].GetComponent<StepUI>().title.text
            = step.cardDescription.GetComponent<CardDescription>().title.text;
#endif
    }

    private void OnValidate()
    {
        //scrollRect.verticalNormalizedPosition = 1f;
    }

    public void UpdateScrollRectPositionForward(int currentStepID)
    {
        if (currentStepID > 2 && currentStepID < 17)
            scrollRect.verticalNormalizedPosition -= verticalScrollPerListItem;
    }

    public void UpdateScrollRectPositionBackwards(int currentStepID)
    {
        if (currentStepID > 2 && currentStepID < 17)
            scrollRect.verticalNormalizedPosition += verticalScrollPerListItem;
    }

    public void ChangeFontStyleToFat(int cur)
    {
        //ListOfStepsInMainMenu[cur].GetComponent<StepUI>().title.fontStyle = (FontStyles)FontStyle.Bold;
        ListOfStepsInMainMenu[cur].GetComponent<StepUI>().title.color = Color.green;
    }
    public void ChangeFontStyleToNormal(int old)
    {
        //ListOfStepsInMainMenu[old].GetComponent<StepUI>().title.fontStyle = (FontStyles)FontStyle.Normal;
        ListOfStepsInMainMenu[old].GetComponent<StepUI>().title.color = Color.white;
    }


    public void OnShowHideMainMenuClicked()
    {
        if (stepMenuIsVisible)
        {
            stepMenu.SetActive(false);
            stepMenuIsVisible = false;
        }
        else
        {
            stepMenu.SetActive(true);
            stepMenuIsVisible = true;
        }

    }

    public void OnShowHideToolMenuClicked()
    {
        if (toolMenuIsVisible)
        {
            toolMenu.SetActive(false);
            toolMenuIsVisible = false;
        }
        else
        {
            toolMenu.SetActive(true);
            toolMenuIsVisible = true;
        }

    }
}