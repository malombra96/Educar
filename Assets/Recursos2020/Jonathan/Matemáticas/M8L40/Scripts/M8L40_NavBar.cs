using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M8L40_NavBar : MonoBehaviour
{
    [Header("NavBar :")] public GameObject navbar;
    
    public enum accion
    {
        prender,
        apagar
    }
    [Header("que se va hacer con el navbar :")] public accion hacer;
    void Start()
    {
        switch (hacer)
        {
            case accion.prender:
                navbar.SetActive(true);
                break;
            case accion.apagar:
                navbar.SetActive(false);
                break;
        }
    }
    
    public void Prender()
    {
        navbar.SetActive(true);
    }
    
    public void Apagar()
    {
        navbar.SetActive(false);
    }
}
