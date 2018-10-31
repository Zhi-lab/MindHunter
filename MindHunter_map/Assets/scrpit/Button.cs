using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    string defaultText;
    // Use this for initialization
    void Start()
    {
        defaultText = this.GetComponentInChildren<Text>().text;
        //this.GetComponentInChildren<Text>().text = "<color=#831010>" + defaultText + "</color>";
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //   this.transform.localScale = new Vector3(1.4f, 1.4f, 1);
        this.GetComponentInChildren<Text>().text = "<color=#831010>" + defaultText + "</color>";
        //this.GetComponentInChildren<Text>().text = "<color=#831010>" + text + "</color>";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.localScale = new Vector3(1f, 1f, 1);
        this.GetComponentInChildren<Text>().text = "<color=#831010>" + defaultText + "</color>";
    }
}
