﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipLevels : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if ((Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))) {
            StaticLives.currLost = 0;
            StaticCheckpoint.checkpoint = 0;
            Time.timeScale = 1;
            SceneManager.LoadScene("Level 1");
        }
        if ((Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))) {
            StaticLives.currLost = 0;
            StaticCheckpoint.checkpoint = 0;
            Time.timeScale = 1;
            SceneManager.LoadScene("Level 2");
        }
        if ((Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))) {
            StaticLives.currLost = 0;
            StaticCheckpoint.checkpoint = 0;
            Time.timeScale = 1;
            SceneManager.LoadScene("Level 3");
        }
        if ((Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))) {
            StaticLives.currLost = 0;
            StaticCheckpoint.checkpoint = 0;
            Time.timeScale = 1;
            SceneManager.LoadScene("Level 4");
        }
    }
}
