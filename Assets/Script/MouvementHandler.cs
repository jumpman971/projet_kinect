using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementHandler : MonoBehaviour {
    public bool activateMouvementHandler;
    public float timeout;

    public int movementInProgress;
    private float nextTimeout;

	// Use this for initialization
	void Start () {
        movementInProgress = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > nextTimeout)
            movementInProgress = 0;

        //mouvement1
        /*int moveId = 1;
        if (isAMovementInProgress() && !isMyMovementInProgress(moveId))
        {
            Debug.Log("stopped move " + moveId);
            return;
        }
        
        if (goingX == -1 || goingX == 0)
        {
            //Debug.Log("left to right");
            startPos = currPos;
            startTime = Time.time;
            startMovement(moveId);
        }
        if (goingX == 1)
        {
        //Debug.Log("right to left");
        //Debug.Log(Vector2.Distance(currPos, startPos));
        if (Vector2.Distance(currPos, startPos) < movementSensitivity
        {
            goingX = 0;
            startTime = 0f;
                    startPos = new Vector2();
                    started = false;
                    //Debug.Log("faux mouvement "+moveId);
                    mh.endMovement(moveId);
                }
            }
            if (Vector2.Distance(currPos, startPos) < movementSensitivity && startTime != 0)
            {
                goingX = 0;
                startTime = 0f;
                startPos = new Vector2();
                started = false;
                Debug.Log("finished move " + moveId);
                mh.endMovement(moveId);
                GetComponent<TextDisplayer>().changeText("Mouvement " + moveId);
            }*/

    }

    public bool startMovement(int movementIndex)
    {
        if (!activateMouvementHandler)
            return false;

        if (movementInProgress == 0) {
            nextTimeout = Time.time + timeout;
            movementInProgress = movementIndex;
            return true;
        }

        return false;
    }

    public bool isMyMovementInProgress(int movementIndex)
    {
        if (!activateMouvementHandler)
            return false;

        if (movementInProgress == movementIndex)
        { 
            return true;
        }

        return false;
    }

    public bool isAMovementInProgress()
    {
        if (!activateMouvementHandler)
            return false;

        if (movementInProgress != 0)
        {
            return true;
        }

        return false;
    }

    public bool endMovement(int movementIndex)
    {
        if (!activateMouvementHandler)
            return false;

        if (movementInProgress == movementIndex) {
            movementInProgress = 0;
            return true;
        }

        return false;
    }
}
