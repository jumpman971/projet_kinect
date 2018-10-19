using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement4 : MonoBehaviour
{
    public GameObject rightHand;
    public float sensitivity;
    public float movementSensitivity;

    private Vector3 lastPos;
    private Vector3 currPos;
    private int goingZ = 0;
    private float startTime;
    private Vector2 startPos;

    // Use this for initialization
    void Start()
    {
        lastPos = rightHand.transform.position;
        //lastPos = new Vector3(0, 0, rightHand.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        currPos = rightHand.transform.position;
        //currPos = new Vector3(0, 0, rightHand.transform.position.z);

        float diff = Mathf.Abs(currPos.z - lastPos.z);

        //Debug.Log(currPos + " - " + lastPos);

        if (currPos.z > lastPos.z && diff >= sensitivity) {
            //Debug.Log("to right");
            if (goingZ == -1 || goingZ == 0) {
                //Debug.Log("left to right");
                startTime = Time.time;
                startPos = currPos;
            }
            goingZ = 1;
        } else if (currPos.z < lastPos.z && diff >= sensitivity) {
            //Debug.Log("to left");
            if (goingZ == 1) {
                //Debug.Log("right to left");
                //Debug.Log(Vector2.Distance(currPos, startPos));
                if (Vector3.Distance(currPos, startPos) < movementSensitivity) {
                    goingZ = 0;
                    startTime = 0f;
                    startPos = new Vector3();
                    //Debug.Log("faux mouvement");
                }
            }
            if (Vector3.Distance(currPos, startPos) < movementSensitivity && startTime != 0) {
                goingZ = 0;
                startTime = 0f;
                startPos = new Vector3();
                //Debug.Log("finished");
                GetComponent<TextDisplayer>().changeText("Mouvement 4");
            }
            goingZ = -1;
        }

        lastPos = currPos;
    }
}
