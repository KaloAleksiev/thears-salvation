using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float movementSpeed;
    public Rigidbody2D rb; // used to add velocity and forces to the player

    private float mx; // movement on the x-axis

    private void Update() {
        mx = Input.GetAxis("Horizontal"); // set the movement on the x-axis to what the player inputs (A, D, Left Arrow Key, Right Arrow Key)
    }

    private void FixedUpdate() {
        Vector2 movement = new Vector2(mx * movementSpeed, rb.velocity.y);

        rb.velocity = movement;
    }
}
