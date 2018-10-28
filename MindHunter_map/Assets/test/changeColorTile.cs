using UnityEngine;
using UnityEngine.Tilemaps;

// Tile that displays a Sprite when it is alone and a different Sprite when it is orthogonally
[CreateAssetMenu]
public class changeColorTile : TileBase
{
	public Sprite spriteA;
    public Sprite spriteB;
    private int counter = 0;

    void Update(){
        Debug.Log(counter);
    	counter += 1;
    }

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        Vector3Int location = new Vector3Int(position.x, position.y, position.z);
        if (counter / 2 == 0){
        	tilemap.RefreshTile(location);
        }else{
        	tilemap.RefreshTile(location);
        }
    }
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        if (counter / 2 == 0){
        	tileData.sprite = spriteA;
        }else
        {
        	tileData.sprite = spriteB;
        }
    }
 }