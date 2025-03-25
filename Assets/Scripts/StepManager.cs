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
            "Laufweg mit EMS zur nächsten Karosse inkl. anschieben und verrriegeln",
            "Mit EMS (Ergonomischer Montage-Sitz zur nächsten gehen 8m\r\nEMS anschieben 2x\r\nverriegeln 2x\r\nVerriegelung lösen 2x",
            "",
            ""
            ),
        new SABStep(
            "724/744 Bremsdruckleitung Motorraum rechts vorn Gummi, Kabelbinder und Folien entfernen",
            "2x Gummi, 1x Kabelbinder entfernen/abziehen und in Sammelbehälter abwerfen. 2 Folien entsorgen.\r\nZeit anteilig für 15 BDL.",
            "724/744 BDL Bündel vom Regal aufnehmen. 2x mal Gummi mit Folie und 1x mal Kabelbinder entfernen. 2x mal Folie in Knappsack entsorgen. 2x mal Gummi und 1x mal Kabelbinder in den dafür vorgesehen Sammelbehälter ablegen. Auf die richtige Mülltrennung achten.",
            "wird nicht auf die richtige Mülltrennung geachtet, entstehen Mehrkosten für den Betrieb.",
            "SABBilder/3a",
            "SABBilder/3b"
            ),
        new SABStep(
            "725/745 Bremsdruckleitung Motorraum Trennstelle Gummi, Kabelbinder und Folien entfernen",
            "2x Gummi, 1x Kabelbinder entfernen/abziehen und in Sammelbehälter abwerfen. 2 Folien entsorgen.\r\nZeit anteilig für 15 BDL.",
            "725/745 BDL Bündel vom Regal aufnehmen. 2x mal Gummi mit Folie und 1x mal Kabelbinder entfernen. 2x mal Folie in Knappsack entsorgen. 2x mal Gummi und 1x mal Kabelbinder in den dafür vorgesehen Sammelbehälter ablegen. Auf richtige Mülltrennung achten.",
            "wird nicht auf die richtige Mülltrennung geachtet, entstehen Mehrkosten für den Betrieb.",
            "SABBilder/4a",
            "SABBilder/4b"
            ),
        new SABStep(
            "L0L 724 Bremsdruckleitung Motorraum in WAKA montieren und durch Radhaus rechts vorne fädeln inkl. einclipsen",
            "Linkslenker BDL aufnehmen.\r\nLeitung in Ausschnitt Radhaus rechts einfädeln und 4x in Halter WAKA und 2x in vormontierten doppelspurigen Abstandshalter (auf der 85H614725) einclipsen.\r\n\r\nLeitung ca.200cm Länge \r\n\r\n- 85H614724 BDL",
            "724 BDL vom Regal aufnehmen. Leitung in Ausschnitt Radhaus rechts einfädeln und 4x mal in Halter WaKa und 2x mal in vormontierten doppelspurigen Abstandshalter einclipsen.",
            "wird die BDL nicht in die Halter eingeclipst, ist sie lose und kann mit der Zeit beschädigt werden. Dadurch ist eine Ordnungsgemäße Funktion der Bremsanlage nicht mehr gegeben. Es besteht Gefahr für Leib und Leben.",
            "SABBilder/5a",
            "SABBilder/5b"
            ),

        new SABStep(
            "BDL 724 2x Bügel von 3-fach Bügelclip auf Wasserkastenstirnwand mitte schließen (L0L)",
            "(nach Verbau BDL)\r\n2x Bügel aufnehmen und schliessen\r\n\r\nfür BDL:\r\n\r\nL0L\r\n85H614724",
            "Die Bügel von 3-Fach Bügelclip auf WaKa-Stirnwand mitte nach Verbau der BDL schließen.",
            "werden die Bügel nicht geschloßen, ist die BDL lose und kann mit der Zeit beschädigt werden. Dadurch ist eine Ordnungsgemäße Funktion der Bremsanlage nicht mehr gegeben. Es besteht Gefahr für Leib und Leben.",
            "SABBilder/6"
            ),

        new SABStep(
            "L0L 725 Bremsdruckleitung Motorraum montieren inkl. einclipsen",
            "Linkslenker BDL aufnehmen und an WAKA Stirnwand positionieren und 1x in Halter eindrücken:\r\n\r\nLeitung ca.130cm Länge \r\n\r\n- 85H614725 BDL",
            "725 BDL vom Regal aufnehmen, an WaKa-Stirnwand positionieren und 1x mal in 3-Fach Halter unten einclipsen.",
            "wird die BDL nicht in den Halter eingeclipst, ist sie lose und kann mit der Zeit beschädigt werden. Dadurch ist eine Ordnungsgemäße Funktion der Bremsanlage nicht mehr gegeben. Es besteht Gefahr für Leib und Leben.",
            "SABBilder/7"
            ),

        new SABStep(
            "L0R 744 Bremsdruckleitung Motorraum in WAKA montieren und durch Radhaus rechts vorne fädeln",
            "Rechtslenker BDL aufnehmen.\r\nLeitung in Ausschnitt Radhaus rechts einfädeln.\r\n\r\nLeitung ca.130cm Länge \r\n\r\n- 85H614744 BDL",
            "744 BDL vom Regal aufnehmen und in Ausschnitt Radhaus rechts einfädeln.",
            "wird die BDL nicht durch das Radhaus gefädelt, kann sie nicht verbaut werden. Die Bremsanlage hat keine Funktion. Es besteht Gefahr für Leib und Leben.",
            "SABBilder/8"
            ),


        new SABStep(
            "L0R 745 Bremsdruckleitung Motorraum montieren inkl. einclipsen",
            "Rechtslenker BDL aufnehmen und an WAKA Stirnwand positionieren und 3x in Halter eindrücken:\r\n\r\nLeitung ca.150cm Länge \r\n\r\n- 85H614745 BDL",
            "745 BDL vom Regal aufnehmen und an WaKa-Stirnwand positionieren und den 3-Fach Halter an der Stirnwand eindrücken",
            "wird die BDL nicht in die Halter eingeclipst, ist sie lose und kann mit der Zeit beschädigt werden. Dadurch ist eine Ordnungsgemäße Funktion der Bremsanlage nicht mehr gegeben. Es besteht Gefahr für Leib und Leben.",
            "SABBilder/9"
            ),

        new SABStep(
            "2x Staubschutzkappe von IBRS abziehen",
            "2x Staubschutzkappe von IBRS aufnhemen, abziehen und abwerfen.",
            "2x mal Staubschutzkappe von IBRS abziehen und in den dafür vorgesehen Sammelbehälter abwerfen.",
            "werden die Staubschutzkappen nicht in den richtigen Sammelbehälter abgeworfen, entstehen Mehrkosten bei der Abfallentsorgung für den Betrieb.werden die Staubschutzkappen nicht entfernt, kann die BDL nicht verschraubt werden. Die Bremsanlage ist ohne Funktion. Es besteht Gefahr für Leib  und Leben.",
            "SABBilder/10"
            ),

        new SABStep(
            "724/744 Bremsdruckleitung Motorraum Staubschutzkappe BDL abziehen",
            "Staubschutzkappe von BDL aufnehmen, abziehen und abwerfen.\r\n\r\nL0L\r\n- 85H614724 BDL\r\n\r\nL0R\r\n- 85H614744 BDL",
            "Staubschutzkappe von 724/744 BDL abziehen und in den dafür vorgesehenen Sammelbehälter abwerfen.",
            "werden die Staubschutzkappen nicht in den richtigen Sammelbehälter abgeworfen, entstehen Mehrkosten bei der Abfallentsorgung für den Betrieb.werden die Staubschutzkappen nicht entfernt, kann die BDL nicht verschraubt werden. Die Bremsanlage ist ohne Funktion. Es besteht Gefahr für Leib  und Leben.",
            "SABBilder/11"
            ),

        new SABStep(
            "725/745 Bremsdruckleitung Motorraum Staubschutzkappe BDL abziehen",
            "Staubschutzkappe von BDL aufnehmen, abziehen und abwerfen.\r\n\r\nL0L\r\n- 85H614725 BDL\r\n\r\nL0R\r\n- 85H614745 BDL",
            "Staubschutzkappe von 725/745 BDL abziehen und in den dafür vorgesehenen Sammelbehälter abwerfen.",
            "werden die Staubschutzkappen nicht in den richtigen Sammelbehälter abgeworfen, entstehen Mehrkosten bei der Abfallentsorgung für den Betrieb.werden die Staubschutzkappen nicht entfernt, kann die BDL nicht verschraubt werden. Die Bremsanlage ist ohne Funktion. Es besteht Gefahr für Leib  und Leben.",
            "SABBilder/12"
            ),

        new SABStep(
            "724/744 Bremsdruckleitung Motorraum vorne an iBRS anfädeln",
            "BDL entwirren und von Hand an iBRS anfädeln\r\n\r\nL0L\r\n- 85H614724 BDL\r\n\r\nL0R\r\n- 85H614744 BDL",
            "724/744 BDL rechts vorne an IBRS von Hand anfädeln.",
            "wird die BDL nicht von Hand angefädelt, kann sich das Gewinde beim verschrauben verkannten. Die Bremsanlage ist undicht und hat keine Funktion. Es besteht Gefahr für Leib und Leben.",
            "SABBilder/13"
            ),

        new SABStep(
            "725/745 Bremsdruckleitung Motorraum Trennstelle an iBRS anfädeln",
            "BDL von Hand an iBRS anfädeln\r\n\r\nL0L\r\n- 85H614725 BDL\r\n\r\nL0R\r\n- 85H614745 BDL",
            "725/745 BDL Motorraum Trennstelle rechts hinten an IBRS von Hand anfädeln.",
            "wird die BDL nicht von Hand angefädelt, kann sich das Gewinde beim verschrauben verkannten. Die Bremsanlage ist undicht und hat keine Funktion. Es besteht Gefahr für Leib und Leben.",
            "SABBilder/14"
            ),

        new SABStep(
            "L0L: Stirnwandtülle Aussenrahmen 7x EC verschrauben",
            "Schrauber aufnehmen und ablegen\r\n- Tülle 7x EC verschrauben",
            "EC-Schrauber vom MLW aufnehmen. Den Aussenrahme der  Stirnwandtülle 7x mal an der Stirnwand verschrauben. Es gibt keine Verschraubreihenfolge für den Stirnwandtüllenrahmen. EC-Schrauber auf MLW ablegen.",
            "wird der Aussenrahmen nicht richtig verschraubt kommt es zum Bandstop. Bei N.I.O Verschraubung ist das Fzg. Undicht. Es läuft Wasser in den Fzg.-Innenraum.",
            "SABBilder/16"
            ),

        new SABStep(
            "L0R: Stirnwandtülle Aussenrahmen 7x EC verschrauben",
            "Schrauber aufnehmen und ablegen\r\n- Tülle 7x EC verschrauben",
            "EC-Schrauber vom MLW aufnehmen. Den Aussenrahme der  Stirnwandtülle 7x mal an der Stirnwand verschrauben. Es gibt keine Verschraubreihenfolge für den Stirnwandtüllenrahmen. EC-Schrauber auf MLW ablegen.",
            "wird der Aussenrahmen nicht richtig verschraubt kommt es zum Bandstop. Bei N.I.O Verschraubung ist das Fzg. Undicht. Es läuft Wasser in den Fzg.-Innenraum.",
            "SABBilder/17"
            ),

        new SABStep(
            "Dachleitungssatz an A-Pfosten links 3x mit Wickelclip befestigen",
            "Dachleitungssatz an A-Pfosten 3x mit Wickelclip befestigen\r\n\r\nBezeichnung: \r\nF5S: CL_3003/CL_3004/CL_3005\r\nF5R: CL_3003/CL_3009/CL_3055",
            "Dachleitungssatz aufnehmen und 2x mal an der A-Säule mit Wickelclips befestigen.",
            "wird der Dachleitungssatz nicht mit den Clips an der A-Säule befestigt, ist er lose und es kommt zu Geräuschen im Fzg.-Innenraum.",
            "SABBilder/18"
            ),

        new SABStep(
            "Stecker an Laustsprecher A-Säule oben links stecken",
            "Stecker aufnehmen und anstecken\r\n\r\nBezeichnung: LSPMTVOLI",
            "Stecker für 3D-Sound aufnehmen und am Lautsprecher vorne links anstecken und hörbar verrasten.",
            "wird der Stecker nicht gesteckt, hat der Lautsprecher keine Funktion. Die vorgesehene Soundqualität ist nicht gegeben.",
            "SABBilder/19"
            ),

        new SABStep(
            "Dachleitungssatz an Dachquerträger links 3x mit Wickelclip befestigen",
            "Dachleitungssatz an Dachquerträger 3x mit Wickelclip bis Dachmodul \r\n\r\nBezeichnung: \r\nF5S: CL_3006/CL_3007/CL_3008\r\nF5R: CL_3056/CL_3057/CL_3058",
            "Dachleitungssatz aufnehmen und 3x mal an Dachquerträger links mit Wickelclips bis zum Dachmodul befestigen.",
            "werden die Clips nicht gesetzt, ist die Leitung lose und es entstehen Geräusche im Fzg.-Innenraum.",
            "SABBilder/20"
            ),

        new SABStep(
            "Abgang für Stecker Sonnenblende mit Schalter durch Dachquerträger fädeln links",
            "Abgang durch Dachquerträger vorne links fädeln",
            "Abgang für Stecker Sonnenblende aufnehmen und durch die Öffnung vom Dachquerträger links fädeln.",
            "wird der Abgang nicht durch den Dachquerträger gefädelt, kann die Sonnenblende nicht verbaut werden, da das Kabel dadurch eingeklemmt wird.",
            "SABBilder/21"
            ),

        new SABStep(
            "Masse Dach vorne Mitte Spreizmutter und Öse montieren",
            "- Spreizmutter aufnehmen\r\n- an Aussparung am Dach platzieren \r\n- eindrücken\r\n- Öse aufnehmen und in Spreizmutter montieren\r\n\r\nBezeichnung: MPB71",
            "Spreizmutter vom Regal aufnehmen. Spreizmutter an Aussparung am Dach platzieren und eindrücken. Öse aufnehmen und die Spreizmutter montieren.",
            "wird die Spreizmutter nicht verbaut, kann das Massekabel nicht verschraubt werden. Die Ordnungsgemäße Funktion der Elektronik ist nicht mehr gegeben.",
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
            "Stecker für USB-Antenne Dach aufnehmen und an die Antenne Dach hörbar verrasten. Gegenzugprüfung durchführen.",
            "wird die USB-Antenne Dach nicht gesteckt, hat sie keine Funktion. Fzg. hat keinen Empfang.",
            "SABBilder/24"
            ),
};
}
