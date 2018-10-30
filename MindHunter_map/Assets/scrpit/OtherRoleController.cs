using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OtherRoleController : PlayerController {

    private bool pressed = false;
    TileUtility tileUtility = new TileUtility();
    private Tilemap doorTileMap;
    public TileBase passTile;
    public TileBase unpassTile;
    GameObject player;
    PlayerController playerController;
    public  Vector2Int initTarget;
    public  Vector2Int stay;
    Vector3Int RoomPos;
    private statusBarController statusBarController;
    protected new void Start()
    {
        //doorTileMap = GameObject.FindGameObjectWithTag("doorTile").GetComponent<Tilemap>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        //UI
        statusBarController = GameObject.Find("statusBar").GetComponent<statusBarController>();
        base.Start();
    }

    void release()
    {
        playerController.enabled = true;
        playerController.attatchTo = null;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        if (statusBarController != null)
        {
            statusBarController.setPlayerIcon(true);
            statusBarController.setButtons();
        }
    }
    //	Update is called once per frame
    void FixedUpdate()
    {
        if (statusBarController == null)
        {
            statusBarController = GameObject.Find("statusBar").GetComponent<statusBarController>();
        }
        if (playerController.attatchTo == gameObject)
        {
            move();
            if (Input.GetKeyDown(KeyCode.C))
            {
                pressed = true;
            }
            if (Input.GetKeyUp(KeyCode.C) || (!Input.GetKey(KeyCode.C) && pressed == true))
            {
                release();
                pressed = false;
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (tag == "servant" && collider.tag == "doorTile" && playerController.attatchTo != gameObject&& tileUtility.getAvatarPosInTilemap(transform.position)!=RoomPos)
        {

            var doorTileMap = collider.gameObject.GetComponent<Tilemap>();
            collider.gameObject.GetComponent<CompositeCollider2D>().isTrigger = false;
            tileUtility.changeToReplaceTile(doorTileMap, RoomPos, unpassTile);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "servant" && collision.collider.tag == "doorTile")
        {
            var doorTileMap = collision.collider.gameObject.GetComponent<Tilemap>();
            collision.collider.gameObject.GetComponent<CompositeCollider2D>().isTrigger = true;
            var contact = collision.GetContact(0).point;
            var pos = tileUtility.getAvatarPosInTilemap(2 * new Vector3(contact.x, contact.y) - transform.position);

            RoomPos = pos;
            tileUtility.changeToReplaceTile(doorTileMap, pos, passTile);
        }

        if (playerController.attatchTo== gameObject)
        {
           
            if (tag == "fighter" && (collision.collider.tag == "boss" || collision.collider.tag == "servant" || collision.collider.tag == "fighter"))
            {
                Destroy(collision.collider.gameObject);
                release();
            }
        }
    }
}
