using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ButtonLeft : NetworkBehaviour
{
    public Movement player = null;

    // Update is called once per frame
    void Update()
    {
        if (player)
            return;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var p in players)
        {
            if (p.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                print("111");
                player = p.GetComponent<Movement>();
            }
        }
    }

    public void MoveLeft()
    {
        if (player)
        {
            player.ButtonMove(-1);
        }
    }

    public void MoveStop()
    {
        if(player)
        {
            player.ButtonStop();
        }
    }
}
