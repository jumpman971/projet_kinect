using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement6 : MonoBehaviour
{
    public GameObject rightHand;
    public float startSensitivity;
    public float sensitivity;
    public float movementSensitivity;

    private Vector3 lastPos;
    private Vector3 currPos;
    private int goingY = 0;
    private float startTime;
    private Vector2 startPos;
    private bool started;
    private int moveId = 6;

    // Use this for initialization
    void Start()
    {
        lastPos = rightHand.transform.position;
        started = false;
    }

    // Update is called once per frame
    void Update()
    {
        currPos = rightHand.transform.position;

        float diff = Mathf.Abs(currPos.y - lastPos.y);

        //Debug.Log(currPos + " - " + lastPos);

        MouvementHandler mh = GetComponent<MouvementHandler>();

        if (mh.isAMovementInProgress() && !mh.isMyMovementInProgress(moveId)) {
            Debug.Log("stopped move " + moveId);
            return;
        }

        if (currPos.y > lastPos.y && diff >= sensitivity) {
            //Debug.Log("to up");
            if (goingY == -1) {
                //Debug.Log("up to down");
                //Debug.Log(Vector2.Distance(currPos, startPos));
                if (Vector2.Distance(currPos, startPos) < movementSensitivity) {
                    goingY = 0;
                    startTime = 0f;
                    startPos = new Vector2();
                    started = false;
                    //Debug.Log("faux mouvement "+moveId);
                    mh.endMovement(moveId);
                }
            }
            if (Vector2.Distance(currPos, startPos) < movementSensitivity && startTime != 0) {
                goingY = 0;
                startTime = 0f;
                startPos = new Vector2();
                started = false;
                Debug.Log("finished move " + moveId);
                mh.endMovement(moveId, true);
                GetComponent<TextDisplayer>().changeText("Mouvement " + moveId);
            }
            goingY = 1;
        } else if (currPos.y < lastPos.y && diff >= sensitivity) {
            //Debug.Log("to down");
            if (goingY == 1 || goingY == 0) {
                //Debug.Log("down to up");
                startPos = currPos;
                startTime = Time.time;
                mh.startMovement(moveId);
            }
            goingY = -1;
        }

        lastPos = currPos;
    }
}
