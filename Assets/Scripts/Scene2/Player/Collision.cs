using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [Header("layers")]
    public LayerMask groundMask;

    [Space]
    public bool onGround;
    public bool OnWall;
    public bool OnLeftWall;
    public bool OnRightWall;

    [Header("Offset")]
    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset, rightOffset, leftOffset;
    private Color GizmosColor = Color.red;


    void Update()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundMask);
        OnRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundMask);
        OnLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundMask);
        OnWall = OnRightWall || OnLeftWall;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
    }

}
