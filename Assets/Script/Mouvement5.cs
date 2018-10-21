using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement5 : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;
    public float sensitivity;
    public float movementSensitivity;

    private Vector3 lastPosRight;
    private Vector3 currPosRight;
    private int goingYRight = 0;
    private Vector2 startPosRight;

    private Vector3 lastPosLeft;
    private Vector3 currPosLeft;
    private int goingYLeft = 0;
    private Vector2 startPosLeft;
    private int moveId = 5;

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

        float diffRight = Mathf.Abs(currPosRight.y - lastPosRight.y);
        float diffLeft = Mathf.Abs(currPosLeft.y - lastPosLeft.y);

        MouvementHandler mh = GetComponent<MouvementHandler>();

        if (mh.isAMovementInProgress() && !mh.isMyMovementInProgress(moveId))
        {
            Debug.Log("stopped move " + moveId);
            return;
        }

        //Debug.Log(currPos + " - " + lastPos);
        bool rightHandGoesUp = currPosRight.y > lastPosRight.y && diffRight >= sensitivity;
        bool leftHandGoesUp = currPosLeft.y > lastPosLeft.y && diffLeft >= sensitivity;

        bool case1 = (rightHandGoesUp && !leftHandGoesUp);
        bool case2 = (!rightHandGoesUp && leftHandGoesUp);

        if (case1 || case2)  
        {
            //Debug.Log("to top");
            if ( (goingYRight == -1 && goingYLeft == -1) || (goingYRight == 0 && goingYLeft == 0))
            {
                //Debug.Log("bottom to top");
                startTime = Time.time;
                startPosRight = currPosRight;
                startPosLeft = currPosLeft;
                mh.startMovement(moveId);
            }
            if (rightHandGoesUp && !leftHandGoesUp) {
                goingYRight = 1;
                goingYLeft = 0;
            } else if (!rightHandGoesUp && leftHandGoesUp) {
                goingYRight = 0;
                goingYLeft = 1;
            }
        } else 
        {
            bool rightHandGoesDown = currPosRight.y < lastPosRight.y && diffRight >= sensitivity;
            bool leftHandGoesDown = currPosLeft.y < lastPosLeft.y && diffLeft >= sensitivity;

            if (rightHandGoesDown && leftHandGoesUp) {
                //Debug.Log("to bottom");
                if (goingYRight == 1 && goingYLeft == 0) {
                    //Debug.Log("top to bottom");
                    //Debug.Log(Vector2.Distance(currPos, startPos));
                    if (Vector2.Distance(currPosRight, startPosRight) < movementSensitivity && Vector2.Distance(currPosLeft, startPosLeft) < movementSensitivity) {
                        goingYRight = 0;
                        startPosRight = new Vector2();

                        goingYLeft = 0;
                        startPosLeft = new Vector2();

                        startTime = 0f;
                        mh.endMovement(moveId);
                        //Debug.Log("faux mouvement");
                    }
                }
                if (Vector2.Distance(currPosRight, startPosRight) < movementSensitivity && Vector2.Distance(currPosLeft, startPosLeft) < movementSensitivity && startTime != 0) {
                    goingYRight = 0;
                    startPosRight = new Vector2();

                    goingYLeft = 0;
                    startPosLeft = new Vector2();

                    startTime = 0f;
                    //Debug.Log("finished");
                    mh.endMovement(moveId);
                    GetComponent<TextDisplayer>().changeText("Mouvement " + moveId);
                }
                goingYRight = -1;
                goingYLeft = 1;
            } else if (rightHandGoesUp && leftHandGoesDown) {
                //Debug.Log("to bottom");
                if (goingYRight == 0 && goingYLeft == 1) {
                    //Debug.Log("top to bottom");
                    //Debug.Log(Vector2.Distance(currPos, startPos));
                    if (Vector2.Distance(currPosRight, startPosRight) < movementSensitivity && Vector2.Distance(currPosLeft, startPosLeft) < movementSensitivity) {
                        goingYRight = 0;
                        startPosRight = new Vector2();

                        goingYLeft = 0;
                        startPosLeft = new Vector2();

                        startTime = 0f;
                        mh.endMovement(moveId);
                        //Debug.Log("faux mouvement");
                    }
                }
                if (Vector2.Distance(currPosRight, startPosRight) < movementSensitivity && Vector2.Distance(currPosLeft, startPosLeft) < movementSensitivity && startTime != 0) {
                    goingYRight = 0;
                    startPosRight = new Vector2();

                    goingYLeft = 0;
                    startPosLeft = new Vector2();

                    startTime = 0f;
                    //Debug.Log("finished");
                    mh.endMovement(moveId);
                    GetComponent<TextDisplayer>().changeText("Mouvement " + moveId);
                }
                goingYRight = 1;
                goingYLeft = -1;
            }

            /*if (rightHandGoesDown && leftHandGoesDown)
            {
                //Debug.Log("to bottom");
                if (goingYRight == 1 && goingYLeft == 1)
                {
                    //Debug.Log("top to bottom");
                    //Debug.Log(Vector2.Distance(currPos, startPos));
                    if (Vector2.Distance(currPosRight, startPosRight) < movementSensitivity && Vector2.Distance(currPosLeft, startPosLeft) < movementSensitivity)
                    {
                        goingYRight = 0;
                        startPosRight = new Vector2();

                        goingYLeft = 0;
                        startPosLeft = new Vector2();

                        startTime = 0f;
                        mh.endMovement(moveId);
                        //Debug.Log("faux mouvement");
                    }
                }
                if (Vector2.Distance(currPosRight, startPosRight) < movementSensitivity && Vector2.Distance(currPosLeft, startPosLeft) < movementSensitivity && startTime != 0)
                {
                    goingYRight = 0;
                    startPosRight = new Vector2();

                    goingYLeft = 0;
                    startPosLeft = new Vector2();

                    startTime = 0f;
                    //Debug.Log("finished");
                    mh.endMovement(moveId);
                    GetComponent<TextDisplayer>().changeText("Mouvement " + moveId);
                }
                goingYRight = -1;
                goingYLeft = -1;
            }*/
        }

        lastPosRight = currPosRight;
        lastPosLeft = currPosLeft;
    }
}
