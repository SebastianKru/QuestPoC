using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAB: MonoBehaviour
{


}

public class SABStep
{
    public string descriptionHeading;
    public string descriptionText;
    public string hint;
    public string hintReasoning;
    public string image1Path;
    public string image2Path;

    public SABStep(string _descriptionHeading, string _descriptionText, string _hint, string _hintReasoning, string _img1, string _img2)
    {
        descriptionHeading = _descriptionHeading;
        descriptionText = _descriptionText;
        hint = _hint;
        hintReasoning = _hintReasoning;
        image1Path = _img1;
        image2Path = _img2;
    }

    public SABStep(string _descriptionHeading, string _descriptionText, string _hint, string _hintReasoning, string _img1)
    {
        descriptionHeading = _descriptionHeading;
        descriptionText = _descriptionText;
        hint = _hint;
        hintReasoning = _hintReasoning;
        image1Path = _img1;
    }

    public SABStep(string _descriptionHeading, string _descriptionText, string _hint, string _hintReasoning)
    {
        descriptionHeading = _descriptionHeading;
        descriptionText = _descriptionText;
        hint = _hint;
        hintReasoning = _hintReasoning;
    }



}
