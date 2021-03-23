using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class M8L56ControlJuego : MonoBehaviour
{
    ControlAudio controlAudio;
    public Button ahora;

    public GameObject Puntos;

    public TipoDeAplico tipoDeAplico;

    public GameObject Pregunta, Diana, fuerzaHorizontal, fuerzaVertical, Proyectil;

    int lanzamientos;
    int i = 0;
    int j = 0;
    // Start is called before the first frame update
    void Awake()
    {
        for (int k = 0; k < Puntos.transform.childCount; k++)
            Puntos.transform.GetChild(k).GetComponent<Text>().text = "";
        controlAudio = FindObjectOfType<ControlAudio>();        
        ahora.onClick.AddListener( delegate { StartCoroutine(lanzar()); });
    }

    IEnumerator lanzar()
    {
        controlAudio.PlayAudio(0);
        ahora.interactable = false;

        fuerzaHorizontal.GetComponent<Animator>().speed = 0;
        fuerzaVertical.GetComponent<Animator>().speed = 0;

        float x = fuerzaHorizontal.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.x;
        float y = fuerzaVertical.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.y;

        Proyectil.transform.GetChild(lanzamientos).gameObject.SetActive(false);
        Diana.transform.GetChild(lanzamientos).gameObject.SetActive(true);
        Diana.transform.GetChild(lanzamientos).GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);

        if (lanzamientos == 0)
        {
            for (int o = Diana.transform.childCount - 1; o > 0; o--)
                if (Diana.transform.GetChild(o).GetComponent<M8L56Colliders>())
                    Diana.transform.GetChild(o).gameObject.SetActive(true);

        }

        lanzamientos++;

        if (lanzamientos == Proyectil.transform.childCount)
        {
            yield return new WaitForSeconds(1);
            operacion();
        }
        else
        {
            yield return new WaitForSeconds(2);           

            j++;
            ahora.interactable = true;
            fuerzaHorizontal.GetComponent<Animator>().speed = 1;
            fuerzaVertical.GetComponent<Animator>().speed = 1;            
        }
        i = 0;
    }

    public enum TipoDeAplico
    {
        DesviacionMedia,
        Varianza
    }

    void operacion()
    {
        float media = 0;
        float respuesta = 0;

        for (int k = 0; k < Puntos.transform.childCount; k++)
            media += Convert.ToSingle(Puntos.transform.GetChild(k).GetComponent<Text>().text);

        media /= Puntos.transform.childCount;

        switch (tipoDeAplico)
        {
            case TipoDeAplico.DesviacionMedia:

                for (int k = 0; k < Puntos.transform.childCount; k++)
                {
                    float temp= ((Convert.ToSingle(Puntos.transform.GetChild(k).GetComponent<Text>().text) - media));
                    if (temp < 0)
                        temp *= -1;
                    respuesta += temp;
                }               
                break;

            case TipoDeAplico.Varianza:

                for (int k = 0; k < Puntos.transform.childCount; k++)
                {
                    float temp = ((Convert.ToSingle(Puntos.transform.GetChild(k).GetComponent<Text>().text) - media));
                                   
                    if (temp < 0)
                        temp *= -1;
                    temp *= temp;                   

                    respuesta += temp;
                }                
                break;
        }

        respuesta /= Puntos.transform.childCount;        
        Pregunta.SetActive(true);        
        respuesta = Convert.ToSingle(Math.Round(Convert.ToDouble(respuesta), 2));
        print("Respuesta: " + respuesta);

        Pregunta.GetComponent<ManagerInputField>()._groupInputField[0].
            _inputFields[0].GetComponent<BehaviourInputField>().respuestaCorrecta.Add(Convert.ToString(respuesta));

        char[] Char = Convert.ToString(respuesta).ToCharArray();
        Pregunta.GetComponent<ManagerInputField>()._groupInputField[0]._inputFields[0].GetComponent<InputField>().characterLimit = Char.Length;
        for (int x = 0; x < Char.Length; x++)
        {
            if (Char[x].ToString() == ".")
            {
                Pregunta.GetComponent<ManagerInputField>()._groupInputField[0].
                    _inputFields[0].GetComponent<BehaviourInputField>().respuestaCorrecta.Add(Convert.ToString(respuesta).Replace(".", ","));
                break;
            }
            else if (Char[x].ToString() == ",")
            {
                Pregunta.GetComponent<ManagerInputField>()._groupInputField[0].
                    _inputFields[0].GetComponent<BehaviourInputField>().respuestaCorrecta.Add(Convert.ToString(respuesta).Replace(",", "."));
                break;
            }
        }
    }
    public void puntos(int puntos)
    {
        i++;
        if (i == 1)
        {            
            Puntos.transform.GetChild(j).GetComponent<Text>().text = Convert.ToString(puntos);
        }

    }        

    public void resetAll()
    {
        Pregunta.GetComponent<ManagerInputField>().resetAll();
        if (Pregunta.GetComponent<ManagerInputField>()._groupInputField.Count>0)
            Pregunta.GetComponent<ManagerInputField>()._groupInputField[0]._inputFields[0].GetComponent<BehaviourInputField>().respuestaCorrecta.Clear();
        Pregunta.SetActive(false);

        ahora.interactable = true;
        fuerzaHorizontal.GetComponent<Animator>().speed = 1;
        fuerzaVertical.GetComponent<Animator>().speed = 1;
        i = 0;
        j = 0;
        lanzamientos = 0;

        for (int x = 0; x < Diana.transform.childCount; x++)
        {
            if (Diana.transform.GetChild(x).GetComponent<M8L56Dardos>())
            {
                Diana.transform.GetChild(x).gameObject.SetActive(false);
                Diana.transform.GetChild(x).GetComponent<M8L56Dardos>().acerte = false;
            }
            else if (Diana.transform.GetChild(x).GetComponent<M8L56Colliders>())
                      Diana.transform.GetChild(x).gameObject.SetActive(false);
            
        }

        for (int x = 0; x < Puntos.transform.childCount; x++)
        {
            Proyectil.transform.GetChild(x).gameObject.SetActive(true);
            Puntos.transform.GetChild(x).GetComponent<Text>().text = "";
        }

    }   
}
    
