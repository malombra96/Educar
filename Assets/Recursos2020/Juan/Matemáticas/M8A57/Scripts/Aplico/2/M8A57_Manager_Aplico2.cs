using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VideoHelper;
using UnityEngine;
using UnityEngine.UI;

public class M8A57_Manager_Aplico2 : MonoBehaviour
{
    [Header("Control :")] public GameObject control;    
    [Header("Toggle correspondiente :")] public GameObject imagen;
    bool respondida;
    [Header("Botón validar :")] public Button btn_validar;    
    ControlNavegacion controlNavegacion;

    int intentos;
    private void Start()
    {        
        controlNavegacion = FindObjectOfType<ControlNavegacion>();
        btn_validar.onClick.AddListener( delegate { StartCoroutine(Check()); });
        InvokeRepeating("verificador", .1f, .2f);
        control.GetComponent<M8A57_Ruleta>().preguntas.Add(this.gameObject);
    }

    void verificador()
    {
        if (respondida && control.GetComponent<M8A57_Control>().contador <= 5)
        {
            if (gameObject.activeSelf)
            {
                StartCoroutine(tiempo_gana(.1f));
            }
        }
    }

    IEnumerator Check()
    {
        yield return new WaitForSeconds(0.1f);
        intentos++;
        foreach (var input in GetComponent<ManagerInputField>()._groupInputField)
        {
            if (input._inputFields[0].text == input._inputFields[0].GetComponent<BehaviourInputField>().respuestaCorrecta[0] || intentos == 1)
            {
                control.GetComponent<M8A57_Control>().contador++;
                imagen.GetComponent<M8A57_Go2>().preguntaContestada = true;
            }
             
        }
        StartCoroutine(tiempo_gana(1.9f));
    }    

    IEnumerator tiempo_gana(float t)
    {
        yield return new WaitForSeconds(t);
        
        if (imagen.GetComponent<M8A57_Go2>().preguntaContestada)
        {            
            respondida = true;
            control.GetComponent<M8A57_Control>().opciones.Remove(imagen);
        }        
        else
        {            
            GetComponent<ManagerInputField>().resetAll();            
        }
        SeleccionadorPregunta();

    }    
    void SeleccionadorPregunta()
    {      
        
        if (control.GetComponent<M8A57_Control>().contador == 5)
        {
            controlNavegacion.GoToLayout(14);            
        }
        else
        {
            control.GetComponent<M8A57_Ruleta>().ruleta = true;
            controlNavegacion.GoToLayout(8);
        }
    }
    public void resetAll()
    {
        intentos = 0;
        GetComponent<ManagerInputField>().resetAll();
        respondida = false;
        control.GetComponent<M8A57_Ruleta>().ruleta = true;
        imagen.GetComponent<M8A57_Go2>().preguntaContestada = false;       
    }
}
