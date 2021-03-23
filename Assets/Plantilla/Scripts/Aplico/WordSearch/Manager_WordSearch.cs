using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Manager_WordSearch : MonoBehaviour
{
    [HideInInspector] public List<GameObject> listaObjetoLetra;

    [HideInInspector]public List<String> listaLetra,listaCorrecta;

    [Header("Current Word")]
    public String palabraActual;

    ControlAudio _controlAudio;

    ControlNavegacion _controlNavegacion;
    
    ControlPuntaje _controlPuntaje;

    [Header("Number of Attempts")]
    [Range(1,5)] public int intentos = 3;
    
    [HideInInspector] public int correctas;

   [HideInInspector] public bool calificado,state;

    [FormerlySerializedAs("_wordSearch")] Generate_WordSearch generateWordSearch;

    private void Awake()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
        _controlPuntaje = FindObjectOfType<ControlPuntaje>();
        generateWordSearch = transform.GetChild(0).GetChild(0).GetComponent<Generate_WordSearch>();

        foreach (var palabras in generateWordSearch.palabrasVerticales.Concat(generateWordSearch.palabrasHorizontales))
        {
            listaCorrecta.Add(palabras.ToUpper());
        }
    }

    /// verifica si la actividad termino
    private void CheckActivity()
    {
        if (intentos == 0 || (correctas == listaCorrecta.Count))
        {
            foreach (var toogle in generateWordSearch.listaToogles)
                toogle.GetComponent<Toggle>().enabled = false;

            _controlNavegacion.Forward(2);
        }
    }

    // Cuando no realiza la actividad pero se revisa
    void OnEnable()
    {
        if(GetComponent<BehaviourLayout>()._isEvaluated)
        {
            foreach (var toogle in generateWordSearch.listaToogles)
                toogle.GetComponent<Toggle>().enabled = false;
        }
    }
    

    /// <summary>
    /// verifica si la seleccion es una palabra correcta
    /// </summary>
    /// <param name="letra"></param>
    public void AñadirALista(GameObject letra)
    {
       // print(letra.transform.GetChild(0).GetComponent<Text>().text);
        listaObjetoLetra.Add(letra);
        listaLetra.Add(letra.transform.GetChild(0).GetComponent<Text>().text);
        
        if (listaObjetoLetra.Count > 1)
        {
            for (int i = 0; i < listaObjetoLetra.Count-1; i++)
            {
                // verifica si el boton pertenece al mismo grupo
                if (listaObjetoLetra[i].GetComponent<Behaviour_Character>().grupo !=
                    listaObjetoLetra[i+1].GetComponent<Behaviour_Character>().grupo)
                {
                    intentos--;
                    _controlAudio.PlayAudio(2);
                    EliminarRegistro();
                }
                else
                {
                    // verifica si el boton es distinto al grupo 100, para luego hacer la verificacion si es un anagrama
                    if (listaObjetoLetra[i].GetComponent<Behaviour_Character>().grupo != 100)
                    {
                        palabraActual = string.Join( "", listaLetra.ToArray() );
                        
                        if (!state)
                        {
                            for (int k = 0; k < listaCorrecta.Count; k++)
                            {
                                if (SonAnagramas2(palabraActual, listaCorrecta[k]))
                                {
                                    //print("xa");
                                    foreach (var obj in listaObjetoLetra)
                                    {
                                        obj.GetComponent<Behaviour_Character>().calificado = true;
                                    } 
                                   
                                    correctas++;
                                    _controlAudio.PlayAudio(1);

                                    if (_controlPuntaje.GetComponent<ControlPuntaje>() != null)
                                        _controlPuntaje.GetComponent<ControlPuntaje>().IncreaseScore();
                                   
                                   
                                    EliminarRegistro();
                                }

                            }
                            
                           
                        }
                    }
                    
                    else
                    {
                        intentos--;
                        _controlAudio.PlayAudio(2);
                        EliminarRegistro();
                    }
                }
            }
        }

        CheckActivity();
    }
    
    
    /// <summary>
    /// ordena cada uno de los caractere de las dos palabra en forma ascendente para compararlas y retornar si son anagramas
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public bool SonAnagramas2(string a,string b) 
    {
       
            string aa = String.Concat(a.OrderBy(c => c));
            string bb = String.Concat(b.OrderBy(c => c));

            if (aa == bb)
            {
                state = true;
                return true;
            }

            return false;

    }

    public void EliminarRegistro()
    {
       
        palabraActual = "";
        foreach (var letras in listaObjetoLetra)
        {
            letras.GetComponent<Toggle>().isOn = false;
        }

        StartCoroutine(LimpiarLista());
    }

    IEnumerator LimpiarLista()
    {
        yield return new WaitForSeconds(0.1f);
        listaObjetoLetra.Clear();
        listaLetra.Clear();
        state = false;
    }

 
    public void resetAll()
    {
        state = false;
        listaObjetoLetra.Clear();
        listaLetra.Clear();
        calificado = false;
        correctas = 0;
        intentos = 3;
        palabraActual = "";
        
        if (_controlPuntaje.GetComponent<ControlPuntaje>() != null)
            _controlPuntaje.GetComponent<ControlPuntaje>().resetScore();
        
        generateWordSearch.resetAll();
    }
}
