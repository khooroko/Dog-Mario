using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfTooFar : MonoBehaviour {

    Vector3 position;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        position = transform.position;
        if (position.x < -20f || position.x > 20f || position.y < -12f || position.y > 12f) {
            Destroy(gameObject);
        }
	}
}
