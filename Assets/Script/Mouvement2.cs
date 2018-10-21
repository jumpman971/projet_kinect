﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement2 : MonoBehaviour
{
    public GameObject rightHand;
    public float startSensitivity;
    public float sensitivity;
    public float movementSensitivity;

    private Vector3 lastPos;
    private Vector3 currPos;
    private int goingX = 0;
    private float startTime;
    private Vector2 startPos;
    private int moveId = 2;
    
    // Use this for initialization
    void Start()
    {
        lastPos = rightHand.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currPos = rightHand.transform.position;

        float diff = Mathf.Abs(currPos.x - lastPos.x);

        //Debug.Log(currPos + " - " + lastPos);
        MouvementHandler mh = GetComponent<MouvementHandler>();

        if (mh.isAMovementInProgress() && !mh.isMyMovementInProgress(moveId))
        {
            Debug.Log("stopped move "+moveId);
            return;
        }

        if (currPos.x > lastPos.x && diff >= sensitivity)
        {
            //Debug.Log("to right");
            if (goingX == -1)
            {
                //Debug.Log("left to right");
                if (Vector2.Distance(currPos, startPos) < movementSensitivity)
                {
                    goingX = 0;
                    startTime = 0f;
                    startPos = new Vector2();
                    Debug.Log("faux mouvement "+moveId);
                    mh.endMovement(moveId);
                }
            }
            if (Vector2.Distance(currPos, startPos) < movementSensitivity && startTime != 0)
            {
                goingX = 0;
                startTime = 0f;
                startPos = new Vector2();
                Debug.Log("finished move "+moveId);
                GetComponent<TextDisplayer>().changeText("Mouvement "+moveId);
                mh.endMovement(moveId);
            }
            goingX = 1;
        } else if (currPos.x < lastPos.x && diff >= sensitivity)
        {
            //Debug.Log("to left");
            if (goingX == 1 || goingX == 0)
            {
                //Debug.Log("right to left");
                //Debug.Log(Vector2.Distance(currPos, startPos));
                //startTime = Time.time;
                startPos = currPos;
                mh.startMovement(moveId);
                //started = true;
                startTime = Time.time;
            }
            goingX = -1;
        }

        lastPos = currPos;
    }
}
