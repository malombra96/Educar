using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M8L40_Transicion : MonoBehaviour
{
    [Header("Tiempo de transicion :")] public float tiempo;
    private ControlNavegacion navegacion;
    // [HideInInspector] public GameObject navbar;
    public int layout;


    private void Awake()
    {
        navegacion = FindObjectOfType<ControlNavegacion>();
     //   navbar = GameObject.Find("NavBar Leccion");
    }

    private void Start()
    {
        StartCoroutine(next());
    }

    IEnumerator next()
    {
        //navbar.SetActive(false);
        yield return new WaitForSeconds(tiempo);
        // navbar.SetActive(true);
        navegacion.GoToLayout(layout);
    }

}
