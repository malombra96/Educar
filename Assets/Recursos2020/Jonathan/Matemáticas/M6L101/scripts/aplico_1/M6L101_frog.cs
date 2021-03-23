using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M6L101_frog : MonoBehaviour
{
    public GameObject Point;
    public GameObject Player;
    public float speed;
    public bool e;
    public Vector3 inicio;

    // Update is called once per frame
    void Update()
    {
        if (e)
        {
            float step = speed * Time.deltaTime;
            Player.transform.position = Vector2.MoveTowards(Player.transform.position, Point.transform.position, step);
        }
    }

    public void Move()
    {
        e = true;
    }
}
