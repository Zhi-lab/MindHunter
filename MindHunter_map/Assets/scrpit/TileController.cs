using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; 

public class TileController : MonoBehaviour {

    public TileBase[] replaceTiles;
    public TileBase hideTile;
    public GameObject player;
    public Transform playerTransform;
    public PlayerController playerController;
    //mapSizeRow，mapSizeColumn均为测试用参数
    MapConfig config = new MapConfig(17, 17, -8, 8);
    private Tilemap shadeTilemap;
    private TileUtility tileUtility;


    void Start()
    {
        //tilemap = GetComponent<Tilemap>();
        shadeTilemap = GameObject.FindGameObjectWithTag("shadeTile").GetComponent<Tilemap>();
        tileUtility = new TileUtility();
        player = GameObject.FindGameObjectWithTag("Player");//.GetComponent<PlayerCountroller>();
        playerController = player.GetComponent<PlayerController>();
        playerTransform = player.transform;

        //测试
        //changeToReplaceTile(tilemap, player.GetComponent<PlayerCountroller>().getPlayerPosInTilemap(), replaceTiles[0]);
    }

    void Update()
    {
        int viewSize;
        if(player == null){
            player = GameObject.FindGameObjectWithTag("Player");//.GetComponent<PlayerCountroller>();
            playerController = player.GetComponent<PlayerController>();
            playerTransform = player.transform;
        }
        if(playerController.attatchTo!=null)
        {
            playerTransform = playerController.attatchTo.transform;
            viewSize = playerController.attatchTo.GetComponent<PlayerController>().viewSize;
        }
        else
        {
            playerTransform = player.transform;
            viewSize = playerController.viewSize;
        }
        //阴影效果
        Vector3Int avatarPositionInGrid = tileUtility.getAvatarPosInTilemap(playerTransform.position);
        tileUtility.hideInvisibleTiles(shadeTilemap, hideTile, avatarPositionInGrid, config, viewSize);
    }
}
