using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Movement : NetworkBehaviour
{

    private Rigidbody2D rigid;
    public float speed = 10;
    public float jumpForce = 50;
    private Collision collision;

    public float H;
    public float V;

    public bool IsKeyBoardControl = true;

    [SyncVar(hook ="FixFaceDir")]
    public bool FaceDirection = true;

    //移除控制权
    public bool LockMovement = false;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        collision = GetComponent<Collision>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsKeyBoardControl)
        {
            H = Input.GetAxis("Horizontal");
            V = Input.GetAxis("Vertical");
        }
        Vector2 dir = new Vector2(H, V);
        if (hasAuthority)
        {
            if (!LockMovement)
            {
                CmdFixForwardDirection(dir.x);
                walk(dir);
                if (Input.GetButton("Jump"))
                {
                    Jump();
                }
            }
        }
    }


    public void ButtonMove(int dir)
    {
        H = dir;
    }

    public void ButtonStop()
    {
        H = 0;
        V = 0;
    }

    //当同步变量改变时，此函数调用
    public void FixFaceDir(bool newDir)
    {
        FaceDirection = newDir;
        float angle = newDir ? 180 : 0;
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    //客户端调用，服务端执行，客户端请求改变朝向时，首先在服务端响应，然后再更新同步变量一供各客户端响应
    [Command]
    void CmdFixForwardDirection(float h)
    {
        if(h > 0)
        {
            FaceDirection = true;
        }
        else if(h < 0)
        {
            FaceDirection = false;
        }
    }

    private void walk(Vector2 dir)
    {
        //插值移动
        Vector2 targetMovement = new Vector2(dir.x * speed, rigid.velocity.y);
        rigid.velocity = Vector2.Lerp(rigid.velocity, targetMovement, 10 * Time.deltaTime);
        if(collision.OnWall && !collision.onGround)
        {
            if (collision.OnLeftWall)
            {
                rigid.velocity = new Vector2(Mathf.Clamp(dir.x, 0, 1) * speed, rigid.velocity.y);
            }
            else if(collision.OnRightWall)
            {
                rigid.velocity = new Vector2(Mathf.Clamp(dir.x, -1, 0) * speed, rigid.velocity.y);
            }
        }
    }

    public void Jump()
    {
        if(collision.onGround)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
            rigid.velocity += Vector2.up * jumpForce;
        }
    }
}
