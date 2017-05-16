﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioQblockSpawnObject : MonoBehaviour {

    public GameObject objectToSpawn;
    public bool invisible;
    Animator anim;
    bool broken;
    bool entered;
    SpriteRenderer[] sprites;
    Vector3 startPosition;
    bool moved;

    void Awake() {
 
    }

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        if (invisible) {
            GetComponent<BoxCollider2D>().enabled = false;
            anim.SetBool("Invis", true);
        }
        startPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D c) {
        if (c.tag == "DogFeet" || c.tag == "Player") {
            entered = true;
        }
        if (c.tag == "DogHead" && !broken && c.transform.position.y < transform.position.y - 0.2f && !entered) {
            Debug.Log("head hit below");
            GetComponent<BoxCollider2D>().enabled = true;
            anim.SetTrigger("Broke");

            anim.SetBool("Invis", false);
            if (objectToSpawn != null) {
                Instantiate(objectToSpawn, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            }
            broken = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "DogHead") {
            entered = false;
        }
    }

    /*void LateUpdate() {
        if (broken && !moved) {
            //transform.localPosition += new Vector3(startPosition.x, -startPosition.y,0f);
            transform.localPosition += startPosition;
            moved = true;
        }
    }*/
}