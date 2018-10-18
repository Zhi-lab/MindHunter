using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//这个脚本的作用是测试

public class PlayerController : MonoBehaviour {
	
	public float speed;
    private Rigidbody2D rb;
    public int viewSize = 5;

    // Use this for initialization
    void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
        if (moveHorizontal > 0) {
            moveHorizontal = speed;
            moveVertical = 0;
        }
        if (moveHorizontal < 0)
        {
            moveHorizontal = -speed;
            moveVertical = 0;
        }
        if (moveVertical > 0) {
            moveVertical = speed;
            moveHorizontal = 0;
        }
        if (moveVertical < 0)
        {
            moveVertical = -speed;
            moveHorizontal = 0;
        }
        rb.velocity = new Vector2(moveHorizontal, moveVertical);
        //测试
        //Debug.Log(getPlayerPosInTilemap());
	}

}