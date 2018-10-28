using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OtherRoleController : PlayerController {

    private bool pressed = false;
    TileUtility tileUtility = new TileUtility();
    private Tilemap doorTileMap;
    public TileBase passTile;
    protected void Start()
    {
        doorTileMap = GameObject.FindGameObjectWithTag("doorTile").GetComponent<Tilemap>();
        base.Start();
    }

    void release()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var playerController = player.GetComponent<PlayerController>();
        playerController.enabled = true;
        playerController.attatchTo = null;
        enabled = false;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }
    //	Update is called once per frame
    void FixedUpdate()
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enabled)
        {
            if (tag == "servant" && collision.collider.name == "doorTilemap")
            {
                var contact = collision.GetContact(0).point;
                var pos = tileUtility.getAvatarPosInTilemap(2 * new Vector3(contact.x, contact.y) - transform.position);
                tileUtility.changeToReplaceTile(doorTileMap, pos, passTile);
            }

            if (tag == "fighter" && (collision.collider.tag == "boss" || collision.collider.tag == "servant" || collision.collider.tag == "fighter"))
            {
                Destroy(collision.collider.gameObject);
                release();
            }
        }
    }
}
