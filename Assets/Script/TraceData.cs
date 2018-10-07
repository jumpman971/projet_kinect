using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class TraceData : MonoBehaviour {
    private string data;
    private string folder;

    private KinectPointController k;

    // Use this for initialization
    void Start () {
        //folder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
        folder = "C:\\Users\\Takaruichi\\Documents\\Unity3D Projects\\tp_kinect\\trace.txt";
        File.WriteAllText(folder, "");
        k = GetComponent<KinectPointController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (k.isTracked)
        {
            data = Time.time + " " + transform.position.x + " " + transform.position.y + " " + transform.position.z;
            //File.WriteAllText(folder, data);
            File.AppendAllText(folder, data + "\n");
        }
    }
}
