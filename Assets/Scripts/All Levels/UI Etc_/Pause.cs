using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Pause : MonoBehaviour {

    //Application.Quit() only works outside of unity editor. i.e. need to build and run to see it work

    public AudioMixerSnapshot pausedSS;
    public AudioMixerSnapshot unpausedSS;
    public AudioMixer master;

    private Animator anim;
    private bool paused;
    private float originalTimeScale;
    private int index = 0;

    void Start() {
        anim = GetComponent<Animator>();
        //thisTime = Time.time;
        originalTimeScale = Time.timeScale;
        StartCoroutine(PauseRoutine());
    }

    IEnumerator PauseRoutine() {
        while (true) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (!paused) {
                    anim.SetBool("Paused", true);
                    paused = true;
                    float value;
                    master.GetFloat("MusicVol", out value);
                    master.SetFloat("MusicVol", value - 3.0f);
                    master.SetFloat("SFXVol", 0);
                    Time.timeScale = 0f;
                }
                else if (paused) {
                    StartCoroutine(Unpause());
                }
            }
            if (paused) {
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                    index++;
                    index %= 4;
                }
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                    index--;
                    if (index == -1)
                        index = 3;
                }
                anim.SetInteger("Index", index);
                if (index == 0) {
                    if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && StaticVolume.staticMusicVol > 1) {
                        StaticVolume.staticMusicVol--;
                        master.SetFloat("MusicVol", (StaticVolume.staticMusicVol - 5) * 2);
                    }
                    if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && StaticVolume.staticMusicVol < 10) {
                        StaticVolume.staticMusicVol++;
                        master.SetFloat("MusicVol", (StaticVolume.staticMusicVol - 5) * 2);
                    }
                }
                else if (index == 1) {
                    if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && StaticVolume.staticSFXVol > 1) {
                        StaticVolume.staticSFXVol--;
                        if(StaticVolume.staticSFXVol == 0)
                            master.SetFloat("SFXVol", 0);
                        else
                            master.SetFloat("SFXVol", (StaticVolume.staticSFXVol - 5) * 2);
                    }
                    if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && StaticVolume.staticSFXVol < 10) {
                        StaticVolume.staticSFXVol++;
                        master.SetFloat("SFXVol", (StaticVolume.staticSFXVol - 5) * 2);
                    }
                }
                else if(index == 2) {
                    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                        index = 3;
                    }
                    if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) {
                        StageSelect();
                    }
                }
                else if (index == 3) {
                    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                        index = 2;
                    }
                    if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) {
                        Quit();
                    }
                }
            }
            yield return null;
        }
    }

    public void setIndex(int index) {
        this.index = index;
    }

    public IEnumerator Unpause() {
        anim.SetBool("Paused", false);
        paused = false;
        yield return new WaitForSecondsRealtime(0.75f);
        float value;
        master.GetFloat("MusicVol", out value);
        master.SetFloat("MusicVol", value + 3.0f);
        index = 0;
        Time.timeScale = originalTimeScale;
    }

    public void StageSelect() {
        Time.timeScale = originalTimeScale;
        StaticCheckpoint.checkpoint = 1;
        SceneManager.LoadScene("Start Menu");
    }

    public void Quit() {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
                          Application.Quit();
#endif
    }
}
