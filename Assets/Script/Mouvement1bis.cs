using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement1bis : MonoBehaviour
{
    private Vector3 lastPos;
    private Vector3 currPos;
    private int goingX = 0;
    private Vector2 startPos;
    private int moveId = 1;

    public int state;
    public bool action;

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
        } else if (state != 0 && !mh.isMyMovementInProgress(moveId))
            state = 0;*/
        
        int goingRightX = mh.goingRight[MouvementHandler.AXE_X];

        if (state == 0) {
            //if (!action && (goingRightX == 0 || goingRightX == -1)) {
            if (!action && (goingRightX == -1)) {
                if (!mh.startMovement(moveId))
                    return;
                state = 1;
                startPos = mh.currPosRight;
                action = true;
            }
        } else if (state == 1) {
            if (action) {
                mh.startTimeoutCountdown();
                action = false;
            }
            if (goingRightX == 1 && Vector2.Distance(mh.currPosRight, startPos) > mh.minMove) {
                state = 2;
                action = true;
            } else if (mh.GetMouvementTimeout())
                state = 0;
        } else if (state == 2) {
            if (action) {
                mh.startTimeoutCountdown();
                action = false;
            }
            if (goingRightX == -1 && Vector2.Distance(mh.currPosRight, startPos) < mh.movementSensitivity) {
                state = 3;
                action = true;
            } else if (mh.GetMouvementTimeout())
                state = 0;
        } else if (state == 3) {
            if (action) {
                GetComponent<TextDisplayer>().changeText("Mouvement " + moveId);
                action = false;
                mh.endMovement(moveId, true);
            } else
                state = 0;
        }
    }
}
