using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ButtonJump : MonoBehaviour
{
    public Movement player = null;
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate () {
            buttonJump();
        });
    }

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
                player = p.GetComponent<Movement>();
            }
        }
    }
    public void buttonJump()
    {
        if(player)
        {
            player.Jump();
        }
    }
}
