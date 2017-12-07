using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuMain : MonoBehaviour {

    private int state;
    private Animator anim;
    private float inputLag = 0.2f;
    private float lastCommandTime;
    private int sceneToLoad1;
    private int sceneToLoad2;
    public GameObject[] clearedPics;

	// Use this for initialization
	void Start () {
        state = 0;
        anim = GetComponent<Animator>();
        if (StaticCheckpoint.checkpoint == 1) {
            anim.SetBool("SS", true);
            state = 2;
            StaticCheckpoint.checkpoint = 0;
        }
        for(int i = 1; i < clearedPics.Length; i++) {
            if (!PlayerPrefs.HasKey("Cleared " + i) || PlayerPrefs.GetInt("Cleared " + i) != 1) {
                clearedPics[i].SetActive(false);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
            Debug.Log(state);

        if (state == 0) {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) {
                anim.SetTrigger("Space1");
                state = 1;
                lastCommandTime = Time.time;
            }
        }
        if (state == 1) {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) && Time.time > lastCommandTime + inputLag) {
                anim.SetTrigger("Space2");
                state = 2;
                lastCommandTime = Time.time;
            }
        }
        if (state == 2) {
            if (Input.GetAxis("Vertical") > 0.1f && Time.time > lastCommandTime + inputLag) {
                anim.SetTrigger("Up");
                lastCommandTime = Time.time;
                sceneToLoad1 += 1;
                sceneToLoad1 %= 2;
            }

            if (Input.GetAxis("Vertical") < -0.1f && Time.time > lastCommandTime + inputLag) {
                anim.SetTrigger("Down");
                lastCommandTime = Time.time;
                sceneToLoad1 += 1;
                sceneToLoad1 %= 2;
            }
            if (Input.GetAxis("Horizontal") > 0.1f && Time.time > lastCommandTime + inputLag) {
                anim.SetTrigger("Right");
                lastCommandTime = Time.time;
                sceneToLoad2 += 1;
                sceneToLoad2 %= 2;
            }
            if (Input.GetAxis("Horizontal") < -0.1f && Time.time > lastCommandTime + inputLag) {
                anim.SetTrigger("Left");
                lastCommandTime = Time.time;
                sceneToLoad2 += 1;
                sceneToLoad2 %= 2;
            }
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) && Time.time > lastCommandTime + inputLag) {
                if (sceneToLoad1 == 0) {
                    if (sceneToLoad2 == 0)
                        SceneManager.LoadScene("Level 1");
                    else
                        SceneManager.LoadScene("Level 2");
                }
                else {

                    if (sceneToLoad2 == 0)
                        SceneManager.LoadScene("Level 3");
                    else
                        SceneManager.LoadScene("Level 4");
                }
            }
        }
        if (Input.GetKey(KeyCode.Backslash)) {
            for (int i = 1; i < clearedPics.Length; i++) {
                clearedPics[i].SetActive(false);
            }
        }
    }
}
