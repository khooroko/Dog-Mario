using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingBlocks : MonoBehaviour {

    public bool triggered = false;
    public GameObject[] blocks;
    //public Renderer[] blockRndrs;
    //public BoxCollider2D[] colls;
    public Renderer[] blockRndrs;
    public BoxCollider2D[] colls;
    public int currentPoint;
    public float timeBetweenBlocks = 2f;
    public float timer = 0f;
    public bool move;
    //public Renderer rndr;
    public Collider2D coll;
 

    public AudioSource appearSound;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggered && collision.gameObject.tag == "Player")
        {
            triggered = true;
            StartCoroutine(MagicSequence(blockRndrs, colls));
        }
    }
    
    // Use this for initialization
    void Start () {
        //Renderer[] blockRndrs = new Renderer[blocks.Length];
        //BoxCollider2D[] colls = new BoxCollider2D[blocks.Length];
        for (int i = 0; i < blocks.Length; i++)
        {
            blockRndrs[i] = blocks[i].gameObject.GetComponent<Renderer>();
            colls[i] = blocks[i].gameObject.GetComponent<BoxCollider2D>();
            Disappear(blockRndrs[i], colls[i]);
        }
        appearSound = GetComponent<AudioSource>();
        //Color color = rndr.material.color;

    }

	
	// Update is called once per frame
	void Update () {
        if (triggered)
        {
            
        }
	}

    IEnumerator MagicSequence(Renderer[] blocksRndrs, BoxCollider2D[] colls)
    { 
        int j = 0;
        //float timer = 0f;
        /*
        while (j < blocksRndrs.Length)
        {
            timer += Time.deltaTime;
            if (timer > 2.0f)
            {
                Fade(blocksRndrs[j]); //test
                Appear(blocksRndrs[j+1], colls[j+1]);
                if (timer > 4.0f)
                {
                    Debug.Log("" + timer);
                    Disappear(blocksRndrs[j], colls[j]);
                    j++;
                    timer = 0f;
                }
            }
        }
        yield return null;
        */
        float time = 0.9f;
        while (true) {
            for (j = 0; j + 1 < blocksRndrs.Length; j++) {

                if (j > 4) time = 0.35f;
                if (j == 0) {
                    Appear(blockRndrs[0], colls[0]);
                    yield return new WaitForSeconds(2.0f);
                }
                Appear(blocksRndrs[j + 1], colls[j + 1]);
                yield return new WaitForSeconds(time);
                yield return new WaitForSeconds(0.05f);
                Disappear(blocksRndrs[j], colls[j]);
                yield return new WaitForSeconds(time);
            }
            Disappear(blockRndrs[blockRndrs.Length - 1], colls[blockRndrs.Length - 1]);
            time = 0.9f;
        }

    }
    
    void Appear(Renderer rndr, BoxCollider2D coll)
    {
        Color color = rndr.material.color;
        color.a = 1.0f;
        rndr.material.color = color;
        coll.enabled = true;
        appearSound.PlayOneShot(appearSound.clip, 0.8f);
    }

    void Fade(Renderer rndr)
    {
        Color color = rndr.material.color;
        color.a = 0.5f;
        rndr.material.color = color;
    }

    void Disappear(Renderer rndr, BoxCollider2D coll)
    {
        Color color = rndr.material.color;
        color.a = 0f;
        rndr.material.color = color;  
        coll.enabled = false;
    }

    public void enableTrigger()
    {
        Debug.Log("asd");
        triggered = true;
    }
}
