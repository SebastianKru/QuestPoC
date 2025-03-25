using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SABImporter : MonoBehaviour { 

    //Import from Unity Asset Folder 
    public TextAsset csvFile;
    //directly import from quest, you might have to create the folder SAB
    string directoryPath = "/storage/emulated/0/Oculus/SAB";

    // Start is called before the first frame update
    void Start()
    {
        //load the most recent file from the SAB folder
        var directoryInfo = new DirectoryInfo(directoryPath);
        var mostRecentFile = directoryInfo.GetFiles()
                                         .OrderByDescending(f => f.LastWriteTime)
                                         .FirstOrDefault();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
