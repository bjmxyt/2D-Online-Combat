using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ButtonAttack : NetworkBehaviour
{
    public Animator PlayerAni = null;
    private Button button;
    // Start is called before the first frame update
    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate () {
            buttonAttack();
        });

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerAni)
            return;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var p in players)
        {
            if (p.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                PlayerAni = p.GetComponent<Animator>();
            }
        }
    }

    public void buttonAttack()
    {
        if(PlayerAni)
        {
            PlayerAni.SetTrigger("Attack");
        }
    }
}
