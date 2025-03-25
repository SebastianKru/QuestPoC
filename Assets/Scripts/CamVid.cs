using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class LoadImage : MonoBehaviour
{
    public StepManager stepManager;

    public GameObject cardPrefab;
    public GameObject cardGO;
    public CardImage cardScript;

    public OVRGrabber rightHand;
    public OVRGrabber leftHand;

    [Header("Marker Adjustments")]
    public float pos_offset_x;
    public float pos_offset_y;
    public float pos_offset_z;

    public float rot_offset_x;
    public float rot_offset_y;
    public float rot_offset_z;


    private void Start()
    {
    }

    void Update()
    {
        // while the button is pressed down, the image will be loaded in the Background 
        // but only a preview (transpraten Backgorund )is displayed
        // the preview is attached to the controller, so that the user can place it straight away
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            LoadMostRecentImage();

            cardScript.showPreview();
            stepManager.steps[stepManager.activeStepID].cards.Add(cardGO);
        }
        // once the button is released, the screenshot is displayed
        if (OVRInput.GetUp(OVRInput.Button.One))
        {
            cardScript.showCard();
            cardGO.transform.parent = null;
            cardGO.tag = "Screenshot";
            cardGO = null;
        }


        //else if (OVRInput.GetDown(OVRInput.Button.Two))
        //{
        //    LoadMostRecentVideo();
        //}

    }

    void LoadMostRecentImage()
    {
        string directoryPath = "/storage/emulated/0/Oculus/Screenshots"; // Replace with your directory path

        if (Directory.Exists(directoryPath))
        {
            var directoryInfo = new DirectoryInfo(directoryPath);
            var mostRecentFile = directoryInfo.GetFiles()
                                              .OrderByDescending(f => f.LastWriteTime)
                                              .FirstOrDefault();

            if (mostRecentFile != null)
            {
                byte[] fileData = File.ReadAllBytes(mostRecentFile.FullName);
                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.

                // Convert Texture2D to Sprite and assign it to the SpriteRenderer
                Sprite screenshot =
                    Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height),
                    new Vector2(0.5f, 0.5f), 100.0f);

                cardGO = Instantiate(cardPrefab, rightHand.transform);
                cardScript = cardGO.GetComponent<CardImage>();
                cardScript.image.sprite = screenshot;

                cardGO.transform.SetParent(rightHand.transform);
                cardGO.transform.localRotation = Quaternion.Euler(rot_offset_x, rot_offset_y, rot_offset_z);
                cardGO.transform.localPosition = new Vector3(pos_offset_x, pos_offset_y, pos_offset_z);
            }
            else
            {
                Debug.Log("No files in directory!");
            }
        }
        else
        {
            Debug.Log("Directory does not exist!");
        }
    }


    void LoadMostRecentVideo()
    {
        string directoryPathVid = "/storage/emulated/0/Oculus/VideoShots"; // Replace with your directory path

        if (Directory.Exists(directoryPathVid))
        {
            var directoryInfo = new DirectoryInfo(directoryPathVid);
            var mostRecentFile = directoryInfo.GetFiles()
                                              .OrderByDescending(f => f.LastWriteTime)
                                              .FirstOrDefault();

            if (mostRecentFile != null)
            {
                // Create a new GameObject with a VideoPlayer component
                GameObject videoObject = new GameObject("VideoObject");
                VideoPlayer videoPlayer = videoObject.AddComponent<VideoPlayer>();


                // Set VideoPlayer properties
                videoPlayer.playOnAwake = true;
                videoPlayer.source = VideoSource.Url;
                videoPlayer.url = mostRecentFile.FullName;

                // Create a plane to display the video
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.localScale = new Vector3(1.0f, 1.0f, 0.0001f);
                cube.transform.localPosition = new Vector3(0, 1, 0);
                cube.transform.localRotation = Quaternion.Euler(0, 180, 0);

                // Set the VideoPlayer's target texture
                RenderTexture renderTexture = new RenderTexture(1920, 1080, 24);
                videoPlayer.targetTexture = renderTexture;

                // Assign the RenderTexture to the Material of the cube
                cube.GetComponent<Renderer>().material.mainTexture = renderTexture;

                // Find the annotations of the active child of the parent GameObject
                Transform annotations = stepManager.steps[stepManager.activeStepID].transform.Find("annotations"); ;

                // Make the new GameObject a child of the active child of the parent GameObject
                if (annotations != null)
                {
                    videoObject.transform.SetParent(annotations);
                    cube.transform.SetParent(annotations);
                }
                else
                {
                    Debug.Log("No active child found!");
                }
            }
            else
            {
                Debug.Log("No files in directory!");
            }
        }
    }
}