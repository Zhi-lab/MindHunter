using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObveserController : MonoBehaviour {

    private GameObject player;
    private GameObject boss;
    private GameObject[] fighters;
    private GameObject[] servants;
    private TileUtility tileUtility;
    MapConfig mapConfig;
    public  PathFinder pathFinder;
    Vector2Int winRoom;
    static public bool isWaiting;
    float alertSecond;
    public AudioFX musicController;
    private void Awake()
    {

    }
    // Use this for initialization
    void Start(){
        alertSecond = 0f;
        mapConfig = GameObject.FindObjectOfType<AutoGenerateMap>().config;
        pathFinder = new PathFinder(mapConfig);
        pathFinder.init();
        player = GameObject.FindGameObjectWithTag("Player");
        boss = GameObject.FindGameObjectWithTag("boss");
        fighters = GameObject.FindGameObjectsWithTag("fighter");
        servants = GameObject.FindGameObjectsWithTag("servant");
        tileUtility = new TileUtility();
        winRoom = new Vector2Int(-2, 1);
        musicController = GameObject.Find("AudioController").GetComponent<AudioFX>();
    }
    public void Lose()
    {
        musicController.Alert.Stop();
        musicController.lose();
        Destroy(player);
        Debug.Log("lose");
    }
    public void Win()
    {
        musicController.win();
        Destroy(player);
        Debug.Log("win");
    }
    Vector2Int? CalcRoom(Vector2 pos)
    {
        return mapConfig.getRoomRandCWithRoomLoc(pos);
        
    }
	// Update is called once per frame
	void Update () {
        var playerRoom = CalcRoom(player.transform.position);
        bool alert = false;
        var attachTo = player.GetComponent<PlayerController>().attatchTo;
        foreach (var servant in servants)
        {
            if (servant == null||servant== attachTo) continue;
            if (CalcRoom(servant.transform.position)!=null&& CalcRoom(servant.transform.position)== playerRoom)
            {
                alert = true;
                musicController.alert();
                break;
            }
        }
        foreach (var fighter in fighters)
        {
            if (fighter == null || fighter == attachTo) continue;
            if (CalcRoom(fighter.transform.position) != null && CalcRoom(fighter.transform.position) == playerRoom)
            {
                Lose();
            }
        }
        if(boss==null&& playerRoom== winRoom)
        {
            Win();
        }
        if (alertSecond > 0)
        {
            alertSecond -= Time.deltaTime;
            if(alertSecond<0)
            {
                unAlert();
            }
        }
        if (alert == true)
        {
            alertFighter(playerRoom);
        }
    }
     void unAlert()
     {
        foreach (var fighter in fighters)
        {
            if (fighter == null) continue;
            fighter.GetComponent<OtherRoleController>().unAlert();
        }
        musicController.Alert.Stop();
        Debug.Log("unalert");
    }
    public void alertFighter(Vector2Int? playerRoom)
    {
        foreach (var fighter in fighters)
        {
            if (fighter == null) continue;
                fighter.GetComponent<OtherRoleController>().alert(playerRoom.Value);
        }
        Debug.Log("alert");
        alertSecond = 30;
        
    }
}
