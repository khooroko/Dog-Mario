using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public int movementPattern = 1;
    public float speed;
    public int convergePoint = 1;
    public int convergeDist = 4;
    public GameObject explosion;
    private Rigidbody2D rb;
    Vector2 startPos;
    public bool isDead;

    private Vector2 randomDrift;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start() {
        startPos = new Vector2(transform.position.x, transform.position.y);
    }

    public void CreateWith(int value) { //Converge point, converge dist, movement pattern, speed, speed
        this.speed = (float)(value % 100);
        movementPattern = (value / 100) % 10;
        if (value / 1000 != 0) {
            convergeDist = (value / 1000) % 10;
            convergePoint = value / 10000;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "DogFeet" && !isDead) {
            Die();
        }
    }

    public void Die() {
        isDead = true;
        randomDrift = new Vector2(Random.Range(0, -speed), Random.Range(-1f, 1f));
        tag = "Untagged";
        StartCoroutine(PlayDeath());
    }
    
    IEnumerator PlayDeath() {
        Instantiate(explosion, transform);
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }

    void FixedUpdate() {
        if (!isDead) {
            switch (movementPattern) {
                case 1: //_,-
                    if (rb.transform.position.x > convergePoint + convergeDist) {
                        rb.velocity = speed * Vector2.left;
                    }
                    else if (rb.transform.position.x > convergePoint - convergeDist) {
                        rb.velocity = speed * new Vector2(-0.75f, (-startPos.y / (2 * convergeDist)));
                    }
                    else {
                        rb.velocity = speed * Vector2.left;
                    }
                    break;
                case 2: //move up/down only
                    rb.velocity = speed * Vector2.down * Mathf.Sign(startPos.y);
                    break;

            }
        }
        else {
            rb.velocity = randomDrift;
        }
    }
}
