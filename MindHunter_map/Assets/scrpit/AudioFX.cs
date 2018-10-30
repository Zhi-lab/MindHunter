using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFX : MonoBehaviour {
    public AudioSource openTheDoor;
    public AudioSource servantDead;
    public AudioSource fighterDead;
    public AudioSource useSkill;
    public AudioSource fighterAttack;
    public AudioSource switchTarget;
    public AudioSource Win;
    public AudioSource Lose;
    public AudioSource clickButton;
    public AudioSource gameBGM;
    public AudioSource startBGM;
    public AudioSource Scream;
    public AudioSource Alert;
    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this.gameObject);
	}
    public void openthedoor() {
        openTheDoor.Play();
    }
    public void searvantdead() {
        servantDead.Play();
    }
    public void fighterdead() {
        fighterDead.Play();
    }
    public void useskill() {
        useSkill.Play();
    }
    public void fighterattack(){
        fighterAttack.Play();
    }
    public void switchtarget() {
        switchTarget.Play();
    }
    public void win() {
        Win.Play();
    }
    public void lose() {
        Lose.Play();
    }
    public void clickbutton() {
        clickButton.Play();
    }

    public void startbgm() {
        startBGM.Play();
    }

    public void gamebgm() {
        gameBGM.Play();
    }

    public void alert(){
        gameBGM.Stop();
        Scream.Play();
        Alert.Play();
    }
    // Update is called once per frame
    void Update () {
		
	}

}
