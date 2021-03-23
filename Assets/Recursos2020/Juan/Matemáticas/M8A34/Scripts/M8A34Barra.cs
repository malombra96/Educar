using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M8A34Barra : MonoBehaviour
{
    public bool fuerza;
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            this.GetComponent<Animator>().speed = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        fuerza = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        fuerza = false;
    }
}
