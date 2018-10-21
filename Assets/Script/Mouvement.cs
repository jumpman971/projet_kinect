using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;
    public float sensitivity;
    public float movementSensitivity;
    public float minMove;

    public Vector3 lastPosRight;
    public Vector3 currPosRight;
    private Vector2 startPosRight;

    public Vector3 lastPosLeft;
    public Vector3 currPosLeft;
    private Vector2 startPosLeft;

    public int[] goingRight = { 0, 0, 0 };
    public int[] goingLeft = { 0, 0, 0 };

    public static int AXE_X = 0;
    public static int AXE_Y = 1;
    public static int AXE_Z = 2;

    /*public int goingRightX = 0;
    public int goingRightY = 0;
    public int goingRightZ = 0;
    public int goingLeftX = 0;
    public int goingLeftY = 0;
    public int goingLeftZ = 0;*/

    private float startTime;

    // Use this for initialization
    void Start()
    {
        lastPosRight = rightHand.transform.position;
        lastPosLeft = leftHand.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currPosRight = rightHand.transform.position;
        currPosLeft = leftHand.transform.position;

        updateHands();

        lastPosRight = currPosRight;
        lastPosLeft = currPosLeft;
    }

    void updateHands()
    {
        //####RightHand####
        ArrayList currRightPosAxes = new ArrayList();
        currRightPosAxes.Add(currPosRight.x); currRightPosAxes.Add(currPosRight.y); currRightPosAxes.Add(currPosRight.z);
        ArrayList lastRightPosAxes = new ArrayList();
        lastRightPosAxes.Add(lastPosRight.x); lastRightPosAxes.Add(lastPosRight.y); lastRightPosAxes.Add(lastPosRight.z);

        for (int i = 0; i < 3; ++i)
        {
            float currPosAxe = (float) currRightPosAxes[i]; float lastPosAxe = (float) lastRightPosAxes[i];
            float diff = Mathf.Abs(currPosAxe - lastPosAxe);

            if (currPosAxe > lastPosAxe && diff >= sensitivity)
                goingRight[i] = 1;
            else if (currPosAxe < lastPosAxe && diff >= sensitivity)
                goingRight[i] = -1;
        }

        //####LeftHand####
        ArrayList currLeftPosAxes = new ArrayList();
        currLeftPosAxes.Add(currPosLeft.x); currLeftPosAxes.Add(currPosLeft.y); currLeftPosAxes.Add(currPosLeft.z);
        ArrayList lastLeftPosAxes = new ArrayList();
        lastLeftPosAxes.Add(lastPosLeft.x); lastLeftPosAxes.Add(lastPosLeft.y); lastLeftPosAxes.Add(lastPosLeft.z);

        for (int i = 0; i < 3; ++i) {
            float currPosAxe = (float)currLeftPosAxes[i]; float lastPosAxe = (float)lastLeftPosAxes[i];
            float diff = Mathf.Abs(currPosAxe - lastPosAxe);

            if (currPosAxe > lastPosAxe && diff >= sensitivity) {
                goingLeft[i] = 1;
            } else if (currPosAxe < lastPosAxe && diff >= sensitivity) {
                goingLeft[i] = -1;
            }
        }
    }
}
