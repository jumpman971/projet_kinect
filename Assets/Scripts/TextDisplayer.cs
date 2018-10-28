using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplayer : MonoBehaviour {
    public Text textObject;
    public float delay;

    private float startTime;
    private bool clignote;

	// Use this for initialization
	void Start () {
        clignote = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (clignote && Time.time > startTime)
        {
            textObject.color = Color.black;
            clignote = false;
        }
    }

    public void changeText(string text)
    {
        if (text.Equals(textObject.text))
        {
            clignote = true;
            startTime = Time.time + delay;
            textObject.color = Color.red;

        } else
            textObject.text = text;        
    }
}
