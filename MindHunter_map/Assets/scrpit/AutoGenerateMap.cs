using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AutoGenerateMap : MonoBehaviour{
    public MapConfig config;
    public GameObject tilemaps;
    public Tilemap wallTilemap;
    public TileBase wallTile;
    public Tilemap roomTilemapPrefab;
    public List<Tilemap> roomTilemaps;
    public TileBase roomTile;
    public Tilemap lobbyTilemap;
    public TileBase lobbyTile;
    public Tilemap doorTilemapPrefab;
    public TileBase doorVerticalTile;
    public TileBase doorHorizontalTile;
    public TileUtility tileUtility;

	// Use this for initialization
	void Start () {
        int mapSizeRow = 17;
        int mapSizeColumn = 17;
        int startX = -8;
        int startY = 8;
        int roomNum = 16;
        config = new MapConfig(4,3);
        tileUtility = new TileUtility();
        tilemaps = GameObject.Find("Tilemap");
        wallTilemap = GameObject.FindWithTag("wallTile").GetComponent<Tilemap>();
        lobbyTilemap = GameObject.FindWithTag("lobbyTile").GetComponent<Tilemap>();
        autoGenerate();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //判断地图里的房间是否在不考虑门的情况下全部互相通达
    public bool isRoomAllConnection(List<Room> roomList){
        //对每一个房间检查是否能够通达到其他任意房间，只要有一间房间不满足该条件，则返回false
        foreach (var room in roomList)
        {
            room.addConnectRooms(roomList);
        }
        foreach (var room in roomList){
            if(room.connectID != 1){
                return false;
            }
        }
        return true;
    }
    public List<LobbyConnection> autoGenerateRoomConnection()
    {
        //int[] theRoom = { theRoomR, theRoomC };
        List<LobbyConnection> lobbyList = new List<LobbyConnection>();
        List<Room> oneLobbyRooms = config.roomList.FindAll(x => x.lobbyNum == 1);
        while (!isRoomAllConnection(config.roomList))
        {
            //初始化
            int theRoomR = 1;
            int theRoomC = 1;
            int roomId = 1;
            lobbyList = new List<LobbyConnection>();
            foreach(var room in config.roomList){
                room.connectID = room.ID;
            }
            while (theRoomR <= config.rowNum)
            {
                while (theRoomC <= config.columnNum)
                {
                    Room theRoom = config.roomList.Find(x => x.ID == roomId);
                    bool bottomGenerateFlag = false;
                    bool rightGenerateFlag = false;
                    if (Random.value * 3 > 2)
                    {
                        bottomGenerateFlag = true;
                        rightGenerateFlag = true;
                    }
                    else if (Random.value * 3 > 1)
                    {
                        rightGenerateFlag = true;
                    }
                    else
                    {
                        bottomGenerateFlag = true;
                    }
                    // romdomly generate bottom lobby
                    if (theRoomR + 1 <= config.rowNum)
                    {
                        if (bottomGenerateFlag)
                        {
                            lobbyList.Add(new LobbyConnection(theRoomR * 2, theRoomC));
                            Room theBottomRoom = config.roomList.Find(x => x.ID == roomId + config.columnNum);
                            //theRoom.addConnectRoom(theBottomRoom);
                            if (theRoom.connectID < theBottomRoom.connectID)
                            {
                                List<Room> theConnectedRooms = config.roomList.FindAll(x => x.connectID == theBottomRoom.connectID);
                                foreach (var connectedRoom in theConnectedRooms)
                                {
                                    connectedRoom.connectID = theRoom.connectID;
                                }
                                theBottomRoom.connectID = theRoom.connectID;
                            }
                            if (theRoom.connectID > theBottomRoom.connectID)
                            {
                                List<Room> theConnectedRooms = config.roomList.FindAll(x => x.connectID == theRoom.connectID);
                                foreach (var connectedRoom in theConnectedRooms)
                                {
                                    connectedRoom.connectID = theBottomRoom.connectID;
                                }
                                theRoom.connectID = theBottomRoom.connectID;
                            }
                        }
                    }
                    // romdomly generate right lobby
                    if (theRoomC + 1 <= config.columnNum)
                    {
                        if (rightGenerateFlag)
                        {
                            lobbyList.Add(new LobbyConnection(theRoomR * 2 - 1, theRoomC));
                            Room theRightRoom = config.roomList.Find(x => x.ID == roomId + 1);
                            //theRoom.addConnectRoom(theRightRoom);
                            if (theRoom.connectID < theRightRoom.connectID)
                            {
                                List<Room> theConnectedRooms = config.roomList.FindAll(x => x.connectID == theRightRoom.connectID);
                                foreach (var connectedRoom in theConnectedRooms)
                                {
                                    connectedRoom.connectID = theRoom.connectID;
                                }
                                theRightRoom.connectID = theRoom.connectID;
                            }
                            if (theRoom.connectID > theRightRoom.connectID)
                            {
                                List<Room> theConnectedRooms = config.roomList.FindAll(x => x.connectID == theRoom.connectID);
                                foreach (var connectedRoom in theConnectedRooms)
                                {
                                    connectedRoom.connectID = theRightRoom.connectID;
                                }
                                theRoom.connectID = theRightRoom.connectID;
                            }
                        }
                    }
                    theRoomC += 1;
                    roomId += 1;
                }
                theRoomC = 1;
                theRoomR += 1;
            }
        }

        Debug.Log(config.roomList.Count);
        foreach(var room in config.roomList){
            Debug.Log("roomID");
            Debug.Log(room.ID);
            Debug.Log("connectID");
            Debug.Log(room.connectID);
            Debug.Log("connectRoomID");
            foreach (var connectedRoom in room.connectRooms){
                Debug.Log(connectedRoom.ID);
            }
        }

        //autoGenerateLobbyTile(config, lobbyList);
        return lobbyList;
    }

    public void autoGenerateLobbyTile(List<LobbyConnection> lobbyList)
    {
        //clear all
        for (int r = -(config.mapSizeRow - 1) / 2; r <= (config.mapSizeRow - 1) / 2; r++)
        {
            for (int c = (config.mapSizeColumn - 1) / 2; c >= -(config.mapSizeColumn - 1) / 2; c--)
            {
                tileUtility.changeToReplaceTile(lobbyTilemap, new Vector3Int(r, c, 0), null);
            }
        }
        foreach (var lobby in lobbyList)
        {
            int lobbyPositionX = 0;
            int lobbyPositionY = 0;

            lobbyPositionY = (config.mapSizeColumn - 1) / 2 - lobby.lobbyR * (Mathf.FloorToInt(config.roomScale / 2) + 1);
            if (lobby.lobbyR % 2 == 1)
            {
                lobbyPositionX = -(config.mapSizeRow - 1) / 2 + lobby.lobbyC * (config.roomScale + 1);
            }
            else
            {
                lobbyPositionX = -(config.mapSizeRow - 1) / 2 + lobby.lobbyC * (config.roomScale + 1) - (Mathf.FloorToInt(config.roomScale / 2) + 1);
            }

            Vector3Int lobbyPosition = new Vector3Int(lobbyPositionX, lobbyPositionY, 0);
            tileUtility.changeToReplaceTile(lobbyTilemap, lobbyPosition, lobbyTile, true);
            tileUtility.changeToReplaceTile(wallTilemap, lobbyPosition, null, true);
        }
    }

    public void autoGenerateWallTile(){
        //先将整个地图铺满墙瓷砖
        for (int r = -(config.mapSizeRow - 1) / 2; r <= (config.mapSizeRow - 1) / 2; r++)
        {
            for (int c = (config.mapSizeColumn - 1) / 2; c >= -(config.mapSizeColumn - 1) / 2; c--)
            {
                tileUtility.changeToReplaceTile(wallTilemap, new Vector3Int(r, c, 0), wallTile, true);
            }
        }

    }

    public void autoGenerateRoomTile()
    {
        //再根据行列数生成全部房间，把房间所在处的墙瓷砖从墙图层里删去
        int roomID = 1;
        for (int r = 0; r < config.rowNum; r++)
        {
            for (int c = 0; c < config.columnNum; c++)
            {
                var room = Instantiate(roomTilemapPrefab, tilemaps.transform);
                room.name = "roomTilemap" + (r + 1).ToString() + "_" + (c + 1).ToString();
                int roomCenterLocX = 0;
                int roomCenterLocY = 0;

                roomCenterLocX = -(config.mapSizeRow - 1) / 2 + Mathf.FloorToInt(config.roomScale / 2) + 1 + c * (config.roomScale + 1);
                roomCenterLocY = (config.mapSizeColumn - 1) / 2 - Mathf.FloorToInt(config.roomScale / 2) - 1 - r * (config.roomScale + 1);

                //Vector3Int roomCenterLoc = new Vector3Int(roomCenterLocX, roomCenterLocY, 0);
                for (int tileX = roomCenterLocX - 1; tileX <= roomCenterLocX + 1; tileX++)
                {
                    for (int tileY = roomCenterLocY - 1; tileY <= roomCenterLocY + 1; tileY++)
                    {
                        Vector3Int roomTileLoc = new Vector3Int(tileX, tileY, 0);
                        Tilemap roomTilemap = room.GetComponent<Tilemap>();

                        tileUtility.changeToReplaceTile(roomTilemap, roomTileLoc, roomTile, true);
                        tileUtility.changeToReplaceTile(wallTilemap, roomTileLoc, null, true);
                    }
                }
                config.roomList.Add(new Room(roomID, r + 1, c + 1));
                roomID += 1;
            }
        }
    }

    public int countRoomLobbyNum()
    {
        for (int r = 0; r < config.rowNum; r++)
        {
            for (int c = 0; c < config.columnNum; c++)
            {
                int roomCenterLocX = 0;
                int roomCenterLocY = 0;
                int roomId = r * config.columnNum + (c + 1);
                Room theRoom = config.roomList.Find(x => x.ID == roomId);
                theRoom.lobbyNum = 0;

                roomCenterLocX = -(config.mapSizeRow - 1) / 2 + Mathf.FloorToInt(config.roomScale / 2) + 1 + c * (config.roomScale + 1);
                roomCenterLocY = (config.mapSizeColumn - 1) / 2 - Mathf.FloorToInt(config.roomScale / 2) - 1 - r * (config.roomScale + 1);

                theRoom.roomCenterLocX = roomCenterLocX;
                theRoom.roomCenterLocY = roomCenterLocY;
                Vector3Int rightLobbyPos = new Vector3Int(roomCenterLocX + Mathf.FloorToInt(config.roomScale / 2) + 1, roomCenterLocY, 0);
                Vector3Int leftLobbyPos = new Vector3Int(roomCenterLocX - (Mathf.FloorToInt(config.roomScale / 2) + 1), roomCenterLocY, 0);
                Vector3Int upLobbyPos = new Vector3Int(roomCenterLocX, roomCenterLocY + Mathf.FloorToInt(config.roomScale / 2) + 1, 0);
                Vector3Int downLobbyPos = new Vector3Int(roomCenterLocX, roomCenterLocY - (Mathf.FloorToInt(config.roomScale / 2) + 1), 0);

                if(tileUtility.isTileExistInPos(lobbyTilemap, rightLobbyPos)){
                    theRoom.lobbyNum += 1;
                    theRoom.roomRight = 0;
                }
                if (tileUtility.isTileExistInPos(lobbyTilemap, leftLobbyPos))
                {
                    theRoom.lobbyNum += 1;
                    theRoom.roomLeft = 0;
                }
                if (tileUtility.isTileExistInPos(lobbyTilemap, upLobbyPos))
                {
                    theRoom.lobbyNum += 1;
                    theRoom.roomUp = 0;
                }
                if (tileUtility.isTileExistInPos(lobbyTilemap, downLobbyPos))
                {
                    theRoom.lobbyNum += 1;
                    theRoom.roomDown = 0;
                }
            }
        }
        List<Room> oneLobbyrooms = config.roomList.FindAll(x => x.lobbyNum == 1);
        return oneLobbyrooms.Count;
    }


    public void autoGenerateDoors(){
        List<Room> rooms = config.roomList.FindAll(x => x.lobbyNum == 1);
        Room bossRoom = rooms[Mathf.FloorToInt(Random.Range(0.0f, 0.99f) * rooms.Count)];
        bossRoom.isBossRoom = true;
        foreach (var room in rooms)
        {
            int doorR = 0;
            int doorC = 0;
            Vector3Int doorPos = new Vector3Int();
            TileBase doorTile = null;
            if(true)//(room.isBossRoom == true)
            {
                if (room.roomRight == 0)
                {
                    doorPos = new Vector3Int(room.roomCenterLocX + (Mathf.FloorToInt(config.roomScale / 2) + 1), room.roomCenterLocY, 0);
                    doorTile = doorVerticalTile;
                    doorR = 2 * room.row - 1;
                    doorC = room.column;
                    room.roomRight = 2;
                }
                if (room.roomLeft == 0)
                {
                    doorPos = new Vector3Int(room.roomCenterLocX - (Mathf.FloorToInt(config.roomScale / 2) + 1), room.roomCenterLocY, 0);
                    doorTile = doorVerticalTile;
                    doorR = 2 * room.row - 1;
                    doorC = room.column - 1;
                    room.roomLeft = 2;
                }
                if (room.roomUp == 0)
                {
                    doorPos = new Vector3Int(room.roomCenterLocX, room.roomCenterLocY + (Mathf.FloorToInt(config.roomScale / 2) + 1), 0);
                    doorTile = doorHorizontalTile;
                    doorR = 2 * (room.row - 1);
                    doorC = room.column;
                    room.roomUp = 2;
                }
                if (room.roomDown == 0)
                {
                    doorPos = new Vector3Int(room.roomCenterLocX, room.roomCenterLocY - (Mathf.FloorToInt(config.roomScale / 2) + 1), 0);
                    doorTile = doorHorizontalTile;
                    doorR = 2 * room.row;
                    doorC = room.column;
                    room.roomDown = 2;
                }
            }
            var door = Instantiate(doorTilemapPrefab, tilemaps.transform);
            door.name = "doorTilemap" + doorR.ToString() + "_" + doorC.ToString();
            Tilemap doorTilemap = door.GetComponent<Tilemap>();
            tileUtility.changeToReplaceTile(doorTilemap, doorPos, doorTile, true);
        }
    }

    public void autoGenerate(){
        autoGenerateWallTile();
        autoGenerateRoomTile();
        do
        {
            List<LobbyConnection> lobbyList = autoGenerateRoomConnection();
            autoGenerateLobbyTile(lobbyList);
            countRoomLobbyNum();
        } while (countRoomLobbyNum() == 0);
        autoGenerateDoors();
        int[,][] connectionList = config.getRoomConnectionList();
        float[] testLoc = config.getRoomCenterLocWithRandC(2, 3);
        Debug.Log(testLoc[0]);
        Debug.Log(testLoc[1]);
        int[] testRoom = config.getRoomRandCWithRoomLoc(new Vector2(testLoc[0], testLoc[1]));
        Debug.Log(testRoom[0]);
        Debug.Log(testRoom[1]);
    }
}
