using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//这个脚本的作用是测试

public class PlayerCountroller : MonoBehaviour {
	
	public float speed;
    private Rigidbody2D rb;

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
    /*
     * 返回值：player GameObject的位置在tilemap中的位置
     * 示例：player GameObject的位置为(-1.5,-2.8,0)，则所对应的tilemap的位置为(-2,-3,0)
    */
    public Vector3Int getPlayerPosInTilemap(){
        int x = Mathf.FloorToInt(transform.position.x);
        int y = Mathf.FloorToInt(transform.position.y);
        int z = Mathf.FloorToInt(transform.position.z);
        Vector3Int posInTilemap = new Vector3Int(x, y, z);
        return posInTilemap;
    }
}