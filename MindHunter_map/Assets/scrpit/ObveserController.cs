using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObveserController : MonoBehaviour {

    private GameObject player;
    private GameObject boss;
    private GameObject[] fighters;
    private GameObject[] servants;
    private TileUtility tileUtility;
    private PathFinder pathFinder;
    public Vector2Int winRoom;
    private void Awake()
    {

        pathFinder = new PathFinder();
        pathFinder.init();
    }
    // Use this for initialization
    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
        boss = GameObject.FindGameObjectWithTag("boss");
        fighters = GameObject.FindGameObjectsWithTag("fighter");
        servants = GameObject.FindGameObjectsWithTag("servant");
        tileUtility = new TileUtility();
        winRoom = new Vector2Int(-2, 1);
    }
    void Lose()
    {
        Destroy(player);
        Debug.Log("lose");
    }
    void Win()
    {
        Destroy(player);
        Debug.Log("win");
    }
    Vector2Int? CalcRoom(Vector2 pos)
    {
        var v2d= new Vector2Int(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y));
        if (v2d.x % 4 == 0 || v2d.y % 4 == 0)
            return null;
        return new Vector2Int(Mathf.FloorToInt( v2d.x/4 ),Mathf.FloorToInt(v2d.y/4));
    }
	// Update is called once per frame
	void Update () {
        var playerRoom = CalcRoom(player.transform.position);
        foreach (var servant in servants)
        {
            if (servant == null) continue;
            if (CalcRoom(servant.transform.position)!=null& CalcRoom(servant.transform.position)== playerRoom)
            {
                //servant.alert(CalcRoom(servant.transform.position));
            }
        }
        foreach (var fighter in fighters)
        {
            if (fighter == null) continue;
            if (CalcRoom(fighter.transform.position) != null & CalcRoom(fighter.transform.position) == playerRoom)
            {
                Lose();
            }
        }
        if(boss==null&& playerRoom== winRoom)
        {
            Win();
        }
    }
}
