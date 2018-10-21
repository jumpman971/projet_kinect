using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement4 : MonoBehaviour
{
    public GameObject rightHand;
    public float sensitivity;
    public float movementSensitivity;

    private Vector3 lastPos;
    private Vector3 currPos;
    private int goingZ = 0;
    private float startTime;
    private Vector2 startPos;
    private int moveId = 4;

    // Use this for initialization
    void Start()
    {
        lastPos = rightHand.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currPos = rightHand.transform.position;

        float diff = Mathf.Abs(currPos.z - lastPos.z);

        //Debug.Log(currPos + " - " + lastPos);
        MouvementHandler mh = GetComponent<MouvementHandler>();

        if (mh.isAMovementInProgress() && !mh.isMyMovementInProgress(moveId))
        {
            Debug.Log("stopped move "+ moveId);
            return;
        }

        if (currPos.z > lastPos.z && diff >= sensitivity) {
            //Debug.Log("to right");
            if (goingZ == -1) {
                //Debug.Log("left to right");
                startTime = Time.time;
                startPos = currPos;
                mh.startMovement(moveId);
            }
            goingZ = 1;
        } else if (currPos.z < lastPos.z && diff >= sensitivity) {
            //Debug.Log("to left");
            if (goingZ == 1) {
                //Debug.Log("right to left");
                //Debug.Log(Vector2.Distance(currPos, startPos));
                if (Vector2.Distance(currPos, startPos) < movementSensitivity) {
                    goingZ = 0;
                    startTime = 0f;
                    startPos = new Vector2();
                    //Debug.Log("faux mouvement");
                    mh.endMovement(moveId);
                }
            }
            if (Vector2.Distance(currPos, startPos) < movementSensitivity && startTime != 0) {
                goingZ = 0;
                startTime = 0f;
                startPos = new Vector2();
                //Debug.Log("finished");
                mh.endMovement(moveId, true);
                GetComponent<TextDisplayer>().changeText("Mouvement "+ moveId);
            }
            goingZ = -1;
        }

        lastPos = currPos;
    }
}
