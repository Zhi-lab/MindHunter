using UnityEngine;
using System.Collections;

public class SkillController : MonoBehaviour
{
    public Vector3 direction { get; set; }
    public float skillSpeed { get; set; }

    GameObject player;
    public float duration { get; set; }
    private float restTime { get; set; }

    statusBarController statusBar;

    AudioFX musicController;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Destroy(gameObject, duration);
        Debug.Log("start skill");
        statusBar = GameObject.Find("statusBar").GetComponent<statusBarController>();
        musicController = GameObject.Find("AudioController").GetComponent<AudioFX>();
    }

    private void OnTriggerEnter2D(Collider2D  collider)
    {
        Debug.Log("Trigger");
        Debug.Log(collider.name);
        if (collider.tag == "fighter"|| collider.tag == "servant")
        {
            var playerController= player.GetComponent<PlayerController>();
            playerController.enabled = false;
            playerController.attatchTo = collider.gameObject;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            collider.isTrigger = false;
            //UI
            statusBar.setEnemyIcon(collider.tag);
            statusBar.setButtons(collider.tag);
            //audio
            musicController.switchtarget();
        }if(collider.name == "wallTilemap"){
            Destroy(gameObject);
            Destroy(this);
        }
    }

    private void OnColliderEnter2D(Collision collision)
    {
        Debug.Log("Collision");
        if (collision.collider != player)
        {
            Destroy(gameObject);
            Destroy(this);
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
}
