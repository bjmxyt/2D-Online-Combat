using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraController : NetworkBehaviour
{
    private Transform player;
    public Movement playerMovement;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        player = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (var p in players)
            {
                if (p.GetComponent<NetworkIdentity>().hasAuthority)
                {
                    player = p.transform;
                    transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
                    playerMovement = player.GetComponent<Movement>();
                    offset = transform.position - player.position;
                    break;
                }
            }
        }
        else
        {
            float FixOffset = playerMovement.FaceDirection ? 3 : -3;
            Vector3 targetPosition = new Vector3((offset + player.position).x + FixOffset, transform.position.y, transform.position.z);
            this.transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
        }
    }
}
