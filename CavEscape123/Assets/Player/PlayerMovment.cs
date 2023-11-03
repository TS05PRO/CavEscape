using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Animator anim;
    private SpriteRenderer sprite;

    private float dirX = 0;
    [SerializeField] private float moveSpeed = 12;
    [SerializeField] private float jumpForce = 13;

    private enum MovementState { idle,running, jumping, falling, trancition };

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(dirX * moveSpeed, rigidBody.velocity.y);
        if (Input.GetButtonDown("Jump"))
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
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

        if(rigidBody.velocity.y > 1)
        {
            state = MovementState.jumping;
        }
        else if(rigidBody.velocity.y > 0.1 && rigidBody.velocity.y < 5)
        {
            state = MovementState.trancition;
        }
        else if (rigidBody.velocity.y <- 0.1 && rigidBody.velocity.y >- 4)
        {
            state = MovementState.trancition;
        }
        else if (rigidBody.velocity.y < -0.1)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }
}
