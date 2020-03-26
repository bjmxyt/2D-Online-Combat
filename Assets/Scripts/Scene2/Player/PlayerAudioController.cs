using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    public AudioSource running;
    public AudioSource jumping;
    public AudioSource landing;
    public AudioSource attack;
    public AudioSource hurt;

    Animator PlayerAni;
    Collision collision;

    //记录上一帧的着地情况，用于判断起跳还是落地
    private bool PreGroundState = false;

    // Start is called before the first frame update
    void Start()
    {
        PlayerAni = GetComponent<Animator>();
        collision = GetComponent<Collision>();
        PreGroundState = collision.onGround;
    }

    // Update is called once per frame
    void Update()
    {
        RunPlaying();
        JumpPlaying();
        LandPlaying();
        PreGroundState = collision.onGround;
    }

    //着陆音效
    private void LandPlaying()
    {
        if(collision.onGround == true && PreGroundState == false)
        {
            landing.Play();
        }
    }

    //起跳音效
    private void JumpPlaying()
    {
        if(collision.onGround == false && PreGroundState == true)
        {
            jumping.Play();
        }
    }

    //跑步音效
    private void RunPlaying()
    {
        if (PlayerAni.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            if (!running.isPlaying)
                running.Play();
        }
        else
        {
            if (running.isPlaying)
                running.Stop();
        }
    }

    //动画事件
    public void PlayAttackAudio()
    {
        if (!attack.isPlaying)
            attack.Play();
    }

    public void PlayHurtAudio()
    {
        if (!hurt.isPlaying)
            hurt.Play();
    }
}
