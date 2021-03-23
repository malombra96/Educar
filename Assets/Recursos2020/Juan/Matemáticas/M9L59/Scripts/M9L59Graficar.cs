using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M9L59Graficar : MonoBehaviour
{
    public Button BotonGraficar;
    string respuesta_1; 
    string respuesta_2;
    string respuesta_3;
    string respuesta_4;
    public GameObject grafica;
    public bool fraccion;
    ControlAudio controlAudio;

    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();

        respuesta_1 = transform.GetChild(0).GetComponent<M9L59BehaviourInputField>().respuestaCorrecta[0];
        respuesta_2 = transform.GetChild(1).GetComponent<M9L59BehaviourInputField>().respuestaCorrecta[0];
        if (fraccion)
        {
            respuesta_3 = transform.GetChild(2).GetComponent<M9L59BehaviourInputField>().respuestaCorrecta[0];
            respuesta_4 = transform.GetChild(3).GetComponent<M9L59BehaviourInputField>().respuestaCorrecta[0];
        }
        BotonGraficar.onClick.AddListener(graficar);
        BotonGraficar.gameObject.SetActive(false);
        grafica.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!fraccion)
        {
            if (respuesta_1 == transform.GetChild(0).GetComponent<InputField>().text)
            {
                if (respuesta_2 == transform.GetChild(1).GetComponent<InputField>().text)
                {
                    transform.GetChild(0).GetComponent<InputField>().interactable = false;
                    transform.GetChild(1).GetComponent<InputField>().interactable = false;
                    BotonGraficar.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            if (respuesta_1 == transform.GetChild(0).GetComponent<InputField>().text && respuesta_2 == transform.GetChild(1).GetComponent<InputField>().text)
            {
                if (respuesta_3 == transform.GetChild(2).GetComponent<InputField>().text && respuesta_4 == transform.GetChild(3).GetComponent<InputField>().text)
                {
                    transform.GetChild(0).GetComponent<InputField>().interactable = false;
                    transform.GetChild(1).GetComponent<InputField>().interactable = false;
                    transform.GetChild(2).GetComponent<InputField>().interactable = false;
                    transform.GetChild(3).GetComponent<InputField>().interactable = false;
                    BotonGraficar.gameObject.SetActive(true);
                }
            }
        }
        
    }
    void graficar()
    {
        controlAudio.PlayAudio(0);
        grafica.SetActive(true);
    }
}
