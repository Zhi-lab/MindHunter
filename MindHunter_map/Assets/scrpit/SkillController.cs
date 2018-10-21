using UnityEngine;
using System.Collections;

public class SkillController : MonoBehaviour
{
    public Vector3 direction { get; set; }
    public float skillSpeed { get; set; } 
    public float duration { get; set; }
    private float restTime { get; set; }
    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, duration);
        Debug.Log("start skill");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag!="Player")
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
