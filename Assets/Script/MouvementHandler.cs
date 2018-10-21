using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementHandler : MonoBehaviour {
    public bool activateMouvementHandler;
    public float timeout;

    public int movementInProgress;
    private float nextTimeout;

    private int lastMovementId = 0;
    private float getMovementTimeOut = 1;
    private float startTimeNewMovement;

    // Use this for initialization
    void Start () {
        movementInProgress = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > nextTimeout)
            movementInProgress = 0;
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

    public bool endMovement(int movementIndex, bool success)
    {
        if (!activateMouvementHandler)
            return false;

        if (movementInProgress == movementIndex) {
            movementInProgress = 0;
            setLastMovement(movementIndex);
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

    public bool GetMouvement(int movementId)
    {
        if (lastMovementId == movementId && startTimeNewMovement + getMovementTimeOut > Time.time)
            return true;
        return false;
    }

    private void setLastMovement(int movementId)
    {
        lastMovementId = movementId;
        startTimeNewMovement = Time.time;
    }
}
