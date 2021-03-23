using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M9A81_jugador : MonoBehaviour
{
    // Start is called before the first frame update
    public bool mover;
    public float movespeed ;
    public Vector3 inicial;
    void Start()
    {
        mover = false;
        movespeed = 0;
        inicial = GetComponent<RectTransform>().anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (mover)
        {

            movespeed+=0.0000001f;
            transform.position = new Vector3(transform.position.x + movespeed, transform.position.y,transform.position.z);
            GetComponent<Animator>().SetBool("play", true);
        }
        else {
            GetComponent<Animator>().SetBool("play", false);
        }
    }
}

