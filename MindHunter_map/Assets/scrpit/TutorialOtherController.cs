using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TutorialOtherController : PlayerController
{

    private bool pressed = false;
    TileUtility tileUtility = new TileUtility();
    private Tilemap doorTileMap;
    MapConfig mapConfig;
    GameObject player;
    PlayerController playerController;
    private Vector2Int target;
    private Vector2Int next;
    private Vector2Int born;
    Vector3Int RoomPos;
    PathFinder pathFinder;
    public Vector2Int scout;
    public TileBase passTile_hori;
    public TileBase unpassTile_hori;
    public TileBase passTile_verti;
    public TileBase unpassTile_verti;
    public double walkPrecision = 0.1;
    statusBarController statusBar;

    public MapConfig config;

    protected new void Start()
    {
        config = new MapConfig();
        born = config.getRoomRandCWithRoomLoc(new Vector2(transform.position.x, transform.position.y)).Value;
        statusBar = GameObject.Find("statusBar").GetComponent<statusBarController>();
        if (tag == "servant")
        {
            target = scout;
            next = born;
        }
        else
        {
            target = born;
            next = born;
        }
        doorTileMap = GameObject.FindGameObjectWithTag("doorTile").GetComponent<Tilemap>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        mapConfig = GameObject.FindObjectOfType<AutoGenerateMap>().config;
        pathFinder = GameObject.FindObjectOfType<ObveserController>().pathFinder;
        base.Start();
    }

    void release()
    {
        GetComponent<Collider2D>().isTrigger = true;
        playerController.enabled = true;
        playerController.attatchTo = null;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        next = mapConfig.getNearestRoomLoc(new Vector2(transform.position.x, transform.position.y));

        //Ui
        statusBar.setPlayerIcon(true);
        statusBar.setButtons();

        //audio
        musicController.switchtarget();
    }
    //  Update is called once per frame
    void FixedUpdate()
    {
        //if(pathFinder == null){
        //pathFinder = GameObject.FindObjectOfType<ObveserController>().pathFinder;
        //}
        if (playerController.attatchTo == gameObject)
        {
            move();
            if (Input.GetKeyDown(KeyCode.C))
            {
                pressed = true;
            }
            if (Input.GetKeyUp(KeyCode.C) || (!Input.GetKey(KeyCode.C) && pressed == true))
            {
                pressed = false;
                release();
            }
            if (tag == "servant" && Input.GetMouseButtonDown(1) && mapConfig.getRoomRandCWithRoomLoc(transform.position) != null)
            {
                GameObject.FindObjectOfType<ObveserController>().alertFighter(mapConfig.getRoomRandCWithRoomLoc(transform.position));
                release();
            }
        }
        else
        {
            var nowRoom = mapConfig.getRoomRandCWithRoomLoc(transform.position);
            if (nowRoom != null)
            {
                if (next != nowRoom)
                {
                    var direction = (mapConfig.getRoomCenterLocWithRandC(nowRoom.Value.x, nowRoom.Value.y) + mapConfig.getRoomCenterLocWithRandC(next.x, next.y));
                    var dir = direction / 2;
                    GetComponent<Rigidbody2D>().velocity = (dir - transform.position).normalized * speed;
                }
                else if (next == nowRoom && (mapConfig.getRoomCenterLocWithRandC(next.x, next.y) - transform.position).sqrMagnitude <= walkPrecision)
                {
                    if (tag == "servant" && nowRoom == target)
                    {
                        if (target == scout)
                            target = born;
                        else if (target == born)
                            target = scout;
                    }
                    var tmp = pathFinder.GoForward(nowRoom.Value, target, tag);
                    next = tmp.HasValue ? tmp.Value : next;
                }
            }
            if (nowRoom == null || (next == nowRoom && (mapConfig.getRoomCenterLocWithRandC(next.x, next.y) - transform.position).sqrMagnitude > walkPrecision))
            {
                GetComponent<Rigidbody2D>().velocity = (mapConfig.getRoomCenterLocWithRandC(next.x, next.y) - transform.position).normalized * speed;
            }
        }
    }
    public void alert(Vector2Int alertRoom)
    {
        target = alertRoom;
        statusBar.setAlarmIcon(true);
    }

    public void unAlert()
    {
        target = born;
        statusBar.setAlarmIcon(false);
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (tag == "servant" && collider.tag == "doorTile" && playerController.attatchTo != gameObject && tileUtility.getAvatarPosInTilemap(transform.position) != RoomPos)
        {
            Debug.Log("close door");
            var doorTileMap = collider.gameObject.GetComponent<Tilemap>();
            if (doorTileMap.GetTile(RoomPos) == passTile_verti)
            {
                if (tileUtility.changeToReplaceTile(doorTileMap, RoomPos, unpassTile_verti))
                {
                    collider.gameObject.GetComponent<CompositeCollider2D>().isTrigger = false;
                }
            }
            if (doorTileMap.GetTile(RoomPos) == passTile_hori)
            {
                if (tileUtility.changeToReplaceTile(doorTileMap, RoomPos, unpassTile_hori))
                {
                    collider.gameObject.GetComponent<CompositeCollider2D>().isTrigger = false;
                }
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (tag == "servant" && collider.tag == "doorTile")
        {
            //musicController.openthedoor();
            Debug.Log("open door");
            var doorTileMap = collider.gameObject.GetComponent<Tilemap>();
            Debug.Log(collider.name);
            //var contact = GetContact(0).point;
            //var pos = tileUtility.getAvatarPosInTilemap(2 * new Vector3(contact.x, contact.y) - transform.position);
            //if (doorTileMap.GetTile(pos) == unpassTile_verti)
            //{
            //    RoomPos = pos;
            //    if (tileUtility.changeToReplaceTile(doorTileMap, pos, passTile_verti))
            //    {
            //        collision.collider.gameObject.GetComponent<CompositeCollider2D>().isTrigger = true;
            //    }
            //}
            //if (doorTileMap.GetTile(pos) == unpassTile_hori)
            //{
            //    RoomPos = pos;
            //    if (tileUtility.changeToReplaceTile(doorTileMap, pos, passTile_hori))
            //    {
            //        collision.collider.gameObject.GetComponent<CompositeCollider2D>().isTrigger = true;
            //    }
            //}
        }

        if (tag == "fighter" && collider.tag == "Player" && playerController.attatchTo != gameObject)
        {

            GameObject.FindObjectOfType<ObveserController>().Lose();
        }

        if (playerController.attatchTo == gameObject)
        {

            if (tag == "fighter" && (collider.tag == "boss" || collider.tag == "servant" || collider.tag == "fighter"))
            {
                Debug.Log("fighter  kill " + gameObject.tag);
                Destroy(collider.gameObject);
                release();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "servant" && collision.collider.tag == "doorTile")
        {
            musicController.openthedoor();
            Debug.Log("open door");
            var doorTileMap = collision.collider.gameObject.GetComponent<Tilemap>();
            var contact = collision.GetContact(0).point;
            var pos = tileUtility.getAvatarPosInTilemap(2 * new Vector3(contact.x, contact.y) - transform.position);
            if (doorTileMap.GetTile(pos) == unpassTile_verti)
            {
                RoomPos = pos;
                if (tileUtility.changeToReplaceTile(doorTileMap, pos, passTile_verti))
                {
                    collision.collider.gameObject.GetComponent<CompositeCollider2D>().isTrigger = true;
                }
            }
            if (doorTileMap.GetTile(pos) == unpassTile_hori)
            {
                RoomPos = pos;
                if (tileUtility.changeToReplaceTile(doorTileMap, pos, passTile_hori))
                {
                    collision.collider.gameObject.GetComponent<CompositeCollider2D>().isTrigger = true;
                }
            }
        }

        if (playerController.attatchTo == gameObject)
        {

            if (tag == "fighter" && (collision.collider.tag == "boss" || collision.collider.tag == "servant" || collision.collider.tag == "fighter"))
            {
                musicController.fighterattack();
                Debug.Log("fighter  kill " + gameObject.tag);
                Destroy(collision.collider.gameObject);
                release();
            }
        }
        if (tag == "fighter" && collision.collider.tag == "Player" && playerController.attatchTo != gameObject)
        {

            GameObject.FindObjectOfType<ObveserController>().Lose();
        }
    }
}
