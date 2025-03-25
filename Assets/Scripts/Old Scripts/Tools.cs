using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour
{
    public UnityEngine.UI.Toggle T_Pen;
    public UnityEngine.UI.Toggle T_Arrow;
    public UnityEngine.UI.Toggle T_ExMark;
    public UnityEngine.UI.Toggle T_Bolt;
    public UnityEngine.UI.Toggle T_CamVid;

    public GameObject Pen;
    public GameObject Arrow;
    public GameObject ExMArk;
    public GameObject Bolt;
    public GameObject CamVid;

    // Start is called before the first frame update
    void Start()
    {
        ////Initialize that no tool is used
        //Pen.SetActive(false);
        //Arrow.SetActive(false);
        //ExMArk.SetActive(false);
        //Bolt.SetActive(false);
        //CamVid.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ////Pen tool
        //if (T_Pen == true)
        //{
        //    Pen.SetActive(true);
        //}
        //else 
        //{
        //    Pen.SetActive(false);
        //}

        ////Arrow tool
        //if (T_Arrow == true)
        //{
        //    Arrow.SetActive(true);
        //}
        //else
        //{
        //    Arrow.SetActive(false);
        //}


        ////ExMark Tool
        //if (T_ExMark == true)
        //{
        //    ExMArk.SetActive(true);
        //}
        //else
        //{
        //    ExMArk.SetActive(false);
        //}


        ////Bolt tool
        //if (T_Bolt == true)
        //{
        //    Bolt.SetActive(true);
        //}
        //else
        //{
        //    Bolt.SetActive(false);
        //}


        ////Cam Tool
        //if (T_CamVid == true)
        //{
        //    CamVid.SetActive(true);
        //}
        //else
        //{
        //    CamVid.SetActive(false);
        //}



    }
}
