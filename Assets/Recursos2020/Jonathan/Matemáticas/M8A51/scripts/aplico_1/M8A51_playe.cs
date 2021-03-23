using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8A51_playe : MonoBehaviour
{
    public GameObject seleccionar;
    public bool x,y;
    public Vector3 inicial;
    public Sprite orignal;
    public GameObject pausa;
   

    void Start()
    {
        x = false;
        seleccionar.SetActive(false);
        inicial = GetComponent<RectTransform>().anchoredPosition;
    }

    public void pause() {
        y = true;
    }

    public void resumen() {
        y = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!y) {
            if (Input.GetKey(KeyCode.Space))
            {
                MovePlayer();
            }
        }
        
       
    }

    public void MovePlayer() {
        if (!x) {
            pausa.SetActive(false);
            x = true;
            GetComponent<Animator>().SetInteger("x", 1);
            seleccionar.SetActive(true);
        }
        
    }
}
