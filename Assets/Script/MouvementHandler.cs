using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementHandler : MonoBehaviour {

    private int movementInProgress;

	// Use this for initialization
	void Start () {
        movementInProgress = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool startMovement(int movementIndex)
    {
        if (movementInProgress == 0) {
            movementInProgress = movementIndex;
            return true;
        }

        return false;
    }

    public bool endMovement(int movementIndex)
    {
        if (movementInProgress == movementIndex) {
            movementInProgress = 0;
            return true;
        }

        return false;
    }
}
