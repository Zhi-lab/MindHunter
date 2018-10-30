using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartToMiddle : MonoBehaviour {
    GameObject music;
	// Use this for initialization
	void Start () {
        music = GameObject.Find("AudioController");
        music.GetComponent<AudioFX>().startbgm();
	}
    public void switchToMiddle() {
        music.GetComponent<AudioFX>().clickbutton();
        SceneManager.LoadScene("MiddleScene");
        music.GetComponent<AudioFX>().startBGM.Stop();
    }
    public void switchToGuide(){
        music.GetComponent<AudioFX>().clickbutton();
        SceneManager.LoadScene("GuideScene");
    }
	// Update is called once per frame
	void Update () {
		
	}
}
