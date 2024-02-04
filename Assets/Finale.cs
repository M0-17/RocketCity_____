using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finale : MonoBehaviour
{
    private GameObject player;
    public Vector2 position;
    private bool animStarted;
    public float endTime;
    private float trackTime;
    public Animator am;
    private GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera");
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (animStarted)
        {
            trackTime += Time.deltaTime;
            if (trackTime >= endTime)
            {
                cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + (trackTime/1.2f), cam.transform.position.z);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        cam.GetComponent<CamFollow>().shouldFollow = false;
        cam.transform.position = position;

        //turn on animation
        //am.SetTrigger("");
        animStarted = true;



    }


}
