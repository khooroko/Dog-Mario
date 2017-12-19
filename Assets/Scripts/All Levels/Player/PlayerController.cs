using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : PhysicsObject {

    public float maxSpeed = 8;
    public float jumpTakeOffSpeed = 19;

	// FOR: Dialogue boxes when chars are talking
	// SCRIPT: TextBoxManager
	// control player movement during dialogue
	public bool canMove;
    
    private MusicManager musicManager;
    private bool dead;
    private Animator animator;
    private bool reachedFlag;
    private AudioSource bork;
    private Scene currentScene;
    private string currentSceneName;

    // Use this for initialization
    void Awake() {
        if (GameObject.FindGameObjectWithTag("MusicManager") != null) {
            musicManager = GameObject.FindGameObjectWithTag("MusicManager").GetComponent<MusicManager>();
        }
        animator = GetComponent<Animator>();
        bork = GetComponent<AudioSource>();
        
    currentScene = SceneManager.GetActiveScene();
        currentSceneName = currentScene.name;
    }
		

    protected override void ComputeVelocity() {
		if (!canMove) {
			return;
		}

        if (!dead) {
            if (!reachedFlag) {
                Vector2 move = Vector2.zero * Time.smoothDeltaTime;
                
                move.x = Input.GetAxisRaw("Horizontal");

                if (Input.GetButton("Jump") && grounded) {
                    velocity.y = jumpTakeOffSpeed;
                    bork.Play();
                }
                else if (Input.GetButtonUp("Jump") || (Input.GetButtonDown("Jump") && !grounded)) {
                    if (velocity.y > 0) {
                        velocity.y = velocity.y * 0.8f;
                    }
                }

                bool flipSprite = (Mathf.Abs(Mathf.Round(transform.rotation.y)) == 1 ? (move.x > 0.1f) : (move.x < -0.1f));
                if (flipSprite) {
                    transform.rotation = Quaternion.Euler(0f, (Mathf.Abs(Mathf.Round(transform.rotation.y)) == 1 ? 0f : 180f), 0);
                }

                //if dw play move animation, add (grounded) into condition
                if (Mathf.Abs(velocity.x) > 0.01f) {
                    animator.SetBool("isMoving", true);
                }
                else {
                    animator.SetBool("isMoving", false);
                }

                targetVelocity = move * maxSpeed;

                if (!currentSceneName.Equals("Level 4") && transform.position.y < -5.0f) {  //drop to death
                    Die();
                }
            }
            else {
                if (!grounded) {
                    velocity.x = 0;
                    velocity.y = -2;
                }
                else {
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    if (transform.rotation.y == 1) {
                        transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                    }
                    velocity.y = 0;
                    targetVelocity = new Vector2(2f,0f);
                }
                if (!currentSceneName.Equals("Level 4") && (transform.position.y < -5.0f)) {  //drop to death
                    Die();
                }
            }
        }
    }

    public void Die() {
        if (!dead) {
            dead = true;
            rb2d.velocity.Set(0f, 0f);
            StartCoroutine(PlayDeath());
        }
        
    }

    IEnumerator PlayDeath() {
        animator.SetTrigger("dead");
        if (musicManager != null) {
            musicManager.playDead();
        }
        yield return new WaitForSeconds(1.5f /*if using animation, change to deathAnimation.clip.Length*/);
        StaticLives.lives--;
        StaticLives.currLost++;
        SceneManager.LoadScene(currentSceneName);   //load current level
    }

    public bool isGrounded() {
        return grounded;
    }

    public void ReachFlag() {
        reachedFlag = true;
    }

    public Vector3 getSpeed() {
        return velocity;
    }

    public string getCurrentSceneName()
    {
        return currentSceneName;
    }
}