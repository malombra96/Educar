using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8A57_Control : MonoBehaviour
{
    [Header("Control navegacion :")] public ControlNavegacion Navegacion;
    public int contador;
    [Header("layout a ir :")] public GameObject control2o;
    
    [Header("opciones :")] public List<GameObject> opciones;
    
    public enum aplico
    {
        uno,
        dos,
    }

    [Header("tipo de aplico :")] public aplico TipoAplico;
    

    public void ResetAll()
    {
        contador = 0;
        switch (TipoAplico)
        {
            case aplico.uno:
                foreach (var opcione in opciones)
                    opcione.GetComponent<Toggle>().interactable = true;
                break;
            case aplico.dos:

                foreach (var preguntas in GetComponent<M8A57_Ruleta>().preguntas)
                {
                    preguntas.GetComponent<M8A57_Manager_Aplico2>().resetAll();                    
                }
                break;
        }        

    }
}
