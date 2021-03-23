using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M8L56Colliders : MonoBehaviour
{
    M8L56ControlJuego controlJuego;
    public int puntos;

    //Start is called before the first frame update
    void Start()
    {
        controlJuego = FindObjectOfType<M8L56ControlJuego>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<M8L56Dardos>().acerte)
        {
            collision.GetComponent<M8L56Dardos>().acerte = true;
            darpunto(collision.GetComponent<M8L56Dardos>().acerte);            
        }
    }
    public void darpunto(bool p)
    {
        if (!p)
            controlJuego.puntos(Random.Range(1, 5));
        else
            controlJuego.puntos(Random.Range(puntos - 19, puntos));        
    }
}
