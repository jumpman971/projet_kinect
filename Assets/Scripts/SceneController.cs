using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {
    public Button mainBtn;
    public Button exitBtn;

    // Use this for initialization
    void Start () {
        mainBtn.onClick.AddListener(loadMainScene);
        exitBtn.onClick.AddListener(exitUnity);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void loadMainScene()
    {
        SceneManager.LoadScene("KinectSample");
    }

    void exitUnity()
    {
        Application.Quit();
    }
}
