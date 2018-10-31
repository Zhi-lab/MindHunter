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

    private void OnTriggerEnter2D(Collider2D  collider)
    {
        if (collider.tag == "fighter"|| collider.tag == "servant")
        {
            var playerController= player.GetComponent<PlayerController>();
            playerController.enabled = false;
            playerController.attatchTo = collider.gameObject;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            collider.isTrigger = false;
        }
    }

    private void OnColliderEnter2D(Collision collision)
    {
        
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
