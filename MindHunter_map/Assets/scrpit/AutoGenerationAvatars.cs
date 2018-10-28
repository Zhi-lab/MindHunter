using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGenerationAvatars{
    public GameObject player;
    public GameObject servant;
    public GameObject fighter;
    public GameObject boss;
    public int servantNum;
    public int fighterNum;
    public MapConfig config;

    public AutoGenerationAvatars(int _servantNum, int _fighterNum, GameObject _player, GameObject _servant, GameObject _fighter, GameObject _boss, MapConfig _config)
    {
        this.servantNum = _servantNum;
        this.fighterNum = _fighterNum;
        this.player = _player;
        this.servant = _servant;
        this.fighter = _fighter;
        this.boss = _boss;
        this.config = _config;
    }

    public void generateBoss(){
        Room boosRoom = config.roomList.Find(x => x.isBossRoom == true);
        if(boosRoom != null){
            boosRoom.roomType = 4;
            Vector3 bossPos = new Vector3(boosRoom.roomCenterLocX + 0.5f, boosRoom.roomCenterLocY + 0.5f, 0f);
            Quaternion bossRotation = new Quaternion(0f, 0f, 0f, 0f);
            GameObject.Instantiate(boss, bossPos, bossRotation);
        }
    }

    public void generatePlayer(){
        List<Room> playerRooms = config.roomList.FindAll(x => x.lobbyNum >= 3);
        Room playerRoom = playerRooms[Mathf.FloorToInt(Random.Range(0.0f, 0.99f) * playerRooms.Count)];
        if (playerRoom.roomType == 0)
        {
            playerRoom.roomType = 1;
            Vector3 playerPos = new Vector3(playerRoom.roomCenterLocX + 0.5f, playerRoom.roomCenterLocY + 0.5f, 0f);
            Quaternion playerRotation = new Quaternion(0f, 0f, 0f, 0f);
            GameObject.Instantiate(player, playerPos, playerRotation);
        }
    }

    public void generateServants()
    {
        List<Room> servantRooms = config.roomList.FindAll(x => x.lobbyNum >= 2);
        int i = 0;
        while(i < servantNum){
            Room servantRoom = servantRooms[Mathf.FloorToInt(Random.Range(0.0f, 0.99f) * servantRooms.Count)];
            if (servantRoom.roomType == 0)
            {
                servantRoom.roomType = 1;
                Vector3 servantPos = new Vector3(servantRoom.roomCenterLocX + 0.5f, servantRoom.roomCenterLocY + 0.5f, 0f);
                Quaternion servantRotation = new Quaternion(0f, 0f, 0f, 0f);
                GameObject.Instantiate(servant, servantPos, servantRotation);
                servantRooms.Remove(servantRoom);
                i += 1;
            }
        }
    }

    public void generateFighters()
    {
        List<Room> fighterRooms = config.roomList.FindAll(x => x.lobbyNum >= 2);
        int i = 0;
        while (i < fighterNum)
        {
            Room fighterRoom = fighterRooms[Mathf.FloorToInt(Random.Range(0.0f, 0.99f) * fighterRooms.Count)];
            if (fighterRoom.roomType == 0)
            {
                fighterRoom.roomType = 1;
                Vector3 fighterPos = new Vector3(fighterRoom.roomCenterLocX + 0.5f, fighterRoom.roomCenterLocY + 0.5f, 0f);
                Quaternion fighterRotation = new Quaternion(0f, 0f, 0f, 0f);
                GameObject.Instantiate(fighter, fighterPos, fighterRotation);
                fighterRooms.Remove(fighterRoom);
                i += 1;
            }
        }
    }

}
