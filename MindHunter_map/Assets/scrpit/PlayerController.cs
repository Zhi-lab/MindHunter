using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//这个脚本的作用是测试

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    Skill skill = new Skill(KeyCode.C, 100);
    private Sprite skillEffect;//要创建的Sprite
    float skillSpeed = 5;
    public GameObject attatchTo = null;
    public ScriptableObject skillController;
    public float speed;
    public GameObject skillPrefab;
    public int viewSize = 5;

    // Use this for initialization
    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected void move(bool enable = true)
    {
        if (enable == false)
        {
            rb.velocity = new Vector2(0, 0);
            return;
        }
        float moveHorizontal, moveVertical;
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moveHorizontal = (mousePosInWorld.x - rb.position.x);
            moveVertical = (mousePosInWorld.y - rb.position.y);
            float dis = Mathf.Sqrt(moveHorizontal * moveHorizontal + moveVertical * moveVertical);
            moveHorizontal *= speed / dis;
            moveVertical *= speed / dis;
        }
        else
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
            if (moveHorizontal > 0)
            {
                moveHorizontal = speed;
            }
            if (moveHorizontal < 0)
            {
                moveHorizontal = -speed;
            }
            if (moveVertical > 0)
            {
                moveVertical = speed;
            }
            if (moveVertical < 0)
            {
                moveVertical = -speed;
            }
            if (moveVertical != 0 && moveHorizontal != 0)
            {
                moveVertical = moveVertical / 1.4f;

                moveHorizontal = moveHorizontal / 1.4f;
            }
        }
        rb.velocity = new Vector2(moveHorizontal, moveVertical);

    }

    void CastSkill()
    {
        var r2 = GetComponent<CircleCollider2D>().radius;
        var r1 = skillPrefab.GetComponent<CircleCollider2D>().radius;
        Vector3 mousePosInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var rbpos3d = new Vector3(rb.position.x, rb.position.y, 0);
        var pos = mousePosInWorld - rbpos3d;
        pos.z = 0;
        Vector3 skillPos = rbpos3d + pos.normalized * (r1 + r2);
        Quaternion skillRotation = new Quaternion(0f, 0f, 0f, 0f);
        var rigidbody2D=GameObject.Instantiate(skillPrefab, skillPos, skillRotation).GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = pos.normalized * skillSpeed;
    }
    void InputDetect()
    {
        if (Input.GetMouseButtonDown(1))
        {
            skill.holdFrames = 0;
            Debug.Log("瞄准");
        }
        if (Input.GetMouseButton(1))
        {
            //记录按下的帧数，判断特定按键按下不抬起
            skill.holdFrames++;
            Debug.Log("连按:" + skill.holdFrames + "帧");
        }
        if (Input.GetMouseButtonUp(1) || (skill.holdFrames != 0 && !Input.GetMouseButton(1)))
        {
            //抬起后清空帧数
            skill.holdFrames = 0;
            Debug.Log("释放");
            CastSkill();
            return;
        }
        if (skill.holdFrames == 0)
        {
            move();
        }
        else
        {
            move(false);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        InputDetect();

        //测试
        //Debug.Log(getPlayerPosInTilemap());
    }

}