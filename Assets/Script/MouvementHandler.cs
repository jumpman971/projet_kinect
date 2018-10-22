using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouvementHandler : MonoBehaviour {
    public bool activateMouvementHandler;
    public float timeout;
    public Text countdownTextObject;

    public int movementInProgress;
    private float nextTimeout;

    private int lastMovementId = 0;
    private float getMovementTimeOut = 1;
    private float startTimeNewMovement;
    private float startTimeCountdown;
    private int nbSecLeft;
    private string countdownText = "Redémarrage de la détection dans ";
    public float countDownDelay;

    // Use this for initialization
    void Start () {
        movementInProgress = 0;
        countdownTextObject.enabled = false;

    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > nextTimeout)
            movementInProgress = 0;
        if (nbSecLeft >= 0 && startTimeCountdown + countDownDelay < Time.time) {
            nbSecLeft--;
            if (nbSecLeft < 0) {
                countdownTextObject.enabled = false;
                return;
            }
            countdownTextObject.text = countdownText + nbSecLeft + " sec";
            startTimeCountdown = Time.time;
        }
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
            startDetectionCountDown(3);
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
            startDetectionCountDown(3);
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

    public void startDetectionCountDown(int sec)
    {
        startTimeCountdown = Time.time;
        nbSecLeft = sec;
        countdownTextObject.text = countdownText + nbSecLeft +" sec";
        countdownTextObject.enabled = true;
    }
}
