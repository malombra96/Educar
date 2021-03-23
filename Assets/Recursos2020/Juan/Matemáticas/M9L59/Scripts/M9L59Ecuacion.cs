using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M9L59Ecuacion : MonoBehaviour
{
    public int pregunta;
    public bool calificado;
    float x;
    float y;
    public Text numero_1, numero_2;   
    // Start is called before the first frame update
    void Start()
    {        
        numero_1 = transform.GetChild(0).GetComponent<InputField>().textComponent;
        numero_2 = transform.GetChild(1).GetComponent<InputField>().textComponent;
        
    }

    private void Update()
    {
        if (numero_1.text != "" && numero_2.text != "")
        {
            x = Convert.ToSingle(numero_1.text);
            y = Convert.ToSingle(numero_2.text);
            
            verificar();
        }
        else
        {
            calificado = false;
        }
    }
    void verificar()
    {
        float respuesta = 0;

        if (pregunta == 1 && !calificado)
        {
            respuesta = ((2 * x) + (1 * y));
            if (respuesta < 3)
            {
                float temp = respuesta;
                respuesta = (1 * x) + (1 * y);

                if (respuesta < 1)
                {
                    calificado = true;
                    transform.GetChild(0).GetComponent<M9L59BehaviourInputField>().respuestaCorrecta.Add(Convert.ToString(numero_1.text));
                    transform.GetChild(1).GetComponent<M9L59BehaviourInputField>().respuestaCorrecta.Add(Convert.ToString(numero_2.text));
                }
            }
        }
        else if (pregunta == 2 && !calificado)
        {
            respuesta = ((1 * y) - (1 * x));
            if (respuesta < 3)
            {
                float temp = respuesta;
                respuesta = (1 * y) + (2 * x);

                if (respuesta > 1)
                {
                    calificado = true;
                    transform.GetChild(0).GetComponent<M9L59BehaviourInputField>().respuestaCorrecta.Add(Convert.ToString(numero_1.text));
                    transform.GetChild(1).GetComponent<M9L59BehaviourInputField>().respuestaCorrecta.Add(Convert.ToString(numero_2.text));
                }
            }
        }
        else if (pregunta == 3 && !calificado)
        {
            respuesta = ((2 * y) - (4 * x));
            if (respuesta < 8)
            {
                float temp = respuesta;
                respuesta = (3 * y) + (6 * x);

                if (respuesta < -9)
                {
                    calificado = true;
                    transform.GetChild(0).GetComponent<M9L59BehaviourInputField>().respuestaCorrecta.Add(Convert.ToString(numero_1.text));
                    transform.GetChild(1).GetComponent<M9L59BehaviourInputField>().respuestaCorrecta.Add(Convert.ToString(numero_2.text));
                }
            }
        }
        else if (pregunta == 4 && !calificado)
        {
            respuesta = ((3 * y) - (4 * x));
            if (respuesta > 8)
            {
                float temp = respuesta;
                respuesta = (5 * y) + (6 * x);

                if (respuesta < 3)
                {
                    calificado = true;
                    transform.GetChild(0).GetComponent<M9L59BehaviourInputField>().respuestaCorrecta.Add(Convert.ToString(numero_1.text));
                    transform.GetChild(1).GetComponent<M9L59BehaviourInputField>().respuestaCorrecta.Add(Convert.ToString(numero_2.text));
                }
            }
            else if (pregunta == 5 && !calificado)
            {
                respuesta = ((4 * y) + (5 * x));
                if (respuesta > 15)
                {
                    float temp = respuesta;
                    respuesta = (5 * y) - (9 * x);

                    if (respuesta < 13)
                    {
                        calificado = true;
                        transform.GetChild(0).GetComponent<M9L59BehaviourInputField>().respuestaCorrecta.Add(Convert.ToString(numero_1.text));
                        transform.GetChild(1).GetComponent<M9L59BehaviourInputField>().respuestaCorrecta.Add(Convert.ToString(numero_2.text));
                    }
                }
            }            
        }
    }
}
