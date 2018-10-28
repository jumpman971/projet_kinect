using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement3 : MonoBehaviour
{
    private Vector2 startPosRight;
    private Vector2 startPosLeft;
    private int moveId = 3;

    public float movementSensitivity; //0.1
    public float minMove; //0.2
    private int state;
    private bool action = false;

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
            //Debug.Log("Right Y && Left Y" + ((goingRightY == 1 && goingLeftY == 1) ? "true" : "false"));
            if (!action && ((goingRightY == 1 && goingLeftY == 1))) {
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
            if (goingRightY == 1 && goingLeftY == 1 && Vector2.Distance(mh.currPosRight, startPosRight) > minMove && Vector2.Distance(mh.currPosLeft, startPosLeft) > minMove) {
                if (mh.startMovement(moveId))
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
            Debug.Log("Right = " + Vector2.Distance(mh.currPosRight, startPosRight) + " < " + movementSensitivity + " - Left = " + Vector2.Distance(mh.currPosLeft, startPosLeft) + " < " + movementSensitivity);
            if (goingRightY == -1 && goingLeftY == -1 && Vector2.Distance(mh.currPosRight, startPosRight) < movementSensitivity && Vector2.Distance(mh.currPosLeft, startPosLeft) < movementSensitivity) {
                state = 3;
                action = true;
            } else if (mh.GetMouvementTimeout())
                state = 0;
        } else if (state == 3) {
            if (action) {
                GetComponent<TextDisplayer>().changeText("Mouvement " + moveId);
                action = false;
                mh.endMovement(moveId, true);
            } else {
                state = 0;
            }
        }
    }
}
