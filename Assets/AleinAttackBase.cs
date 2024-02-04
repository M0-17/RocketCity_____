using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// using UnityEngine.InputSystem;

public class AleinAttackBase : MonoBehaviour
{
    [SerializeField] private float gameTime = 60f;
    [SerializeField] private TMP_Text clock = null;
    [SerializeField] private TMP_Text ammo = null;
    [SerializeField] private GameObject recharge = null;
    [SerializeField] private GameObject Enemies = null;
    [SerializeField] private int shots = 9;
    [SerializeField] private float shot_time = 3;
    [SerializeField] private GameObject overlay = null;
    [SerializeField] private GameObject overlay2 = null;
    private int shields = 0;
    private float cooldown = 0;


    // Update is called once per frame
    void Update()
    {
        gameTime -= Time.deltaTime;
        // UpdateLevelTimer(gameTime);
        if(cooldown > 0)
        {
            recharge.SetActive(true);
            cooldown -= Time.deltaTime;
            recharge.GetComponent<Slider>().value = cooldown/shot_time;

        }
        else
        {
            recharge.SetActive(false);
        }
        if(Enemies.transform.childCount <= 0)
        {
            Debug.Log("Game End - you win!");
            Globals.winDHunt = true;
            Globals.transitionToPlatformer();
            return;
        }
       
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = (pos);
        
        
        if (Input.GetMouseButtonUp(0))
        {         
            if(shots > 0 && cooldown <= 0)
            {
                RaycastHit2D hit=Physics2D.Raycast(pos, Vector2.zero, 0f);
                
                if (hit)
                {
                    hit.transform.gameObject.GetComponent<Enemy>().On_hit();
                }
                shots--;
                cooldown = shot_time;
                // overlay.SetActive(true);
                flash1();
                Invoke(nameof(flash2), 0.05f);
                Invoke(nameof(flash3), 0.05f);
            }
        }

        ammo.text = "Ammo: " + shots.ToString("00");
    }

    void flash1()
    {
        
        overlay.SetActive(true);
        overlay.GetComponent<Image>().color = new Color32(255,0,0,128); 
    }

    void flash2()
    {
        overlay.GetComponent<Image>().color = new Color32(255,255,0,128); 
    }

    void flash3()
    {
        overlay.SetActive(false);
    }
    void flash4()
    {
        overlay.SetActive(false);
        overlay2.SetActive(true);
    }

    void flash5()
    {
        Debug.Log("Return to Platformer");
    }

    public void get_hit()
    {
        // if (shields <= 0)
        // {
        //     Debug.Log("Game over"); 
        // }
        // else
        // {
        //     shields--;
        // }



        Debug.Log("Game over"); 
        this.enabled = false;
        flash1();
        Invoke(nameof(flash4), 0.02f);
        Invoke(nameof(flash5), 2.0f);

    }

    // public void UpdateLevelTimer(float totalSeconds)
    // {
    //     int minutes = Mathf.FloorToInt(totalSeconds / 60f);
    //     int seconds = Mathf.RoundToInt(totalSeconds % 60f);
 
    //     string formatedSeconds = seconds.ToString();
 
    //     if (seconds == 60)
    //     {
    //         seconds = 0;
    //         minutes += 1;
    //     }
 
    //     clock.text = minutes.ToString("00") + ":" + seconds.ToString("00");
 
    //     if (totalSeconds <= 0f)
    //     {
    //         Debug.Log("Game over");
    //     }
 
    // }
}
