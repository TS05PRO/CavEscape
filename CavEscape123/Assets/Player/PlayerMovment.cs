using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    private Rigidbody2D rigB;
    private BoxCollider2D bColl;
    private Animator anim;
    private SpriteRenderer sprite;

    private float dirX = 0;
    [SerializeField] private float moveSpeed = 12;
    [SerializeField] private float jumpForce = 13;
    [SerializeField] private LayerMask jumpGround;

    private enum MovementState { idle,running, jumping, falling, trancition, spawn};

    private void Start()
    {
        rigB = GetComponent<Rigidbody2D>();
        bColl = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        
    }


    private void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        rigB.velocity = new Vector2(dirX * moveSpeed, rigB.velocity.y);
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rigB.velocity = new Vector2(rigB.velocity.x, jumpForce);
        }
        UpdatteAnimationState();
    }

    private void UpdatteAnimationState ()
    {
        MovementState state;
        if(dirX > 0) 
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if(dirX < 0)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if(rigB.velocity.y > 4)
        {
            state = MovementState.jumping;
        }
        else if(rigB.velocity.y > 0.01 && rigB.velocity.y < 4)
        {
            state = MovementState.trancition;
        }
        else if (rigB.velocity.y <- 0.01 && rigB.velocity.y >- 3.5)
        {
            state = MovementState.trancition;
        }
        else if (rigB.velocity.y < -3.5)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(bColl.bounds.center, bColl.bounds.size, 0, Vector2.down, 0.1f, jumpGround);
    }
}
