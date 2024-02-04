using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Globals : MonoBehaviour
{
    public static string scene;
    public static Vector2 coords;
    public static bool DHunt = false;
    public static bool winDHunt = false;
    public static bool canJetpack = false;
    public static bool hasFuel = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void winDGame()
    {
        winDHunt = true;
    }

    public static void transitionToPlatformer()
    {
        SceneManager.LoadScene(scene);
    }
}
