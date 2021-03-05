﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float movementSpeed = 10f;
    public float jumpForce = 20f;
    public int extraJumps = 1;

    public Rigidbody2D rb; //used to add velocity and forces to the player
    public Transform feet;
    public LayerMask groundLayer;
    public float groundRadius = 0.1f;

    private float mx; //movement on the x-axis
    private int jumpCounter = 0;
    private float jumpCoolDown;
    private bool isGrounded = false;
    private bool isFacingRight = true;

    public float wallJumpTime = 0.2f; //when the player lets go off the wall, they are still able to jump for period of 0.2 seconds
    public float wallSlideSpeed = 0.3f; //how fast the character slides down the wall
    public float wallDistance = 0.4f; //how far away the wall has to be from the collider of the player when they collide with it

    public bool isWallSliding = false;
    private RaycastHit2D WallCheckHit; //used to check for touching the wall

    private void Update() {
        mx = Input.GetAxis("Horizontal"); //set the movement on the x-axis to what the player inputs (A, D, Left Arrow Key, Right Arrow Key)

        rotateCharacter();

        if (Input.GetButtonDown("Jump") || isWallSliding && Input.GetButtonDown("Jump")) {
            Jump();
        }

        CheckGrounded();
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(mx * movementSpeed, rb.velocity.y);

        //check if the character is colliding a wall
        if (isFacingRight) {
            //returns true if the raycast is touching a ground layer on the right at wall distance from the character position
            WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(wallDistance, 0), wallDistance, groundLayer);
            Debug.DrawRay(transform.position, new Vector2(wallDistance, 0), Color.white);
        } else {
            //returns true if the raycast is touching a ground layer on the left at wall distance from the character position
            WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(-wallDistance, 0), wallDistance, groundLayer);
            Debug.DrawRay(transform.position, new Vector2(-wallDistance, 0), Color.white);
        }

        //check if the character is sliding down the wall
        if (WallCheckHit && !isGrounded && mx != 0) { //check if a wall is hit while the character is not grounded and the player is holding down a horizontal movement key
            isWallSliding = true;
            jumpCoolDown = Time.time + wallJumpTime; //allows the player to first press the movement key and then jump off the wall, instead of vice versa
        } else if (jumpCoolDown < Time.time) { //if the time has passed
            isWallSliding = false;
        }

        //slow the character fall
        if (isWallSliding) {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, wallSlideSpeed, float.MaxValue));
        }
    }

    private void Jump() {
        if (isGrounded || jumpCounter < extraJumps) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCounter++;
        }
    }

    public void CheckGrounded() {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, groundRadius, groundLayer); //returns true if the circle collides with the layer mask
        Debug.DrawRay(feet.position, new Vector2(groundRadius, 0), Color.white);

        if (groundCheck) { //on the ground
            isGrounded = true;
            jumpCounter = 0; //reset the jump count
            jumpCoolDown = Time.time + 0.2f; //give the player the benefit of 0.2 seconds to hit the jump key before actually touching the ground
        } else if (Time.time < jumpCoolDown) { //the player is not touching the ground
            isGrounded = true;
        } else { //the player is not touching the ground
            isGrounded = false;
        }
    }

    private void rotateCharacter() {
        //make character face left or right depending on key pressed
        if (mx > 0f) {
            isFacingRight = true;
            transform.localScale = new Vector3(1f, 1f, 1f);
        } else if (mx < 0f) {
            isFacingRight = false;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}