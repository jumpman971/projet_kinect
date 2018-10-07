using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TraceDataRot : MonoBehaviour {
    private string data;
    private string folder;

    //private KinectPointController k;

    public GameObject right_hand;
    public GameObject neck;

    // Use this for initialization
    void Start()
    {
        //folder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
        folder = "C:\\Users\\Takaruichi\\Documents\\Unity3D Projects\\tp_kinect\\traceRot.txt";
        File.WriteAllText(folder, "");
        //k = GetComponent<KinectPointController>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (k.isTracked)
        {*/
            data = Time.time + " " + transform.rotation.x + " " + transform.rotation.y + " " + transform.rotation.z;
            //File.WriteAllText(folder, data);
            File.AppendAllText(folder, data + "\n");
        //}
    }
}
