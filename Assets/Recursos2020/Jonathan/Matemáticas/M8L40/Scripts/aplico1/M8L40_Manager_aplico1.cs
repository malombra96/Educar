using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8L40_Manager_aplico1 : MonoBehaviour
{
    private ControlNavegacion navegacion;
    private ControlAudio audio;
    private ControlPuntaje puntaje;

    [Header("Entradas :")] public M8L40_Inputs Inputs;
    [Header("Lifes :")] public M8L40_Lifes lifes;
    public int layout;
    public Button validar;
    public bool primero;
    public Button informacion, pregunta;
    //  [Header("NavBar :")] public GameObject navbar;

    private void Awake()
    {
        navegacion = FindObjectOfType<ControlNavegacion>();
        puntaje = FindObjectOfType<ControlPuntaje>();
        audio = FindObjectOfType<ControlAudio>();
    }

    private void Start()
    {
        primero = true;
    }
    public void Wrong()
    {
        
        audio.PlayAudio(2);
        lifes.Refresh();
        if (lifes.contador == 0)
        {
            StartCoroutine(time_next(4));
        }
        else
        {
            StartCoroutine(time_change());
        }
    }

    public void Right()
    {
        audio.PlayAudio(1);
        Check();

    }

    public void Check()
    {
        
        switch (Inputs.tipo)
        {
            case M8L40_Inputs.aplicotipo.normal:
                if (Inputs.contador == 2)
                    StartCoroutine(time_nextg(3));
                break;
            case M8L40_Inputs.aplicotipo.ultimo:
                if (Inputs.contador == 2)
                {
                    StartCoroutine(time_nextg(3));
                }           
                break;
        }
        
    }

    public void RestarAll()
    {
        if (primero) {
            Inputs.RestarAll();
            Inputs.Player.SetActive(false);
            Inputs.plataformas.SetActive(false);
            Inputs.Player.SetActive(true);
            Inputs.plataformas.SetActive(true);
            Inputs.contador = 0;
            Inputs.Activacion();
            lifes.contador = 3;
            for (int x = 0; x < lifes.lifes.Count; x++)
            {
                lifes.lifes[x].GetComponent<Image>().sprite = lifes.lifes[x].GetComponent<BehaviourSprite>()._selection;
            }
            informacion.enabled = true;
            pregunta.enabled = true;
        }
        
    }

    IEnumerator time_change()
    {
        yield return new WaitForSeconds(4f);
        Inputs.RestarAll();
        Inputs.Player.SetActive(false);
        Inputs.plataformas.SetActive(false);
        Inputs.Player.SetActive(true);
        Inputs.plataformas.SetActive(true);
        Inputs.contador = 0;
        Inputs.Activacion();
        validar.interactable = false;
        //informacion.enabled = true;
        //pregunta.enabled = true;
    }

    IEnumerator time_next(float time)
    {
        yield return new WaitForSeconds(time);
        navegacion.GoToLayout(layout);
        //informacion.enabled = true;
        //pregunta.enabled = true;
    }
    
    IEnumerator time_nextg(float time)
    {
        yield return new WaitForSeconds(time);
        
        switch (Inputs.tipo)
        {
            case M8L40_Inputs.aplicotipo.normal:
                for (int i = 0; i < 3; i++)
                    puntaje.IncreaseScore();
                navegacion.GoToLayout(layout);
                break;
            case M8L40_Inputs.aplicotipo.ultimo:
                for (int i = 0; i < 6; i++)
                    puntaje.IncreaseScore();
                navegacion.GoToLayout(layout);
                break;
        }
        informacion.enabled = true;
        pregunta.enabled = true;
    }
    
}
