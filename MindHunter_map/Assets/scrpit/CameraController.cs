using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Transform playerTransform;
    public PlayerController playerController;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");//.GetComponent<PlayerCountroller>();
        playerController = player.GetComponent<PlayerController>();
        playerTransform = player.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void LateUpdate()
    {
        if(player == null){
            player = GameObject.FindGameObjectWithTag("Player");//.GetComponent<PlayerCountroller>();
            playerController = player.GetComponent<PlayerController>();
            playerTransform = player.transform;
        }
        if (playerController.attatchTo != null)
        {
            playerTransform = playerController.attatchTo.transform;
        }
        else
        {
            playerTransform = player.transform;

        }
        this.gameObject.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, -10);
    }
}