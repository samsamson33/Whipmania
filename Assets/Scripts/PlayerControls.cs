﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControls : MonoBehaviour
{
    public float speed, jumpForce;
    private Rigidbody2D myRB;

    public GameObject whip;

    private bool grounded;
    private bool facingRight = true;
    private bool canFlip = true;
    private bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myRB.gravityScale = 2;
        myRB.freezeRotation = true;
        whip.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");

        myRB.velocity = new Vector2(h * speed, myRB.velocity.y);

        //if D, right arrow key, or right analog stick player will face left
        if (myRB.velocity.x > 0 && !facingRight && canFlip)
        {
            Flip();
        }
        //if A, left arrow key, < or left analog stick player will face left
        else if (myRB.velocity.x < 0 && facingRight && canFlip)
        {
            Flip();
        }

        //if(isAttacking)
        //{
        //    myRB.velocity = new Vector2(0, 0);
        //}

        Jump();
        WhipAttack();
    }

    private void Jump()
    {
        if(Input.GetButtonDown("Jump") && grounded)
        {
            myRB.velocity = Vector2.up * jumpForce;
            grounded = false;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }

    private void WhipAttack()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(WhipTimer());
        }
    }

    private IEnumerator WhipTimer()
    {
        whip.SetActive(true);
        canFlip = false;
        isAttacking = true;
        yield return new WaitForSeconds(0.2f);
        whip.SetActive(false);
        canFlip = true;
        isAttacking = false;
    }

    private void Flip()
    {
        facingRight = !facingRight;

        transform.Rotate(0, 180f, 0);
    }
}
