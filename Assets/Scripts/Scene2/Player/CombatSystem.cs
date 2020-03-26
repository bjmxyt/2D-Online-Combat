using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CombatSystem : NetworkBehaviour
{
    //Attack range collider
    public BoxCollider2D swordRange;

    public bool IsDeath = false;
    //maximum health
    [SyncVar(hook =("CmdHealth"))]
    public float Health = 100;

    public Movement playerMov;

    public Animator ani;

    public float PushBackPower = 500;
    public float AttackPower = 50;

    Rigidbody2D PlayerRigid;

    public KeyCode attackKey = KeyCode.K;

    public float InvincibleTime = 0.5f;
    private float currentTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        playerMov = GetComponent<Movement>();
        PlayerRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasAuthority)
            return;
        if (!IsDeath)
        {
            Attack();
            if (!ani.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                swordRange.enabled = false;
            }
        }

    }

    void CmdHealth(float health)
    {
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sword" && collision != this.GetComponentInChildren<Collider2D>())
        {
            if (!IsDeath)
            {
                print("hit");
                ani.SetTrigger("Hurt");
                Health -= AttackPower;
            }

            float forceDir = collision.gameObject.transform.position.x - this.transform.position.x;
            if (forceDir < 0)
            {
                PlayerRigid.AddForce(new Vector2(PushBackPower * 1, 0));
            }
            else if (forceDir > 0)
            {
                PlayerRigid.AddForce(new Vector2(PushBackPower * -1, 0));
            }
        }
    }

    //服务器调用，客户端执行
    [ClientRpc]
    void RpcAttack()
    {
        ani.SetTrigger("Attack");
    }

    //客户端调用，服务器执行
    [Command]
    void CmdAttack()
    {
        ani.SetTrigger("Attack");
        RpcAttack();
    }

    void Attack()
    {
        if (Input.GetButton("Fire1"))
        {
            CmdAttack();
        }

        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            playerMov.LockMovement = true;
        }
        else
        {
            playerMov.LockMovement = false;
        }
    }

    public void EnableSword()
    {
        swordRange.enabled = true;
    }

    public void DisenableSword()
    {
        swordRange.enabled = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (swordRange.GetComponent<Collider2D>().enabled)
            Gizmos.DrawCube(swordRange.bounds.center, swordRange.size);
    }
}
