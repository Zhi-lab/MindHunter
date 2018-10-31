using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class statusBarController : MonoBehaviour {
    public Image playerIcon;
    public Image controlledEnemyIcon;
    public Image fighterAlarmIcon;
    public Image buttonLeft;
    public Image buttonRight;
    public Image buttonCenter;
    public Sprite playerNormal;
    public Sprite servantNormal;
    public Sprite fighterNormal;
    //public Sprite fighterAngry;
    public Sprite playerUncontroled;
    public Sprite controllIcon;
    public Sprite keyIcon;
    public Sprite alarmIcon;
    public Sprite attackIcon;

    private void Start()
    {
        setPlayerIcon(true);
        controlledEnemyIcon.color = new Color(255,255,255,0);
        fighterAlarmIcon.color = new Color(255, 255, 255, 0);
        setButtons();
    }

    public void setPlayerIcon(bool isActive){
        if (isActive)
        {
            playerIcon.sprite = playerNormal;
            playerIcon.color = new Color(255, 255, 255, 255);
            controlledEnemyIcon.color = new Color(255, 255, 255, 0);
        }
        else{
            playerIcon.sprite = playerUncontroled;
        }
    }

    public void setEnemyIcon(string enemyTag)
    {
        controlledEnemyIcon.enabled = true;
        if (enemyTag == "fighter")
        {
            controlledEnemyIcon.sprite = fighterNormal;
            controlledEnemyIcon.color = new Color(255, 255, 255, 255);
            setPlayerIcon(false);
        }
        if(enemyTag == "servant"){
            controlledEnemyIcon.sprite = servantNormal;
            controlledEnemyIcon.color = new Color(255, 255, 255, 255);
            setPlayerIcon(false);
        }
    }

    public void setAlarmIcon(bool isAlarm){
        if(isAlarm){
            //fighterAlarmIcon.sprite = fighterAngry;
            //fighterAlarmIcon.enabled = true;
            fighterAlarmIcon.color = new Color(255, 255, 255, 255);
        }
        else{
            //fighterAlarmIcon.enabled = false;
            fighterAlarmIcon.color = new Color(255, 255, 255, 255);
        }
    }

    public void setButtons(string controlledAvatarTag = null){
        if(controlledAvatarTag == "fighter")
        {
            buttonCenter.sprite = attackIcon;
            buttonCenter.color = new Color(255, 255, 255, 255);
            buttonLeft.sprite = null;
            buttonLeft.color = new Color(255, 255, 255, 0);
            buttonRight.sprite = null;
            buttonRight.color = new Color(255, 255, 255, 0);
        }
        else if(controlledAvatarTag == "servant"){
            buttonCenter.sprite = null;
            buttonCenter.color = new Color(255, 255, 255, 0);
            buttonLeft.sprite = keyIcon;
            buttonLeft.color = new Color(255, 255, 255, 255);
            buttonRight.sprite = alarmIcon;
            buttonRight.color = new Color(255, 255, 255, 255);
        }
        else{
            buttonCenter.sprite = controllIcon;
            buttonCenter.color = new Color(255, 255, 255, 255);
            buttonLeft.sprite = null; 
            buttonLeft.color = new Color(255, 255, 255, 0);
            buttonRight.sprite = null;
            buttonRight.color = new Color(255, 255, 255, 0);
        }
    }
}
