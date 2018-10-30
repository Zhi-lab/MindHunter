using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//这个脚本的作用是测试

public class PlayerController : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb;
    public int viewSize = 5;
    Skill skill=new Skill(KeyCode.C,100);
    public Sprite skillSprite;//2D Sprite的预设
    private Sprite skillEffect;//要创建的Sprite
    float skillSpeed = 12;
    public GameObject attatchTo=null;
    public ScriptableObject skillController;


    // Use this for initialization
    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

     protected void move(bool enable=true)
    {
        if(enable==false)
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
            float dis =Mathf.Sqrt( moveHorizontal* moveHorizontal+ moveVertical* moveVertical);
            moveHorizontal *= speed /dis;
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
        skillSprite=Resources.Load<Sprite>("skillSprite");
        GameObject gameObject = new GameObject();
        var spriteRenderer=gameObject.AddComponent<SpriteRenderer>();
        var r1 = gameObject.AddComponent<CircleCollider2D>().radius;
        var r2 = GetComponent<CircleCollider2D>().radius;
        spriteRenderer.sprite = skillSprite;
        skillEffect = Sprite.Instantiate(skillSprite, rb.position, Quaternion.identity);//在这里创建Sprite
        gameObject.transform.localScale = new Vector3(0.2f,0.2f,0.2f);

        Vector3 mousePosInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var rbpos3d = new Vector3(rb.position.x, rb.position.y,0);
        var pos= mousePosInWorld-rbpos3d;
        pos.z = 0;
        gameObject.transform.position = rbpos3d+pos.normalized * (r1+r2);

        var skillRigid = gameObject.AddComponent<Rigidbody2D>();
        skillRigid.mass = 0;
        skillRigid.velocity = pos.normalized * skillSpeed;
        skillRigid.gravityScale = 0;
        var skillController=gameObject.AddComponent<SkillController>();
        skillController.duration = 0.3f;
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
        if (Input.GetMouseButtonUp(1)|| (skill.holdFrames!=0 && !Input.GetMouseButton(1)))
        {
            //抬起后清空帧数
            skill.holdFrames=0;
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