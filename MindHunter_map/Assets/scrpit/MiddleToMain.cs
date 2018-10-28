using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MiddleToMain : MonoBehaviour {
    AsyncOperation asyncOperation;
    public GameObject Text;
	// Use this for initialization
	void Start () {
        Text.GetComponent<Text>().text = "loading...0%";
        StartCoroutine(loadScene());
    }
    IEnumerator loadScene() {
        yield return asyncOperation = SceneManager.LoadSceneAsync("mainScene");
    }
	// Update is called once per frame
	void Update () {
        Text.GetComponent<Text>().text = "loading..." + (float)asyncOperation.progress * 100 + 10 + "%";
	}
}
