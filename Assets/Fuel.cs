using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    public GameObject fuelSign;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Globals.hasFuel = true;
        GameObject.FindWithTag("Ship").GetComponent<Animator>().SetTrigger("IdleOn");
        Destroy(fuelSign);
        Destroy(gameObject);
    }
}
