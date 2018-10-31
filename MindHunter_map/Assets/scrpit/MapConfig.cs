using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapConfig  {
    //mapSizeRow，mapSizeColumn均为测试用参数
    public int mapSizeRow;
    public int mapSizeColumn;
    //最左上角的房间在网格中的X坐标
    public int startX;
    //最左上角的房间在网格中的Y坐标
    public int startY;
    //地图中的房间数
    public int roomNum;
    //每一行的房间数
    public int roomNumEachRow;
    //每一列的房间数
    public int roomNumEachColumn;
    //总共有几行房间
    public int rowNum;
    //总共有几列房间
    public int columnNum;
    //总共有几个锁门
    public int doorNum;
    //房间是正方形，roomScale表示房间的边长的网格数
    public int roomScale;
    //房间列表
    public List<Room> roomList;

    //教学模式用的Map

    public MapConfig()
    {
        this.roomNumEachRow = 4;
        this.roomNumEachColumn = 4;
        this.rowNum = this.roomNumEachColumn;
        this.columnNum = this.roomNumEachRow;
        this.roomNum = this.rowNum * this.columnNum;
        this.doorNum = 2;
        this.roomScale = 3;
        this.mapSizeRow = this.roomNumEachRow * this.roomScale + this.roomNumEachRow + 1;
        this.mapSizeColumn = this.roomNumEachColumn * this.roomScale + this.roomNumEachColumn + 1;
        this.roomList = new List<Room>();
        int[] _roomRight = { 1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 1, 1, 0, 0, 1, 1 };
        int[] _roomDown = { 0, 1, 1, 1, 0, 0, 1, 0, 1, 0, 0, 0, 1, 1, 1, 1 };
        for (int r = 0; r < this.roomNumEachColumn; r++)
        {
            for (int c = 0; c < this.roomNumEachRow; c++)
            {
                int ID = r * this.columnNum + c + 1;
                Room room = new Room(r * this.columnNum + c + 1, r + 1, c + 1, _roomRight[ID-1], _roomDown[ID - 1])
                {
                    roomCenterLocX = -(this.mapSizeRow - 1) / 2 + Mathf.FloorToInt(this.roomScale / 2) + 1 + c * (this.roomScale + 1),
                    roomCenterLocY = (this.mapSizeColumn - 1) / 2 - Mathf.FloorToInt(this.roomScale / 2) - 1 - r * (this.roomScale + 1)
                };
                this.roomList.Add(room);
            }
        }
    }

    public MapConfig(int roomNumEachRow, int roomNumEachColumn)
    {
        this.roomNumEachRow = roomNumEachRow;
        this.roomNumEachColumn = roomNumEachColumn;
        this.rowNum = roomNumEachColumn;
        this.columnNum = roomNumEachRow;
        this.roomList = new List<Room>();
        this.roomNum = rowNum * columnNum;

        //测试用
        this.doorNum = Mathf.FloorToInt(roomNum / 5);
        this.roomScale = 3;

        this.mapSizeRow = roomNumEachRow * roomScale + roomNumEachRow + 1;
        this.mapSizeColumn = roomNumEachColumn * roomScale + roomNumEachColumn + 1;
    }

    public MapConfig(int mapSizeRow, int mapSizeColumn, int startX, int startY)
    {
        this.mapSizeRow = mapSizeRow;
        this.mapSizeColumn = mapSizeColumn;
        this.startX = startX;
        this.startY = startY;
        this.roomList = new List<Room>();
    }
    public MapConfig(int mapSizeRow, int mapSizeColumn, int startX, int startY, int roomNum)
    {
        this.mapSizeRow = mapSizeRow;
        this.mapSizeColumn = mapSizeColumn;
        this.startX = startX;
        this.startY = startY;
        this.roomNum = roomNum;
        this.roomList = new List<Room>();
    }

    //使用该方法返回一个2x2x2矩阵,
    //例子：若想访问第二排的第三个房间，则访问connectionMatrix[2-1,3-1] = [0,1]，第一个0值表示房间右边是通路，第二个1值表示房间下方是墙壁
    public int[,][] getRoomConnectionList(){
        int[,][] connectionMatrix = new int[rowNum,columnNum][];
        foreach(var room in roomList){
            connectionMatrix[room.row - 1, room.column-1] = new int[2] { room.roomRight, room.roomDown };
        }
        return connectionMatrix;
    }

    //根据房间所在行列，返回房间的中心坐标（标准世界坐标）
    public Vector3 getRoomCenterLocWithRandC(int roomRow, int roomColumn){
        Room room = roomList.Find(x => x.ID == (roomRow) * columnNum + roomColumn+1);
        Vector3 roomCenterLoc = new Vector3(room.roomCenterLocX + 0.5f, room.roomCenterLocY + 0.5f,0);
        return roomCenterLoc;
    }

    //根据某个二维坐标值返回该二维坐标值对应的房间行列
    public Vector2Int? getRoomRandCWithRoomLoc(Vector2 position){

        float posX = position.x;
        float posY = position.y;
        List<Room> rooms = roomList.FindAll(x => Mathf.Abs(x.roomCenterLocX - posX+0.5f) < roomScale *0.5 && Mathf.Abs(x.roomCenterLocY - posY + 0.5f) < roomScale *0.5);
        if (rooms.Count == 0)
        {
            Debug.Log("No room");
            return null;
        }
        return new Vector2Int(rooms[0].row-1, rooms[0].column-1);
    }
    public Vector2Int getNearestRoomLoc(Vector3 position)
    {
        float dis = (getRoomCenterLocWithRandC(roomList[0].row, roomList[0].column)-position).magnitude;
        Room nearest = roomList[0];
        foreach(var room in roomList)
        {
            if((getRoomCenterLocWithRandC(room.row-1, room.column-1) - position).magnitude<dis)
            {
                nearest = room;
                dis = (getRoomCenterLocWithRandC(room.row-1, room.column-1) - position).magnitude;
            }
        }
        return new Vector2Int(nearest.row - 1, nearest.column - 1);
    }
}