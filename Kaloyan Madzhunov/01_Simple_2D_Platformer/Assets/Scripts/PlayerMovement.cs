using System;
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

    private float mx; //movement on the x-axis
    private int jumpCounter = 0;
    private float jumpCoolDown;
    private bool isGrounded = false;

    private void Update() {
        mx = Input.GetAxis("Horizontal"); //set the movement on the x-axis to what the player inputs (A, D, Left Arrow Key, Right Arrow Key)

        rotateCharacter();

        if (Input.GetButtonDown("Jump")) {
            Jump();
        }

        CheckGrounded();
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(mx * movementSpeed, rb.velocity.y);
    }

    private void Jump() {
        if (isGrounded || jumpCounter < extraJumps) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); ;
            jumpCounter++;
        }
    }

    public void CheckGrounded() {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.5f, groundLayer); //returns true if the circle collides with the layer mask

        if (groundCheck) { //on the ground
            isGrounded = true;
            jumpCounter = 0; //reset the jump count
            jumpCoolDown = Time.time + 0.2f;
        } else if (Time.time < jumpCoolDown) { //if the player is not touching the ground, give them the benefit of 0.2 seconds to hit the jump key
            isGrounded = true;
        } else { //the player is not touching the ground
            isGrounded = false;
        }
    }

    private void rotateCharacter() {
        //make character face left or right depending on key pressed
        if (mx > 0f) {
            transform.localScale = new Vector3(1f, 1f, 1f);
        } else if (mx < 0f) {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
