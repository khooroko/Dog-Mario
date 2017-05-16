﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnTriggerEnter : MonoBehaviour {

    public Vector3 spawnPoint;
    public GameObject enemy;
    public bool triggered;
    public bool move1Way;
    public bool moveLeft = true;
    public float speed;

    EnemyController enemyController;

    void Awake() {


    }
   
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" && !triggered) {
            
            GameObject enemyGO = (GameObject) Instantiate(enemy, spawnPoint, Quaternion.identity);
            enemyController = enemyGO.GetComponent<EnemyController>();
            enemyController.move1Way = move1Way;
            enemyController.moveLeft = moveLeft;
            enemyController.speed = speed;
            /*enemy.transform.position = spawnPoint;
            enemy.SetActive(true);*/
            triggered = true;
        }
    }
}