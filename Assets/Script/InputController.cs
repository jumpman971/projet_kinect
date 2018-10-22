using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape) || GetComponent<MouvementHandler>().GetMouvement(6))
        {
            SceneManager.LoadScene("MainMenu");
        }

        /*if (Input.GetKeyDown(KeyCode.A)) {
            GetComponent<MouvementHandler>().startDetectionCountDown(3);
        }*/
	}
}
