using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFromKeyboard : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire1"))
            transform.position += new Vector3(0.1f, 0.0f);
	}
}
