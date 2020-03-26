using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class playerRespawnManager : NetworkBehaviour
{
    //player to be respawned
    public GameObject player;
    void Start()
    {
        if(isServer)
            SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlayer()
    {
         GameObject p = Instantiate(player);

        NetworkServer.SpawnWithClientAuthority(p, connectionToClient);
    }
}
