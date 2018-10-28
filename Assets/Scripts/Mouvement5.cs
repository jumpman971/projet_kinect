using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement5 : MonoBehaviour
{
    private Vector3 lastPosRight;
    private Vector3 currPosRight;
    private Vector2 startPosRight;

    private Vector3 lastPosLeft;
    private Vector3 currPosLeft;
    private Vector2 startPosLeft;
    private int moveId = 5;
    
    public float minMove; //0.2

    private int state;
    private bool action;

    // Use this for initialization
    void Start()
    {
        state = 0;
        action = false;
    }

    // Update is called once per frame
    void Update()
    {
        MouvementHandler mh = GetComponent<MouvementHandler>();

        /*if (mh.isAMovementInProgress() && !mh.isMyMovementInProgress(moveId)) {
            Debug.Log("stopped move " + moveId);
            return;
        } 
        if (state != 0 && !mh.isMyMovementInProgress(moveId)) {
            state = 0;
            action = false;
        }*/
    
        int goingRightY = mh.goingRight[MouvementHandler.AXE_Y];
        int goingLeftY = mh.goingLeft[MouvementHandler.AXE_Y];
        
        if (state == 0) {
            //if (!action && ((goingRightY == 0 && goingLeftY == 0) || (goingRightY == 1 && goingLeftY == -1) || (goingRightY == -1 && goingLeftY == 1))) {
            if (!action && ((goingRightY == 1 && goingLeftY == -1) || (goingRightY == -1 && goingLeftY == 1))) {
                if (!mh.startMovement(moveId))
                    return;
                state = 1;
                startPosRight = mh.currPosRight;
                startPosLeft = mh.currPosLeft;
                action = true;
            }
        } else if (state == 1) {
            if (action) {
                mh.startTimeoutCountdown();
                action = false;
            }
            if ((goingRightY == 1 && goingLeftY == 0) || (goingRightY == 1 && goingLeftY == -1) && Vector2.Distance(mh.currPosRight, startPosRight) > minMove) {
                state = 2;
                action = true;
            } else if (((goingRightY == 0 && goingLeftY == 1) || (goingRightY == -1 && goingLeftY == 1)) && Vector2.Distance(mh.currPosLeft, startPosLeft) > minMove) {
                state = 3;
                action = true;
            } else if (mh.GetMouvementTimeout())
                state = 0;
        } else if (state == 2) {
            if (action) {
                mh.startTimeoutCountdown();
                action = false;
            }
            if (goingRightY == -1 && goingLeftY == 1 && Vector2.Distance(mh.currPosLeft, startPosLeft) > minMove) {
                state = 4;
                action = true;
            } else if (mh.GetMouvementTimeout())
                state = 0;
        } else if (state == 3) {
            if (action) {
                mh.startTimeoutCountdown();
                action = false;
            }
            if (goingRightY == 1 && goingLeftY == -1 && Vector2.Distance(mh.currPosRight, startPosRight) > minMove) {
                state = 5;
                action = true;
            } else if (mh.GetMouvementTimeout())
                state = 0;
        } else if (state == 4) {
            if (action) {
                mh.startTimeoutCountdown();
                action = false;
            }
            if (goingRightY == 1 && goingLeftY == -1 && Vector2.Distance(mh.currPosRight, startPosRight) > minMove) {
                state = 6;
                action = true;
            } else if (mh.GetMouvementTimeout())
                state = 0;
        } else if (state == 5) {
            if (action) {
                mh.startTimeoutCountdown();
                action = false;
            }
            if (goingRightY == -1 && goingLeftY == 1 && Vector2.Distance(mh.currPosLeft, startPosLeft) > minMove) {
                state = 6;
                action = true;
            } else if (mh.GetMouvementTimeout())
                state = 0;
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
