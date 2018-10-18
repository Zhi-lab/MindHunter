using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileUtility : MonoBehaviour
{
    /*
    public TileBase[] replaceTiles;
    public TileBase hideTile;
    public GameObject player;
    public Transform playerTransform;
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
        playerTransform = player.transform;
        shadeTilemap = GameObject.FindGameObjectWithTag("shadeTile").GetComponent<Tilemap>();
        //测试
        //changeToReplaceTile(tilemap, player.GetComponent<PlayerCountroller>().getPlayerPosInTilemap(), replaceTiles[0]);
    }

    void Update()
    {
        //阴影效果
        hideInvisibleTiles(shadeTilemap, getAvatarPosInTilemap(playerTransform.position));
    }
    */
    /*
     * changeToReplaceTile函数的作用是将指定tilemap上的指定网格位置上的tile进行替换，如果isHide = ture, 表示更改对象是阴影层tilemap
     * 参数：tilemap指定要作用的tilemap，position指定要更改的网格位置，replaceTile用于替换的tile，isHide该函数当前作用的tilemap是不是shadeTilemap
     */
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
            }
        }
    }

    /*
     * hideInvisibleTiles函数的作用是将角色视野外的地图用黑色tile覆盖，从而实现阴影效果
     * 参数：shadeTilemap遮罩层的tilemap，hideTile用于遮蔽的tile, position角色在网格中的坐标，config地图配置，viewSize角色视野
    */
    public void hideInvisibleTiles(Tilemap shadeTilemap, TileBase hideTile, Vector3Int position, MapConfig config, int viewSize)
    {
        int rowCounter = 0;
        int colomnCounter = 0;
        int checkRow = config.startY;
        int checkColomn = config.startX;
        while (rowCounter < config.mapSizeRow)
        {
            colomnCounter = 0;
            checkColomn = config.startX;
            while (colomnCounter < config.mapSizeColumn)
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
     * 参数：position角色GameObject的位置
     * 返回值：player GameObject的位置在tilemap中的位置
     * 示例：player GameObject的位置为(-1.5,-2.8,0)，则所对应的tilemap的位置为(-2,-3,0)
    */
    public Vector3Int getAvatarPosInTilemap(Vector3 position)
    {
        int x = Mathf.FloorToInt(position.x);
        int y = Mathf.FloorToInt(position.y);
        int z = Mathf.FloorToInt(position.z);
        Vector3Int posInTilemap = new Vector3Int(x, y, z);
        return posInTilemap;
    }
}
