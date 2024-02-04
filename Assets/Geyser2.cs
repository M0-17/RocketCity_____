using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser2 : MonoBehaviour
{
    public float force = 5f;
    private GameObject player;
    private Rigidbody2D rb;
    public float triggerTime;
    private float trackingTime;
    private int state;
    private Animator am;
    private Vector3 startPos;

    void Start()
    {
        state = 0;
        am = gameObject.GetComponent<Animator>();
        startPos = transform.position;
        player = GameObject.FindWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        trackingTime += Time.deltaTime;
        //Debug.Log("state: " + state + " triggerTime: " + trackingTime);

        if (state == 0)
        {
            if (trackingTime >= triggerTime)
            {
                state = 1;
                am.SetTrigger("explode");
                trackingTime = 0;
                gameObject.transform.position = startPos;
            }
        } else if (state == 1)
        {
            if (trackingTime >= 1.3f)
            {
                am.ResetTrigger("explode");
                state = 0;
                trackingTime = 0;
                gameObject.transform.position = new Vector3(startPos.x, startPos.y - 10, startPos.z - 2);
            }
        }

        //if (trackingTime >= triggerTime)
        //{
        //    gameObject.transform.position = startPos;
        //} else
        //{
        //    gameObject.transform.position = new Vector3(startPos.x, startPos.y - 10, startPos.z - 2);
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("ENTERED");
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, force * rb.mass), ForceMode2D.Impulse);
        }
    }
}
