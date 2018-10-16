using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplayer : MonoBehaviour {
    public Text test;
    public float delay;

    private float startTime;
    private bool clignote;

	// Use this for initialization
	void Start () {
        clignote = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeText(string text)
    {
        /*if (text.Equals(test.text))
        {
            if (startTime == 0f)
            {
                Debug.Log("1");
                startTime = Time.time;
                test.color = Color.red;
            }
            if (startTime+delay >= Time.time)
            {
                Debug.Log("2");
                test.color = Color.black;
                startTime = 0f;
            }

        } else 
            test.text = text;*/

        if (clignote && Time.time > startTime) {
            test.color = Color.black;
            clignote = false;
        } else {
            if (text.Equals(test.text)) {
                clignote = true;
                startTime = Time.time + delay;
                test.color = Color.red;

            } else
                test.text = text;
        }
        
    }
}
