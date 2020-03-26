using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffect : MonoBehaviour
{
    public Rigidbody2D player;
    public Collision collision;
    public AnimationCurve curve;
    float zOffset;
    // Start is called before the first frame update
    void Start()
    {
        zOffset = transform.position.z;
        collision = player.gameObject.GetComponent<Collision>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Jump"))
        {
            StartCoroutine(test());
        }
    }

    private IEnumerator test()
    {
        while(!collision.onGround)
        {
            print("!ground!");
        }
        yield return null;
    }
}
