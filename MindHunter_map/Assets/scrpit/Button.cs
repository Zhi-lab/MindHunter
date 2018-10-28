using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Button : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{

	// Use this for initialization
	void Start () {

     }


    // Update is called once per frame
    void Update () {
		
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.transform.localScale = new Vector3(1.4f, 1.4f, 1);
        this.GetComponentInChildren<Text>().text = "<color=#FF0000>开始游戏</color>";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.localScale = new Vector3(1f, 1f, 1);
        this.GetComponentInChildren<Text>().text = "<color=#8E8B8B>开始游戏</color>";
    }
}
