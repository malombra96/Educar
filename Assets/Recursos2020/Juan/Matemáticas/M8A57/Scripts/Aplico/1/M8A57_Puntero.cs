using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class M8A57_Puntero : MonoBehaviour
{
    private Vector3 mousePos;

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -5;
        transform.position = mousePos;        

    }
}
