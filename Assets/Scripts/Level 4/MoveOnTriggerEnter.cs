﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnTriggerEnter : MonoBehaviour {

    private bool triggered = false;
    public MusicManager musicManager;


    void OnTriggerEnter2D (Collider2D other)
    {
        MoveWithWaypoints moveAirman = transform.parent.GetComponent("MoveWithWaypoints") as MoveWithWaypoints;
        if (other.tag == "Player" && !triggered)
        {
            musicManager.playAirman();
            triggered = true;
            moveAirman.enabled = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
