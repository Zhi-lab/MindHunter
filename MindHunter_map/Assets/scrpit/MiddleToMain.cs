using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MiddleToMain : MonoBehaviour {
    AsyncOperation asyncOperation;
	public GameObject Text;  
    public Image loadBar;
	float displayProgress = 0.0f;
	//this for initialization
	void Start () {
        Text.GetComponent<Text>().text = "Loading...Something is changing";
		StartCoroutine(startLoad("AutoGenerateScene"));
    }
    
    //IEnumerator loadScene() {
        //yield return asyncOperation = SceneManager.LoadSceneAsync("AutoGenerateScene");
        //return asyncOperation = SceneManager.LoadSceneAsync("AutoGenerateScene");
    //}
    IEnumerator startLoad(string sceneName)
    {
        displayProgress = 0.00f;
        int targetProgress = 0;
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        ao.allowSceneActivation = false;

        while (ao.progress < 0.5f)
        {
            targetProgress = (int)(ao.progress * 100);
            while (displayProgress < targetProgress)
            {
                displayProgress += 0.1f;
                //Debug.Log(displayProgress);
                //percentText.text = displayProgress + "%";
                loadBar.fillAmount = (float)displayProgress / 100;
                yield return new WaitForEndOfFrame();
            }

            //yield return new WaitForEndOfFrame();
        }
        while (ao.progress >= 0.5f && ao.progress < 0.9f)
        {
            targetProgress = (int)(ao.progress * 100);
            while (displayProgress < targetProgress)
            {
                displayProgress += 0.02f;
                //Debug.Log(displayProgress);
                //percentText.text = displayProgress + "%";
                loadBar.fillAmount = (float)displayProgress / 100;
                yield return new WaitForEndOfFrame();
            }

            //yield return new WaitForEndOfFrame();
        }
        //Debug.Log(targetProgress);
        targetProgress = 100;
        while (displayProgress < targetProgress)
        {
            displayProgress++;
            //Debug.Log(displayProgress);
            //percentText.text = displayProgress + "%";
			loadBar.fillAmount = (float)displayProgress / 100;
            yield return new WaitForEndOfFrame();
        }
        Text.GetComponent<Text>().text = "Something has occured. Are you ready?";
        yield return new WaitForEndOfFrame();
        ao.allowSceneActivation = true;
    }
	
	// Update is called once per frame
	void Update () {
		//float progress = (float)asyncOperation.progress * 100 + 10;
		if(displayProgress > 1)
		    Text.GetComponent<Text>().text = "loading..." + displayProgress.ToString() + "%";
	}
}