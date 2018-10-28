using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement4 : MonoBehaviour
{
    private Vector2 startPos;
    private int moveId = 4;

    public float movementSensitivity; //0.05
    public float minMove; //0.05
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
        } else if (state != 0 && !mh.isMyMovementInProgress(moveId))
            state = 0;*/

        int goingRightZ = mh.goingRight[MouvementHandler.AXE_Z];

        if (state == 0) {
            //if (!action && (goingRightZ == 0 || goingRightZ == -1)) {
            if (!action && goingRightZ == 1) {

                state = 1;
                startPos = mh.currPosRight;
                action = true;
            }
        } else if (state == 1) {
            if (action) {
                mh.startTimeoutCountdown();
                action = false;
            }
            if (goingRightZ == 1 && Vector2.Distance(mh.currPosRight, startPos) > minMove) {
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
            if (goingRightZ == -1 && Vector2.Distance(mh.currPosRight, startPos) < movementSensitivity) {
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
