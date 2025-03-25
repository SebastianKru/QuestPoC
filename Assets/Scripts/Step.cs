using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Step : MonoBehaviour
{
    public GameObject annotationParent;
    public List<GameObject> annotations;
    public List<GameObject> cards;
    public GameObject cardsParent;
    public GameObject cardDescription;
    public GameObject cardHint;
    public GameObject cardImage1;
    public GameObject cardImage2;

    public void InstantiateCardDescription(GameObject prefab, SABStep sabStep)
    {
        cardDescription = Instantiate(prefab, cardsParent.transform);
        cards.Add(cardDescription);
        cardDescription.GetComponent<CardDescription>().title.text = sabStep.descriptionHeading;
        cardDescription.GetComponent<CardDescription>().description.text = sabStep.descriptionText;
        this.gameObject.name = cardDescription.GetComponent<CardDescription>().title.text;
    }

    public void InstantiateCardHint(GameObject prefab, SABStep sabStep)
    {
        cardHint = Instantiate(prefab, cardsParent.transform);
        cards.Add(cardHint);
        cardHint.GetComponent<CardHint>().hint.text = sabStep.hint;
        cardHint.GetComponent<CardHint>().reasoning.text = sabStep.hintReasoning;
    }

    public void InstantiateCardImage1(GameObject prefab, SABStep sabStep)
    {
        cardImage1 = Instantiate(prefab, cardsParent.transform);
        cards.Add(cardImage1);
        cardImage1.GetComponent<CardImage>().image.sprite = Resources.Load<Sprite>(sabStep.image1Path);
    }

    public void InstantiateCardImage2(GameObject prefab, SABStep sabStep)
    {
        cardImage2 = Instantiate(prefab, cardsParent.transform);
        cards.Add(cardImage2);
        cardImage2.GetComponent<CardImage>().image.sprite = Resources.Load<Sprite>(sabStep.image2Path);
        cardImage2.transform.localPosition = new Vector3(cardImage2.transform.localPosition.x, 0.8f);
    }

    public void setCards(bool state)
    {
        // on reset all cards are enabled again (initial state) 
        foreach (GameObject card in cards)
        {
            if (card.GetComponent<Card>().disabledByUser == false)
                card.SetActive(state);
        }
    }

    public void resetCards()
    {
        // on reset all cards are enabled again (initial state) 
        foreach (GameObject card in cards)
        {
            if (card.tag != "Screenshot")
            {
                card.GetComponent<Card>().disabledByUser = false;
                //reset position to initial position 
                card.GetComponent<Card>().ResetPositionAndRotation();
            }
        }

        setCards(true);
    }

    public void setAnnotations(bool state)
    {
        // on reset all annotations are deletd
        foreach (GameObject annotation in annotations)
        {
            if (annotation != null)
                annotation.SetActive(state);
        }
    }

    public void deleteAnnotations()
    {
        // on reset all annotations are deletd
        foreach (GameObject annotation in annotations)
        {
            Destroy(annotation);
        }
    }


    public void enableStep()
    {
        this.gameObject.SetActive(true);
        setAnnotations(true);
        setCards(true);
    }

    public void disableStep()
    {
        this.gameObject.SetActive(false);
        setAnnotations(false);
        setCards(false);

    }
}
