using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuMain : MonoBehaviour {

    private int state;
    private Animator anim;
    private float inputLag = 0.2f;
    private float lastCommandTime;
    private int sceneToLoad;
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
        if (state == 0) {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)) {
                anim.SetTrigger("Space1");
                state = 1;
                lastCommandTime = Time.time;
            }
        }
        if (state == 1) {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)) && Time.time > lastCommandTime + inputLag) {
                anim.SetTrigger("Space2");
                state = 2;
                lastCommandTime = Time.time;
            }
        }
        if (state == 2) {
            if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f && Time.time > lastCommandTime + inputLag) {
                lastCommandTime = Time.time;
                sceneToLoad = (sceneToLoad == 0) ? 2 : sceneToLoad == 2 ? 0 : sceneToLoad == 1 ? 3 : 1;
                anim.SetInteger("Level", sceneToLoad + 1);
            }
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f && Time.time > lastCommandTime + inputLag) {
                lastCommandTime = Time.time;
                sceneToLoad = (sceneToLoad == 0) ? 1 : sceneToLoad == 1 ? 0 : sceneToLoad == 2 ? 3 : 2;
                anim.SetInteger("Level", sceneToLoad + 1);
            }
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))) {
                LoadSelectedScene();
            }
        }
        if (Input.GetKey(KeyCode.Backslash)) {
            for (int i = 1; i < clearedPics.Length; i++) {
                clearedPics[i].SetActive(false);
            }
        }
    }

    public void SetScene(int level) {
        sceneToLoad = level - 1;
        anim.SetInteger("Level", level);
    }

    public void LoadSelectedScene() {
        if (state == 2 && Time.time > lastCommandTime + inputLag) {
            SceneManager.LoadScene("Level " + (sceneToLoad + 1));
        }
    }
}
