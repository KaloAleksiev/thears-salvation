using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float movementSpeed;
    public Rigidbody2D rb; // used to add velocity and forces to the player

    public float jumpForce = 20f;
    public Transform feet;
    public LayerMask groundLayers;

    private float mx; // movement on the x-axis

    private void Update() {
        mx = Input.GetAxis("Horizontal"); // set the movement on the x-axis to what the player inputs (A, D, Left Arrow Key, Right Arrow Key)

        if (Input.GetButtonDown("Jump") && IsGrounded()) {
            Jump();
        }
    }

    private void FixedUpdate() {
        Vector2 movement = new Vector2(mx * movementSpeed, rb.velocity.y);

        rb.velocity = movement;
    }

    private void Jump() {
        Vector2 movement = new Vector2(rb.velocity.x, jumpForce);

        rb.velocity = movement;
    }

    public bool IsGrounded() {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.5f, groundLayers);

        return groundCheck != null;
    }
}
