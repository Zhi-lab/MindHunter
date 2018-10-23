using UnityEngine;
using System.Collections;

public class SkillController : MonoBehaviour
{
    public Vector3 direction { get; set; }
    public float skillSpeed { get; set; }

    GameObject player;
    public float duration { get; set; }
    private float restTime { get; set; }
    // Use this for initialization
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        Destroy(gameObject, duration);
        Debug.Log("start skill");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "fighter"|| collision.collider.tag == "servant")
        {
            var playerController= player.GetComponent<PlayerController>();
            playerController.enabled = false;
            playerController.attatchTo = collision.collider.gameObject;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            collision.collider.gameObject.GetComponent<PlayerController>().enabled=true;
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
    }
}
