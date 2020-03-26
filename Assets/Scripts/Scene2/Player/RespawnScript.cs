using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    public Transform RespawnPos;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Respawn")
        {
            this.transform.position = RespawnPos.position;
        }
    }
}
