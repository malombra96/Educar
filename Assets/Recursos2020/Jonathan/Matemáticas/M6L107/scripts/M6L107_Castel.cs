using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M6L107_Castel : MonoBehaviour
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
            Player.GetComponent<RectTransform>().anchoredPosition = Vector2.MoveTowards(Player.GetComponent<RectTransform>().anchoredPosition, Point.GetComponent<RectTransform>().anchoredPosition, step);
        }
    }

    public void Move() {
        e = true;
    }

    public void StropAnimation() {
        GetComponent<Animator>().enabled = false;
    }
}
