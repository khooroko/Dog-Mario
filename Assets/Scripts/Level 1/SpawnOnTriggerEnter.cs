﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnTriggerEnter : MonoBehaviour {

    public Vector3 relativeSpawnPoint;
    public GameObject enemy;
    public bool triggered;
    public bool move1Way;
    public bool moveLeft;
	public bool moveUp;
	//public bool movesThenStops;
    public float speed;
	//public float distance;

    EnemyController enemyController;
    AudioSource spawnSound;

    void Awake() {
        spawnSound = GetComponent<AudioSource>();

    }
   
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" && !triggered) {
            
            GameObject enemyGO = (GameObject) Instantiate(enemy, transform.position + relativeSpawnPoint, Quaternion.identity);
            if (spawnSound != null) {
                spawnSound.Play();
            }
            enemyController = enemyGO.GetComponent<EnemyController>();
            if (enemyController != null) {
                enemyController.move1Way = move1Way;
                enemyController.moveLeft = moveLeft;
				enemyController.moveUp = moveUp;
                enemyController.speed = speed;
				//enemyController.distance = distance;
            }
            /*enemy.transform.position = spawnPoint;
            enemy.SetActive(true);*/
            triggered = true;
        }
    }
}
