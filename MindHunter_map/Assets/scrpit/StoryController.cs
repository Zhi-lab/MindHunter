using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryController : MonoBehaviour {
    public Image storyImg;
    public Sprite[] storyImgList;
    public GameObject player;
    public Vector3 playerPos;
    public TileUtility tileUtility;
    public List<Vector3Int> storyPosList;
    GameObject[] avatars;
    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player");
        //playerPos = player.transform.position;
        tileUtility = new TileUtility();
        storyImg = this.GetComponent<Image>();
        //故事点1
        storyPosList.Add(new Vector3Int(6, -6, 0));
        //故事点2
        storyPosList.Add(new Vector3Int(6, -4, 0));
    }
	
	// Update is called once per frame
	void Update () {
        storyFlow();
    }
    public void storyFlow()
    {

        if (tileUtility.getAvatarPosInTilemap(player.transform.position) == storyPosList[0])
        {
            Debug.Log("story");
            //故事1触发
            Debug.Log("story1");
            hideAvatar("servant");
            hideAvatar("fighter");
            storyImg.sprite = storyImgList[0];
            //storyImg.color = new Color(255, 255, 255, 10);
            //showStory(false);
        }
        else if(tileUtility.getAvatarPosInTilemap(player.transform.position) == storyPosList[1])
        {
            Debug.Log("story1");
            hideAvatar("servant");
            hideAvatar("fighter");
            storyImg.sprite = storyImgList[1];
            //storyImg.color = new Color(255, 255, 255, 100);
            //showStory(false);
        }
        else{
            storyImg.color = new Color(255, 255, 255, 0);
            //showStory(true);
            showAvatar("servant");
            showAvatar("fighter");
        }
        /*
        //故事2触发
        if (tileUtility.getAvatarPosInTilemap(playerPos) == storyPosList[1])
        {
            storyImg.sprite = storyImgList[1];
        }
        //故事1触发
        if (tileUtility.getAvatarPosInTilemap(playerPos) == storyPosList[2])
        {
            storyImg.sprite = storyImgList[2];
        }
        //故事1触发
        if (tileUtility.getAvatarPosInTilemap(playerPos) == storyPosList[3])
        {
            storyImg.sprite = storyImgList[3];
        }
        //故事1触发
        if (tileUtility.getAvatarPosInTilemap(playerPos) == storyPosList[4])
        {
            storyImg.sprite = storyImgList[4];
        }
        //故事1触发
        if (tileUtility.getAvatarPosInTilemap(playerPos) == storyPosList[5])
        {
            storyImg.sprite = storyImgList[5];
        }
        //故事1触发
        if (tileUtility.getAvatarPosInTilemap(playerPos) == storyPosList[6])
        {
            storyImg.sprite = storyImgList[6];
        }
        //故事1触发
        if (tileUtility.getAvatarPosInTilemap(playerPos) == storyPosList[7])
        {
            storyImg.sprite = storyImgList[7];
        }
        */
    }
    public void showStory(bool isOver){
        GameObject[] servants = GameObject.FindGameObjectsWithTag("servant");
        foreach (var servant in servants)
        {
            servant.GetComponent<OtherRoleController>().enabled = !isOver;
        }
        PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController.enabled = !isOver;
    }
    public void hideAvatar(string tagName){
        GameObject[] avatars = GameObject.FindGameObjectsWithTag(tagName);
        foreach (var avatar in avatars)
        {
            avatar.SetActive(false);
        }
    }
    public void showAvatar(string tagName)
    {
        GameObject[] avatars = GameObject.FindGameObjectsWithTag(tagName);
        foreach (var avatar in avatars)
        {
            avatar.SetActive(true);
        }
    }
}
