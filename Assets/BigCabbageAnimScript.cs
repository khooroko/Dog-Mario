using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCabbageAnimScript : MonoBehaviour {

    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        anim.SetBool("isMoving", true);
        anim.speed = 2f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
