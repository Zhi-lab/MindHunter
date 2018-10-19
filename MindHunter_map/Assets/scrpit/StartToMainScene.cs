using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartToMainScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void SwitchToMain() {
        SceneManager.LoadScene("mainScene");
    }

	// Update is called once per frame
	void Update () {
		
	}
}
