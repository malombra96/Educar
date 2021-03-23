using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M8L40_Plataformas : MonoBehaviour
{
    [Header("Plataformas :")] public List<GameObject> plataformas;

    [Header("Container Entradas :")]  public M8L40_Inputs inputs;
    
    void Start()
    {
        plataformas[0].SetActive(true);
        plataformas[2].SetActive(true);
        plataformas[1].SetActive(false);
    }

    private void OnEnable()
    {
        plataformas[0].SetActive(true);
        plataformas[2].SetActive(true);
        plataformas[1].SetActive(false);
    }
}
