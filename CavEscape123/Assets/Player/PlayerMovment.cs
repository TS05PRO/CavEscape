using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource runingSound;
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
            jumpSound.Play();
            rigB.velocity = new Vector2(rigB.velocity.x, jumpForce);
        }
        UpdatteAnimationState();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            runingSound.Pause();
        }
    }

    private void UpdatteAnimationState ()
    {
        bool runSound = false;
        
        MovementState state;
        if(dirX > 0) 
        {
            state = MovementState.running;
            sprite.flipX = false;
            runSound = true;
        }
        else if(dirX < 0)
        {
            state = MovementState.running;
            sprite.flipX = true;
            runSound = true;
        }
        else
        {
            state = MovementState.idle;
            runSound = false;

        }

        if(rigB.velocity.y > 4)
        {
            state = MovementState.jumping;
            runSound = false;

        }
        else if(rigB.velocity.y > 0.01 && rigB.velocity.y < 4)
        {
            state = MovementState.trancition;
            runSound = false;

        }
        else if (rigB.velocity.y <- 0.01 && rigB.velocity.y >- 3.5)
        {
            state = MovementState.trancition;
            runSound = false;

        }
        else if (rigB.velocity.y < -3.5)
        {
            state = MovementState.falling;
            runSound = false;

        }
        if (runSound == true)
        {
            if (!runingSound.isPlaying)
            {
                runingSound.Play();
            }
        }
        else
        {
            runingSound.Pause(); 
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(bColl.bounds.center, bColl.bounds.size, 0, Vector2.down, 0.1f, jumpGround);
    }
}
