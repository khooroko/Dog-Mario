using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkLinkMovement : MonoBehaviour {

    public int movementPattern = 1;
    public float speed = 5;
    public float zRotation = 200;
    private Rigidbody2D rb;
    Vector2 startPos;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start() {
        startPos = new Vector2(transform.position.x, transform.position.y);
    }

    public void CreateWith(int value) { //rotation speed, rotation speed, rotation speed, movement pattern, speed, speed
        this.speed = (float)(value % 100);
        movementPattern = (value / 100) % 10;
        if (value / 1000 != 0) {
            this.zRotation = (float)(value / 1000);
        }
    }

    void FixedUpdate() {
        switch (movementPattern) {
            case 1:
                rb.velocity = speed * Vector2.left;
                break;
            case 2: //spin
                rb.velocity = speed * Vector2.left;
                rb.MoveRotation(rb.rotation + zRotation * Time.deltaTime);
                break;
            case 3:
                rb.velocity = speed * Vector2.down * Mathf.Sign(startPos.y);
                break;
        }
    }
}