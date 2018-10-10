using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDetector : MonoBehaviour {
    public GameObject rightHand;
    public float sensitivity;

    private Vector3 lastPos;
    private Vector3 currPos;
    private bool goingRight;
    private bool goingLeft;
    private bool forward;
    private bool backward;

    // Use this for initialization
    void Start () {
        lastPos = rightHand.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        currPos = rightHand.transform.position;

        float diff = Mathf.Abs(currPos.x - lastPos.x);

        //Debug.Log(currPos + " - " + lastPos);

        if (currPos.x > lastPos.x && diff >= sensitivity)
        {
            Debug.Log("Going to right");
        } else if (currPos.x < lastPos.x && diff >= sensitivity)
        {
            Debug.Log("Going to left");
        }

        lastPos = currPos;
	}
}
