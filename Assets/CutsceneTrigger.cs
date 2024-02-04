using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneTrigger : MonoBehaviour
{
    public Animator anim1;
    public string anim1Trigger;
    public Animator anim2;
    public string anim2Trigger;
    private bool started;
    public float endTime;
    private float trackTime;
    public string nextScene;
    public Vector2 fixCam;
    // Start is called before the first frame update
    void Start()
    {
        started = false;
        trackTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            trackTime += Time.deltaTime;
        }
        if (trackTime >= endTime)
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Globals.winDHunt = false;
            Destroy(GameObject.FindWithTag("Player"));
            GameObject cam = GameObject.FindWithTag("MainCamera");
            cam.GetComponent<CamFollow>().shouldFollow = false;
            cam.transform.position = new Vector3(fixCam.x, fixCam.y, cam.transform.position.z);
            anim2.gameObject.GetComponent<SpriteRenderer>().enabled = true;

            anim1.SetTrigger(anim1Trigger);
            anim2.SetTrigger(anim2Trigger);
            started = true;
        }
    }
}
