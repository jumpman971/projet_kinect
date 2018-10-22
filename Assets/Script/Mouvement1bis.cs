using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement1bis : MonoBehaviour
{
    private Vector3 lastPos;
    private Vector3 currPos;
    private int goingX = 0;
    private float startTime;
    private Vector2 startPos;
    private bool started;
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

        if (mh.isAMovementInProgress() && !mh.isMyMovementInProgress(moveId)) {
            Debug.Log("stopped move " + moveId);
            return;
        } else if (state != 0 && !mh.isMyMovementInProgress(moveId))
            state = 0;
        
        Mouvement m = GetComponent<Mouvement>();
        int goingRightX = m.goingRight[Mouvement.AXE_X];

        if (state == 0) {
            if (!action && (goingRightX == 0 || goingRightX == -1)) {
                state = 1;
                startPos = m.currPosRight;
                startTime = Time.time;
                if (!mh.startMovement(moveId))
                    return;
                action = true;
            }
        } else if (state == 1) {
            if (action) {
                mh.startTimeoutCountdown();
                action = false;
            }
            if (goingRightX == 1 && Vector2.Distance(m.currPosRight, startPos) > m.minMove) {
                state = 2;
                action = true;
            }
        } else if (state == 2) {
            if (action) {
                mh.startTimeoutCountdown();
                action = false;
            }
            if (goingRightX == -1 && Vector2.Distance(m.currPosRight, startPos) < m.movementSensitivity) {
                state = 3;
                action = true;
            }
        } else if (state == 3) {
            if (action) {
                GetComponent<TextDisplayer>().changeText("Mouvement " + moveId);
                action = false;
                mh.endMovement(moveId, true);
            } else
                state = 0;
        }



        /*
        if (currPos.x > lastPos.x && diff >= sensitivity) {
            //Debug.Log("to right");
            if (goingX == -1 || goingX == 0) {
                //Debug.Log("left to right");
                startPos = currPos;
                startTime = Time.time;
                mh.startMovement(moveId);
            }
            goingX = 1;
        } else if (currPos.x < lastPos.x && diff >= sensitivity) {
            //Debug.Log("to left");
            if (goingX == 1) {
                //Debug.Log("right to left");
                //Debug.Log(Vector2.Distance(currPos, startPos));
                if (Vector2.Distance(currPos, startPos) < movementSensitivity) {
                    goingX = 0;
                    startTime = 0f;
                    startPos = new Vector2();
                    started = false;
                    //Debug.Log("faux mouvement "+moveId);
                    mh.endMovement(moveId);
                }
            }
            if (Vector2.Distance(currPos, startPos) < movementSensitivity && startTime != 0) {
                goingX = 0;
                startTime = 0f;
                startPos = new Vector2();
                started = false;
                Debug.Log("finished move " + moveId);
                mh.endMovement(moveId, true);
                GetComponent<TextDisplayer>().changeText("Mouvement " + moveId);
            }
            goingX = -1;
        }

        lastPos = currPos;*/
    }
}
