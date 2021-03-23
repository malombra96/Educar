using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M10A41_Mira : MonoBehaviour
{
    private Vector3 mousePos;
    public bool Mover;
    public bool MoverAlClick;

    void Start()
    {

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -9.5f;
        Mover = true;
        MoverAlClick = true;
        gameObject.transform.position = mousePos;
    }
    void Update()
    {
        if (Mover)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = -9.5f;
            gameObject.transform.position = mousePos;
        }
    }
    public void posicionOpcion(Transform t)
    {
        transform.position = t.position;
    }
}
