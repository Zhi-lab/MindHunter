using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tileUtility : MonoBehaviour
{
    public TileBase[] replaceTiles;
    public TileBase hideTile;
    public GameObject player;
    //mapSizeRow，mapSizeColumn均为测试用参数
    int mapSizeRow = 17;
    int mapSizeColumn = 17;
    int startX = -8;
    int startY = 8;
    int viewSize = 5;
    private Tilemap shadeTilemap;
    private Tilemap[] tilemaps;

    void Start()
    {
        //tilemap = GetComponent<Tilemap>();
        player = GameObject.FindGameObjectWithTag("Player");//.GetComponent<PlayerCountroller>();
        shadeTilemap = GameObject.FindGameObjectWithTag("shadeTile").GetComponent<Tilemap>();
        //测试
        //changeToReplaceTile(tilemap, player.GetComponent<PlayerCountroller>().getPlayerPosInTilemap(), replaceTiles[0]);
    }
    void Update()
    {
        hideInvisibleTiles(shadeTilemap, player.GetComponent<PlayerCountroller>().getPlayerPosInTilemap());
    }
    public void changeToReplaceTile(Tilemap tilemap, Vector3Int position, TileBase replaceTile, bool isHide = false)
    {
        TileBase changedtile = tilemap.GetTile(position);
        //如果替换对象是来自于阴影层，则直接替换
        if (isHide)
        {
            tilemap.SetTile(position, replaceTile);
            //Debug.Log("Change tile in this position");
        }
        //如果替换对象不是来自于阴影层，则先判断该图层是否本身有tile，如果有的话才执行替换
        else
        {
            if (changedtile != null)
            {
                tilemap.SetTile(position, replaceTile);
                //Debug.Log("Change tile in this position");
            }
            else
            {
                //Debug.Log("No tile in this position");
            }
        }
    }
    public void hideInvisibleTiles(Tilemap shadTilemap, Vector3Int position)
    {
        int rowCounter = 0;
        int colomnCounter = 0;
        int checkRow = startY;
        int checkColomn = startX;
        while (rowCounter < mapSizeRow)
        {
            colomnCounter = 0;
            checkColomn = startX;
            while (colomnCounter < mapSizeColumn)
            {
                if (Mathf.Abs(checkColomn - position.x) > viewSize || Mathf.Abs(checkRow - position.y) > viewSize)
                {
                    changeToReplaceTile(shadeTilemap, new Vector3Int(checkColomn, checkRow, 0), hideTile, true);
                }
                else
                {
                    changeToReplaceTile(shadeTilemap, new Vector3Int(checkColomn, checkRow, 0), null, true);
                }
                colomnCounter += 1;
                checkColomn += 1;
            }
            rowCounter += 1;
            checkRow -= 1;
        }
    }

    /*
    public GameObject theTilemapG;
    private Tilemap theTilemap;
    public TileBase replaceTile;
    
    // Use this for initialization
    void Start () {
        theTilemapG = GameObject.FindGameObjectWithTag("tilemap");
        theTilemap = theTilemapG.GetComponent<Tilemap>();
        FindReplaceableTilesInTilemap(theTilemap);
    }

    // Update is called once per frame
    void Update () {
        
    }
    private void FindReplaceableTilesInTilemap(Tilemap tilemap)
    {
        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(position);
            if (tile != null)
            {
                HandleReplaceTile(tilemap, tile, position);
            }
        }
    }
    private void HandleReplaceTile(Tilemap tilemap, TileBase tile, Vector3Int position)
    {

        tilemap.SetTile(position, replaceTile);
            
    }
    */

}
