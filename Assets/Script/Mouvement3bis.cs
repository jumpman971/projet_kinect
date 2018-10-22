using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement3bis : MonoBehaviour
{
    private Vector3 lastPosRight;
    private Vector3 currPosRight;
    private int goingYRight = 0;
    private Vector2 startPosRight;

    private Vector3 lastPosLeft;
    private Vector3 currPosLeft;
    private int goingYLeft = 0;
    private Vector2 startPosLeft;
    private int moveId = 3;

    public int state = 0;
    public bool action = false;

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
        
        int goingRightY = mh.goingRight[MouvementHandler.AXE_Y];
        int goingLeftY = mh.goingLeft[MouvementHandler.AXE_Y];

        if (state == 0) {
            //if (!action && ((goingRightY == 0 && goingLeftY == 0) || (goingRightY == -1 && goingLeftY == -1))) {
            if (!action && ((goingRightY == -1 && goingLeftY == -1))) {
                if (mh.startMovement(moveId))
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
            if (goingRightY == 1 && goingLeftY == 1 && Vector2.Distance(mh.currPosRight, startPosRight) > mh.minMove && Vector2.Distance(mh.currPosLeft, startPosLeft) > mh.minMove) {
                state = 2;
                action = true;
            } else if (mh.GetMouvementTimeout())
                state = 0;
        } else if (state == 2) {
            if (action) {
                mh.startTimeoutCountdown();
                action = false;
            }
            if (goingRightY == -1 && goingLeftY == -1 && Vector2.Distance(mh.currPosRight, startPosRight) < mh.movementSensitivity && Vector2.Distance(mh.currPosLeft, startPosLeft) < mh.movementSensitivity) {
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
