using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement6 : MonoBehaviour
{
    private Vector2 startPos;
    private int moveId = 6;

    public float movementSensitivity; //0.1
    public float minMove; //0.1
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
        } else if (state != 0 && !mh.isMyMovementInProgress(moveId)) {
            state = 0;
            action = false;
        }*/

        int goingRightY = mh.goingRight[MouvementHandler.AXE_Y];
        //int goingLeftY = mh.goingLeft[MouvementHandler.AXE_Y];

        if (state == 0) {
            //if (!action && (goingRightY == 0 || goingRightY == 1)) {
            if (!action && (goingRightY == -1)) {
                

                state = 1;
                startPos = mh.currPosRight;
                action = true;
            }
        } else if (state == 1) {
            if (action) {
                mh.startTimeoutCountdown();
                action = false;
            }
            if (goingRightY == -1 && Vector2.Distance(mh.currPosRight, startPos) > minMove) {
                if (!mh.startMovement(moveId))
                    return;
                state = 2;
                action = true;
            } else if (mh.GetMouvementTimeout())
                state = 0;
        } else if (state == 2) {
            if (action) {
                mh.startTimeoutCountdown();
                action = false;
            }
            if (goingRightY == 1 && Vector2.Distance(mh.currPosRight, startPos) < movementSensitivity) {
                state = 3;
                action = true;
            } else if (mh.GetMouvementTimeout())
                state = 0;
        } else if (state == 3) {
            if (action) {
                GetComponent<TextDisplayer>().changeText("Mouvement " + moveId);
                mh.endMovement(moveId, true);
                action = false;
            } else
                state = 0;
        }
    }
}
