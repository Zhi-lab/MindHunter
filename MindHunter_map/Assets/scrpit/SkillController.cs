using UnityEngine;
using System.Collections;

public class SkillController : MonoBehaviour
{
    public Vector3 direction { get; set; }
    public float skillSpeed { get; set; }

    GameObject player;
    public float duration { get; set; }
    private float restTime { get; set; }

    private statusBarController statusBarController;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Destroy(gameObject, duration);
        Debug.Log("start skill");

        //UI
        statusBarController = GameObject.Find("statusBar").GetComponent<statusBarController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "fighter"|| collision.collider.tag == "servant")
        {
            var playerController= player.GetComponent<PlayerController>();
            playerController.enabled = false;
            playerController.attatchTo = collision.collider.gameObject;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            statusBarController.setEnemyIcon(collision.collider.tag);
            statusBarController.setButtons(collision.collider.tag);
        }
        else if(collision.collider!=player)
        {
            Destroy(gameObject);
            Destroy(this);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(statusBarController == null){
            statusBarController = GameObject.Find("statusBar").GetComponent<statusBarController>();
        }
    }
}
