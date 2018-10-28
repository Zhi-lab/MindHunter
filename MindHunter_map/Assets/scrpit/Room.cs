using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room {
    //房间id是按从左往右，从上到下，从1开始数，比如最左上角是房间1，第一排第二列是房间2，第一排第三列是房间3
    public int ID;
    public int row;
    public int column;
    public int roomCenterLocX;
    public int roomCenterLocY;
    public int connectID;
    public List<Room> connectRooms;
    public int lobbyNum;
    //房间右方的元素，0表示是通路，1表示是墙壁，2表示是门，初始值为墙1
    public int roomRight;
    public int roomLeft;
    public int roomUp;
    public int roomDown;
    //该房间是否是Boss房间，默认值是false
    public bool isBossRoom;
    //该房间初始状态是谁在，0表示没人，1 = player, 2 = servant, 3 = fighter, 4 = boss;
    public int roomType;

    public Room(int _ID, int _row, int _column){
        this.ID = _ID;
        this.connectID = _ID;
        this.row = _row;
        this.column = _column;
        this.connectRooms = new List<Room>();
        this.lobbyNum = 0;
        this.roomRight = 1;
        this.roomLeft = 1;
        this.roomDown = 1;
        this.roomUp = 1;
        this.isBossRoom = false;
        this.roomCenterLocX = 0;
        this.roomCenterLocY = 0;
    }

    public void addConnectRooms(List<Room> roomList)
    {
        foreach(var room in roomList){
            if(room != this && room.connectID == this.connectID){
                connectRooms.Add(room);
            }
        }
    }
}