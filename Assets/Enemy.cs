using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Math;

public class Enemy : MonoBehaviour
{

    private int lastDir = 1;
    List<Vector3> allPossibleDirections = new List<Vector3> {
            new Vector3(0f, 1f, 0f),
            new Vector3(-1f, 0f, 0f),
            new Vector3(0f, -1f, 0f),
            new Vector3(1f, 0f, 0f)
            };

    private float size = 15/2;

    public enum movementType{
        RandomMove,
        Circle,
        Box,
        Triangle
    }

    [SerializeField] private GameObject BaseController = null;
    [SerializeField] private int shields = 0;

    [SerializeField] private float offsetTime = 0f;
    [SerializeField] private float Offsetx = 0f;
    [SerializeField] private float Offsety = 0f;
    [SerializeField] private float radius = 30f;
    [SerializeField] private movementType flightType = movementType.RandomMove;
    [SerializeField] private float speed = 75f;
    [SerializeField] private float killTime = 20;
    [SerializeField] private float scale = 0.5f;


    private Vector2 Offset;
    private float time = 0f;
    private bool needToStart = true;
    private float angle = 0f;
    private int vertex = 1;


    void Start()
    {
        Offset = new Vector2(Offsetx, Offsety);
    }

    void FixedUpdate()
    {
        time += Time.deltaTime;
        if(offsetTime > time)
        {
            return;
        }
        
        if(offsetTime + killTime <= time)
        {
            BaseController.GetComponent<AleinAttackBase>().get_hit();
            Destroy(gameObject);   
        }

        if (needToStart)
        {
            switch (flightType)
            {
                case movementType.RandomMove:
                    transform.position = Offset;
                    break;
                case movementType.Circle:
                case movementType.Triangle:
                case movementType.Box:
                    transform.position = Offset + new Vector2(0, radius);
                    break;

            }
            needToStart = false;
        }
        transform.localScale += new Vector3(scale, scale, scale) * Time.deltaTime;

        switch (flightType)
        {
            case movementType.RandomMove:
                    float randomDir = Random.Range(1, 10);
                    if (randomDir <= 4f)
                    {
                        transform.position += allPossibleDirections[lastDir % 4] * speed * 3 * Time.deltaTime;
                        lastDir = lastDir % 4;
                    }
                    else if (randomDir <= 6.5f)
                    {
                        transform.position += allPossibleDirections[(lastDir + 1) % 4] * speed* 3 * Time.deltaTime;
                        lastDir = (lastDir + 1) % 4;
                    }
                    else if (randomDir <= 7.5f)
                    {
                        transform.position += allPossibleDirections[(lastDir + 2) % 4] * speed * 3 * Time.deltaTime;
                        lastDir = (lastDir + 2) % 4;
                    }
                    else
                    {
                        transform.position += allPossibleDirections[(lastDir + 3) % 4] * speed * 3 * Time.deltaTime;
                        lastDir = (lastDir + 3) % 4;
                    }

                    if(transform.position.x <= -Screen.width/4)
                    {
                        transform.position= new Vector3(-Screen.width/4, transform.position.y, 0);
                    }
                    else if(transform.position.x >= Screen.width/4)
                    {
                        transform.position = new Vector3(Screen.width/4, transform.position.y, 0);
                    }

                    if(transform.position.y <= -Screen.height/4)
                    {
                        transform.position = new Vector3(transform.position.x, -Screen.height/4, 0);
                    }
                    else if(transform.position.y >= Screen.height/4)
                    {
                        transform.position = new Vector3(transform.position.x, Screen.height/4, 0);
                    }
                    break;

            case movementType.Circle:
                angle += (speed / (radius * Mathf.PI * 2.0f)) * Time.deltaTime;
                transform.position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
                break;

            case movementType.Triangle:
                if(vertex == 1)
                {
                    transform.position += new Vector3(-0.577f, -0.866f, 0) * speed * Time.deltaTime;
                    if(transform.position.x <= Offsetx-radius*0.866f) vertex = 2;
                }
                else if(vertex == 2)
                {
                    transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
                    if(transform.position.x >= Offsetx+radius*0.866f) vertex = 3;
                }
                else if(vertex == 3)
                {
                    transform.position += new Vector3(-0.577f, 0.866f, 0) * speed * Time.deltaTime;
                    if(transform.position.x <= Offsetx) vertex = 1;
                }
                break;
            case movementType.Box:
                if(vertex == 1)
                {
                    transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
                    if(transform.position.x <= Offsetx-radius) vertex = 2;
                }
                else if(vertex == 2)
                {
                    transform.position += new Vector3(0, -speed, 0) * Time.deltaTime;
                    if(transform.position.y <= Offsety-radius) vertex = 3;
                }
                else if(vertex == 3)
                {
                    transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
                    if(transform.position.x >= Offsetx+radius) vertex = 4;
                }
                else if(vertex == 4)
                {
                    transform.position += new Vector3(0, speed, 0) * Time.deltaTime;
                    if(transform.position.y >= Offsety+radius) vertex = 1;
                }
                break;
        }

    }



    public void On_hit()
    {
        if (shields <= 0)
        {
            Destroy(gameObject);   
        }
        else
        {
            shields--;
        }
    }
}
