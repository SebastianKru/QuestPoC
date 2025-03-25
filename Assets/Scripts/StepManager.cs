using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StepManager : MonoBehaviour
{
    public bool updateStepList = false;
    public GameObject stepPrefab;
    public GameObject cardDescriptionPrefab;
    public GameObject cardHintPrefab;
    public GameObject cardImagePrefab;

    public StepManagerUI stepManagerUI;

    // forward and backwards < > buttons 
    public Button backButton;
    public Button forwardButton;

    public Step[] steps;

    public int activeStepID = 0;
    private int oldStepID;

    // Start is called before the first frame update
    void Start()
    {
        //disbale all steps in the beginnin
        foreach (Step step in steps)
        {
            step.disableStep();
        }
        // enable the first step in the array
        ShowStep(activeStepID);
    }

    private void OnValidate()
    {
#if (UNITY_EDITOR)
        if (updateStepList)
        {
            UnityEditor.EditorApplication.delayCall += () =>
            {
                if (steps[0] != null)
                {
                    foreach (Step step in steps)
                    {
                        if (step != null)
                            DestroyImmediate(step.gameObject);
                    }
                }

                for (int i = 0; i < sabSteps.Length; i++)
                {
                    Debug.Log(sabSteps.Length);
                    GameObject stepGO = Instantiate(stepPrefab, this.transform);
                    steps[i] = stepGO.GetComponent<Step>();

                    steps[i].InstantiateCardDescription(cardDescriptionPrefab, sabSteps[i]);

                    if (sabSteps[i].hint != "")
                        steps[i].InstantiateCardHint(cardHintPrefab, sabSteps[i]);

                    if (sabSteps[i].image1Path != null)
                        steps[i].InstantiateCardImage1(cardImagePrefab, sabSteps[i]);

                    if (sabSteps[i].image2Path != null)
                        steps[i].InstantiateCardImage2(cardImagePrefab, sabSteps[i]);
                }

                UpdateListViewOfStepsInMainMenu();
                updateStepList = false;
            };
        }
#endif

    }

    private void UpdateListViewOfStepsInMainMenu()
    {
        stepManagerUI.DeleteElementsOfStepList();

        for (int i = 0; i < steps.Length; i++)
        {
            stepManagerUI.CreateElementOfStepList(i, steps[i]);
        }
    }

    // Update is called once per frame

    void Update()
    {
        // the following if else statements handle the interactive state of the back and forward buttons 
        if (activeStepID > 0)
        {
            backButton.interactable = true;
        }
        else
        {
            backButton.interactable = false;
        }

        if (activeStepID < steps.Length - 1)
        {
            forwardButton.interactable = true;
        }
        else
        {
            forwardButton.interactable = false;
        }

    }


    // go one step back in authoring mode 
    public void OnBackButtonPressed()
    {
        steps[activeStepID].disableStep();
        oldStepID = activeStepID;
        stepManagerUI.UpdateScrollRectPositionBackwards(activeStepID);
        activeStepID--;
        ShowStep(activeStepID, oldStepID);
    }

    // go one step forward in authoring mode 
    public void OnForwardButtonPressed()
    {
        steps[activeStepID].disableStep();
        oldStepID = activeStepID;
        stepManagerUI.UpdateScrollRectPositionForward(activeStepID);
        activeStepID++;
        ShowStep(activeStepID, oldStepID);
    }



    // is used if a user goes backwards or forwards through the steps
    // updates the necessary fields, so that a step is displayed 
    private void ShowStep(int curStep)
    {
        steps[curStep].enableStep();
        stepManagerUI.ChangeFontStyleToFat(curStep);
    }
    private void ShowStep(int curStep, int oldStep)
    {
        steps[curStep].enableStep();
        stepManagerUI.ChangeFontStyleToFat(curStep);
        stepManagerUI.ChangeFontStyleToNormal(oldStep);
    }


    // reset the step to its default state
    // disbale all annotations (could be deleted as well) 
    // enable all cards, in case they were disabled by clicking X 
    public void OnResetCardsButtonPressed()
    {
        steps[activeStepID].resetCards();
    }

    public void OnResetAnnotationsButtonPressed()
    {
        steps[activeStepID].deleteAnnotations();
    }

    public SABStep[] sabSteps = new SABStep[]
    {
        new SABStep(
            "Laufweg mit EMS zur n�chsten Karosse inkl. anschieben und verrriegeln",
            "Mit EMS (Ergonomischer Montage-Sitz zur n�chsten gehen 8m\r\nEMS anschieben 2x\r\nverriegeln 2x\r\nVerriegelung l�sen 2x",
            "",
            ""
            ),
        new SABStep(
            "724/744 Bremsdruckleitung Motorraum rechts vorn Gummi, Kabelbinder und Folien entfernen",
            "2x Gummi, 1x Kabelbinder entfernen/abziehen und in Sammelbeh�lter abwerfen. 2 Folien entsorgen.\r\nZeit anteilig f�r 15 BDL.",
            "724/744 BDL B�ndel vom Regal aufnehmen. 2x mal Gummi mit Folie und 1x mal Kabelbinder entfernen. 2x mal Folie in Knappsack entsorgen. 2x mal Gummi und 1x mal Kabelbinder in den daf�r vorgesehen Sammelbeh�lter ablegen. Auf die richtige M�lltrennung achten.",
            "wird nicht auf die richtige M�lltrennung geachtet, entstehen Mehrkosten f�r den Betrieb.",
            "SABBilder/3a",
            "SABBilder/3b"
            ),
        new SABStep(
            "725/745 Bremsdruckleitung Motorraum Trennstelle Gummi, Kabelbinder und Folien entfernen",
            "2x Gummi, 1x Kabelbinder entfernen/abziehen und in Sammelbeh�lter abwerfen. 2 Folien entsorgen.\r\nZeit anteilig f�r 15 BDL.",
            "725/745 BDL B�ndel vom Regal aufnehmen. 2x mal Gummi mit Folie und 1x mal Kabelbinder entfernen. 2x mal Folie in Knappsack entsorgen. 2x mal Gummi und 1x mal Kabelbinder in den daf�r vorgesehen Sammelbeh�lter ablegen. Auf richtige M�lltrennung achten.",
            "wird nicht auf die richtige M�lltrennung geachtet, entstehen Mehrkosten f�r den Betrieb.",
            "SABBilder/4a",
            "SABBilder/4b"
            ),
        new SABStep(
            "L0L 724 Bremsdruckleitung Motorraum in WAKA montieren und durch Radhaus rechts vorne f�deln inkl. einclipsen",
            "Linkslenker BDL aufnehmen.\r\nLeitung in Ausschnitt Radhaus rechts einf�deln und 4x in Halter WAKA und 2x in vormontierten doppelspurigen Abstandshalter (auf der 85H614725) einclipsen.\r\n\r\nLeitung ca.200cm L�nge \r\n\r\n- 85H614724 BDL",
            "724 BDL vom Regal aufnehmen. Leitung in Ausschnitt Radhaus rechts einf�deln und 4x mal in Halter WaKa und 2x mal in vormontierten doppelspurigen Abstandshalter einclipsen.",
            "wird die BDL nicht in die Halter eingeclipst, ist sie lose und kann mit der Zeit besch�digt werden. Dadurch ist eine Ordnungsgem��e Funktion der Bremsanlage nicht mehr gegeben. Es besteht Gefahr f�r Leib und Leben.",
            "SABBilder/5a",
            "SABBilder/5b"
            ),

        new SABStep(
            "BDL 724 2x B�gel von 3-fach B�gelclip auf Wasserkastenstirnwand mitte schlie�en (L0L)",
            "(nach Verbau BDL)\r\n2x B�gel aufnehmen und schliessen\r\n\r\nf�r BDL:\r\n\r\nL0L\r\n85H614724",
            "Die B�gel von 3-Fach B�gelclip auf WaKa-Stirnwand mitte nach Verbau der BDL schlie�en.",
            "werden die B�gel nicht geschlo�en, ist die BDL lose und kann mit der Zeit besch�digt werden. Dadurch ist eine Ordnungsgem��e Funktion der Bremsanlage nicht mehr gegeben. Es besteht Gefahr f�r Leib und Leben.",
            "SABBilder/6"
            ),

        new SABStep(
            "L0L 725 Bremsdruckleitung Motorraum montieren inkl. einclipsen",
            "Linkslenker BDL aufnehmen und an WAKA Stirnwand positionieren und 1x in Halter eindr�cken:\r\n\r\nLeitung ca.130cm L�nge \r\n\r\n- 85H614725 BDL",
            "725 BDL vom Regal aufnehmen, an WaKa-Stirnwand positionieren und 1x mal in 3-Fach Halter unten einclipsen.",
            "wird die BDL nicht in den Halter eingeclipst, ist sie lose und kann mit der Zeit besch�digt werden. Dadurch ist eine Ordnungsgem��e Funktion der Bremsanlage nicht mehr gegeben. Es besteht Gefahr f�r Leib und Leben.",
            "SABBilder/7"
            ),

        new SABStep(
            "L0R 744 Bremsdruckleitung Motorraum in WAKA montieren und durch Radhaus rechts vorne f�deln",
            "Rechtslenker BDL aufnehmen.\r\nLeitung in Ausschnitt Radhaus rechts einf�deln.\r\n\r\nLeitung ca.130cm L�nge \r\n\r\n- 85H614744 BDL",
            "744 BDL vom Regal aufnehmen und in Ausschnitt Radhaus rechts einf�deln.",
            "wird die BDL nicht durch das Radhaus gef�delt, kann sie nicht verbaut werden. Die Bremsanlage hat keine Funktion. Es besteht Gefahr f�r Leib und Leben.",
            "SABBilder/8"
            ),


        new SABStep(
            "L0R 745 Bremsdruckleitung Motorraum montieren inkl. einclipsen",
            "Rechtslenker BDL aufnehmen und an WAKA Stirnwand positionieren und 3x in Halter eindr�cken:\r\n\r\nLeitung ca.150cm L�nge \r\n\r\n- 85H614745 BDL",
            "745 BDL vom Regal aufnehmen und an WaKa-Stirnwand positionieren und den 3-Fach Halter an der Stirnwand eindr�cken",
            "wird die BDL nicht in die Halter eingeclipst, ist sie lose und kann mit der Zeit besch�digt werden. Dadurch ist eine Ordnungsgem��e Funktion der Bremsanlage nicht mehr gegeben. Es besteht Gefahr f�r Leib und Leben.",
            "SABBilder/9"
            ),

        new SABStep(
            "2x Staubschutzkappe von IBRS abziehen",
            "2x Staubschutzkappe von IBRS aufnhemen, abziehen und abwerfen.",
            "2x mal Staubschutzkappe von IBRS abziehen und in den daf�r vorgesehen Sammelbeh�lter abwerfen.",
            "werden die Staubschutzkappen nicht in den richtigen Sammelbeh�lter abgeworfen, entstehen Mehrkosten bei der Abfallentsorgung f�r den Betrieb.werden die Staubschutzkappen nicht entfernt, kann die BDL nicht verschraubt werden. Die Bremsanlage ist ohne Funktion. Es besteht Gefahr f�r Leib  und Leben.",
            "SABBilder/10"
            ),

        new SABStep(
            "724/744 Bremsdruckleitung Motorraum Staubschutzkappe BDL abziehen",
            "Staubschutzkappe von BDL aufnehmen, abziehen und abwerfen.\r\n\r\nL0L\r\n- 85H614724 BDL\r\n\r\nL0R\r\n- 85H614744 BDL",
            "Staubschutzkappe von 724/744 BDL abziehen und in den daf�r vorgesehenen Sammelbeh�lter abwerfen.",
            "werden die Staubschutzkappen nicht in den richtigen Sammelbeh�lter abgeworfen, entstehen Mehrkosten bei der Abfallentsorgung f�r den Betrieb.werden die Staubschutzkappen nicht entfernt, kann die BDL nicht verschraubt werden. Die Bremsanlage ist ohne Funktion. Es besteht Gefahr f�r Leib  und Leben.",
            "SABBilder/11"
            ),

        new SABStep(
            "725/745 Bremsdruckleitung Motorraum Staubschutzkappe BDL abziehen",
            "Staubschutzkappe von BDL aufnehmen, abziehen und abwerfen.\r\n\r\nL0L\r\n- 85H614725 BDL\r\n\r\nL0R\r\n- 85H614745 BDL",
            "Staubschutzkappe von 725/745 BDL abziehen und in den daf�r vorgesehenen Sammelbeh�lter abwerfen.",
            "werden die Staubschutzkappen nicht in den richtigen Sammelbeh�lter abgeworfen, entstehen Mehrkosten bei der Abfallentsorgung f�r den Betrieb.werden die Staubschutzkappen nicht entfernt, kann die BDL nicht verschraubt werden. Die Bremsanlage ist ohne Funktion. Es besteht Gefahr f�r Leib  und Leben.",
            "SABBilder/12"
            ),

        new SABStep(
            "724/744 Bremsdruckleitung Motorraum vorne an iBRS anf�deln",
            "BDL entwirren und von Hand an iBRS anf�deln\r\n\r\nL0L\r\n- 85H614724 BDL\r\n\r\nL0R\r\n- 85H614744 BDL",
            "724/744 BDL rechts vorne an IBRS von Hand anf�deln.",
            "wird die BDL nicht von Hand angef�delt, kann sich das Gewinde beim verschrauben verkannten. Die Bremsanlage ist undicht und hat keine Funktion. Es besteht Gefahr f�r Leib und Leben.",
            "SABBilder/13"
            ),

        new SABStep(
            "725/745 Bremsdruckleitung Motorraum Trennstelle an iBRS anf�deln",
            "BDL von Hand an iBRS anf�deln\r\n\r\nL0L\r\n- 85H614725 BDL\r\n\r\nL0R\r\n- 85H614745 BDL",
            "725/745 BDL Motorraum Trennstelle rechts hinten an IBRS von Hand anf�deln.",
            "wird die BDL nicht von Hand angef�delt, kann sich das Gewinde beim verschrauben verkannten. Die Bremsanlage ist undicht und hat keine Funktion. Es besteht Gefahr f�r Leib und Leben.",
            "SABBilder/14"
            ),

        new SABStep(
            "L0L: Stirnwandt�lle Aussenrahmen 7x EC verschrauben",
            "Schrauber aufnehmen und ablegen\r\n- T�lle 7x EC verschrauben",
            "EC-Schrauber vom MLW aufnehmen. Den Aussenrahme der  Stirnwandt�lle 7x mal an der Stirnwand verschrauben. Es gibt keine Verschraubreihenfolge f�r den Stirnwandt�llenrahmen. EC-Schrauber auf MLW ablegen.",
            "wird der Aussenrahmen nicht richtig verschraubt kommt es zum Bandstop. Bei N.I.O Verschraubung ist das Fzg. Undicht. Es l�uft Wasser in den Fzg.-Innenraum.",
            "SABBilder/16"
            ),

        new SABStep(
            "L0R: Stirnwandt�lle Aussenrahmen 7x EC verschrauben",
            "Schrauber aufnehmen und ablegen\r\n- T�lle 7x EC verschrauben",
            "EC-Schrauber vom MLW aufnehmen. Den Aussenrahme der  Stirnwandt�lle 7x mal an der Stirnwand verschrauben. Es gibt keine Verschraubreihenfolge f�r den Stirnwandt�llenrahmen. EC-Schrauber auf MLW ablegen.",
            "wird der Aussenrahmen nicht richtig verschraubt kommt es zum Bandstop. Bei N.I.O Verschraubung ist das Fzg. Undicht. Es l�uft Wasser in den Fzg.-Innenraum.",
            "SABBilder/17"
            ),

        new SABStep(
            "Dachleitungssatz an A-Pfosten links 3x mit Wickelclip befestigen",
            "Dachleitungssatz an A-Pfosten 3x mit Wickelclip befestigen\r\n\r\nBezeichnung: \r\nF5S: CL_3003/CL_3004/CL_3005\r\nF5R: CL_3003/CL_3009/CL_3055",
            "Dachleitungssatz aufnehmen und 2x mal an der A-S�ule mit Wickelclips befestigen.",
            "wird der Dachleitungssatz nicht mit den Clips an der A-S�ule befestigt, ist er lose und es kommt zu Ger�uschen im Fzg.-Innenraum.",
            "SABBilder/18"
            ),

        new SABStep(
            "Stecker an Laustsprecher A-S�ule oben links stecken",
            "Stecker aufnehmen und anstecken\r\n\r\nBezeichnung: LSPMTVOLI",
            "Stecker f�r 3D-Sound aufnehmen und am Lautsprecher vorne links anstecken und h�rbar verrasten.",
            "wird der Stecker nicht gesteckt, hat der Lautsprecher keine Funktion. Die vorgesehene Soundqualit�t ist nicht gegeben.",
            "SABBilder/19"
            ),

        new SABStep(
            "Dachleitungssatz an Dachquertr�ger links 3x mit Wickelclip befestigen",
            "Dachleitungssatz an Dachquertr�ger 3x mit Wickelclip bis Dachmodul \r\n\r\nBezeichnung: \r\nF5S: CL_3006/CL_3007/CL_3008\r\nF5R: CL_3056/CL_3057/CL_3058",
            "Dachleitungssatz aufnehmen und 3x mal an Dachquertr�ger links mit Wickelclips bis zum Dachmodul befestigen.",
            "werden die Clips nicht gesetzt, ist die Leitung lose und es entstehen Ger�usche im Fzg.-Innenraum.",
            "SABBilder/20"
            ),

        new SABStep(
            "Abgang f�r Stecker Sonnenblende mit Schalter durch Dachquertr�ger f�deln links",
            "Abgang durch Dachquertr�ger vorne links f�deln",
            "Abgang f�r Stecker Sonnenblende aufnehmen und durch die �ffnung vom Dachquertr�ger links f�deln.",
            "wird der Abgang nicht durch den Dachquertr�ger gef�delt, kann die Sonnenblende nicht verbaut werden, da das Kabel dadurch eingeklemmt wird.",
            "SABBilder/21"
            ),

        new SABStep(
            "Masse Dach vorne Mitte Spreizmutter und �se montieren",
            "- Spreizmutter aufnehmen\r\n- an Aussparung am Dach platzieren \r\n- eindr�cken\r\n- �se aufnehmen und in Spreizmutter montieren\r\n\r\nBezeichnung: MPB71",
            "Spreizmutter vom Regal aufnehmen. Spreizmutter an Aussparung am Dach platzieren und eindr�cken. �se aufnehmen und die Spreizmutter montieren.",
            "wird die Spreizmutter nicht verbaut, kann das Massekabel nicht verschraubt werden. Die Ordnungsgem��e Funktion der Elektronik ist nicht mehr gegeben.",
            "SABBilder/22a",
            "SABBilder/22b"
            ),

        new SABStep(
            "ER6; USB Buchse FAS Blende an Karosse montieren",
            "- USB Buchse aufnehmen\r\n- Adapter aufnehmen und auf USB Modeul verrasten\r\n- ZSB an Karosse verrasten\r\n- Leitung aufnehmen und 1x mit Clip befestigen",
            "ER6 USB Buchse und Adapter aufnehmen und zusammenstecken. ZSB an Karosse verrasten. Leitung aufnehmen und 1x mal mit Clip am Dachhimmel befestigen.",
            "wird der Adapter nicht an das USB-Modul gesteckt, hat die USB-Buchse keine Funktion.",
            "SABBilder/23a",
            "SABBilder/23b"
            ),

        new SABStep(
            "Stecker an USB Antenne Dach anstecken",
            "- Stecker aufnehmen und anstecken\r\n\r\nBezeichnung: USBDACH",
            "Stecker f�r USB-Antenne Dach aufnehmen und an die Antenne Dach h�rbar verrasten. Gegenzugpr�fung durchf�hren.",
            "wird die USB-Antenne Dach nicht gesteckt, hat sie keine Funktion. Fzg. hat keinen Empfang.",
            "SABBilder/24"
            ),
};
}
