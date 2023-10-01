using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    private Rigidbody2D prb;

    private void Start()
    {
        prb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        prb.velocity = new Vector2(dirX * 9, prb.velocity.y);
        if (Input.GetButtonDown("Jump"))
        {
            prb.velocity = new Vector2(prb.velocity.x, 13);
        }
    }
}
