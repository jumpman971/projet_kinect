using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement2 : MonoBehaviour
{
    public GameObject rightHand;
    public float sensitivity;
    public float movementSensitivity;

    private Vector3 lastPos;
    private Vector3 currPos;
    private int goingX = 0;
    private float startTime;
    private Vector2 startPos;

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
                    //Debug.Log("faux mouvement");
                    if (!GetComponent<MouvementHandler>().endMovement(2))
                        return;
                }
            }
            if (Vector2.Distance(currPos, startPos) < movementSensitivity && startTime != 0)
            {
                goingX = 0;
                startTime = 0f;
                startPos = new Vector2();
                //Debug.Log("finished");
                GetComponent<TextDisplayer>().changeText("Mouvement 2");
                if (!GetComponent<MouvementHandler>().endMovement(2))
                    return;
            }
            goingX = 1;
        } else if (currPos.x < lastPos.x && diff >= sensitivity)
        {
            //Debug.Log("to left");
            if (goingX == 1)
            {
                //Debug.Log("right to left");
                //Debug.Log(Vector2.Distance(currPos, startPos));
                startTime = Time.time;
                startPos = currPos;
            } else {
                if (!GetComponent<MouvementHandler>().startMovement(2))
                    return;
            }
            goingX = -1;
        }

        lastPos = currPos;
    }
}
