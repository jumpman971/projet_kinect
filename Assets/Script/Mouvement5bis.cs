using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement5bis : MonoBehaviour
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

    public int state = 0;
    public bool action = false;

    private float startTime;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MouvementHandler mh = GetComponent<MouvementHandler>();
        
        if (mh.isAMovementInProgress() && !mh.isMyMovementInProgress(moveId)) {
            Debug.Log("stopped move " + moveId);
            return;
        } else if (state != 0 && !mh.isMyMovementInProgress(moveId))
            state = 0;

        Mouvement m = GetComponent<Mouvement>();
        int goingRightY = m.goingRight[Mouvement.AXE_Y];
        int goingLeftY = m.goingLeft[Mouvement.AXE_Y];
        
        if (state == 0) {
            if (!action && ((goingRightY == 0 && goingLeftY == 0) || (goingRightY == 1 && goingLeftY == -1) || (goingRightY == -1 && goingLeftY == 1))) {
                state = 1;
                startPosRight = m.currPosRight;
                startPosLeft = m.currPosLeft;
                startTime = Time.time;

                if (mh.startMovement(moveId))
                    return;
                action = true;
            }
        } else if (state == 1) {
            if (action) {
                mh.startTimeoutCountdown();
                action = false;
            }
            if ( (goingRightY == 1 && goingLeftY == 0) || (goingRightY == 1 && goingLeftY == -1) && Vector2.Distance(m.currPosRight, startPosRight) > m.minMove) {
                state = 2;
               action = true;
            } else if ( ((goingRightY == 0 && goingLeftY == 1) || (goingRightY == -1 && goingLeftY == 1)) && Vector2.Distance(m.currPosLeft, startPosLeft) > m.minMove) {
                state = 3;
                action = true;
            }
        } else if (state == 2) {
            if (action) {
                mh.startTimeoutCountdown();
                action = false;
            }
            if (goingRightY == -1 && goingLeftY == 1 && Vector2.Distance(m.currPosLeft, startPosLeft) > m.minMove) {
                state = 4;
                action = true;
            }
        } else if (state == 3) {
            if (action) {
                mh.startTimeoutCountdown();
                action = false;
            }
            if (goingRightY == 1 && goingLeftY == -1 && Vector2.Distance(m.currPosRight, startPosRight) > m.minMove) {
                state = 5;
                action = true;
            }
        } else if (state == 4) {
            if (action) {
                mh.startTimeoutCountdown();
                action = false;
            }
            if (goingRightY == 1 && goingLeftY == -1 && Vector2.Distance(m.currPosRight, startPosRight) > m.minMove) {
                state = 6;
                action = true;
            }
        } else if (state == 5) {
            if (action) {
                mh.startTimeoutCountdown();
                action = false;
            }
            if (goingRightY == -1 && goingLeftY == 1 && Vector2.Distance(m.currPosLeft, startPosLeft) > m.minMove) {
                state = 6;
                action = true;
            }
        } else if (state == 6) {
            if (action) {
                GetComponent<TextDisplayer>().changeText("Mouvement " + moveId);
                action = false;
                mh.endMovement(moveId, true);
            } else
                state = 0;
        }
    }
}
