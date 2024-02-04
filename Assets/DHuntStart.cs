using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DHuntStart : MonoBehaviour
{
    public string currentSceneName;
    public string dHuntSceneName;
    public GameObject destroyObj;
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
        if (Globals.winDHunt)
        {
            //Remove the box collider so it doesn't trigger again
            Destroy(destroyObj);
            Destroy(gameObject);
        } else
        {
            Globals.coords = new Vector2(transform.position.x, transform.position.y + 1);
            Globals.DHunt = true;
            Globals.scene = currentSceneName;
            SceneManager.LoadScene(dHuntSceneName);
        }
    }
}
