using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControll : MonoBehaviour
{
    public float moveSpeed = 3f;

    private float velX;

    private float velY;

    private bool facingRight = true;

    private Rigidbody2D rigBody;
    
    private Animator anim;
    
    private bool isWalking = false;

    private bool isJumping = false;
    
    // Start is called before the first frame update
    void Start()
    {
         rigBody = GetComponent<Rigidbody2D>();
         anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        velX = Input.GetAxisRaw("Horizontal");

        // Check if we need to jump
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rigBody.AddForce(Vector2.up * 700f);
            isJumping = true;
        }
        else if (Math.Abs(rigBody.velocity.y) < 0.001)
        {
            isJumping = false;
        }
        
        velY = rigBody.velocity.y;
        rigBody.velocity = new Vector2(velX * moveSpeed, velY);

        if (velX != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
        
        // Set animations booleans
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isJumping", isJumping);
    }

    private void LateUpdate()
    {
        Vector3 localScale = transform.localScale;

        if (velX > 0)
        {
            facingRight = true;
        }
        else if (velX < 0)
        {
            facingRight = false;
        }

        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
        {
            localScale.x *= -1;
        }

        transform.localScale = localScale;
    }
}
