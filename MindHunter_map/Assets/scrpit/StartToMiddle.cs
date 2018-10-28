using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartToMiddle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    public void switchToMiddle() {
        SceneManager.LoadScene("MiddleScene");
    }
	// Update is called once per frame
	void Update () {
		
	}
}
