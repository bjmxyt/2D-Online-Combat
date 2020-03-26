using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AnimatorController : NetworkBehaviour
{
    public Collision collision;
    public Animator ani;
    public Movement playerMov;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        collision = GetComponent<Collision>();
        playerMov = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAuthority)
        {
            float h = playerMov.H;
            ani.SetFloat("Horizontal", Mathf.Abs(h));
            ani.SetBool("IsGround", collision.onGround);
        }
    }


}
