using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardImage : Card
{
    public Image image;

    public GameObject canvas;

    public void showPreview()
    {
        canvas.GetComponent<Canvas>().enabled = false;
    }

    public void showCard()
    {
        canvas.GetComponent<Canvas>().enabled = true;
    }
}
