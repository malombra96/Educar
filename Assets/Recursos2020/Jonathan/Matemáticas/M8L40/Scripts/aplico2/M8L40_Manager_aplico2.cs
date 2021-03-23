using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8L40_Manager_aplico2 : MonoBehaviour
{
    [Header("Drags :")] public List<GameObject> _drags;
    [Header("Drops :")] public List<GameObject> _drops;
    [Header("Boton validar :")] public GameObject btn_validar;
    [Header("Player :")] public GameObject player;
    [Header("Lifes :")] public M8L40_Lifes life;

    private ControlAudio audio;
    private ControlNavegacion navegacion;
    private ControlPuntaje puntaje;
    public int layout;
    public bool primero;
    public Button informacion, pregunta;


    private void Awake()
    {
        audio = FindObjectOfType<ControlAudio>();
        navegacion = FindObjectOfType<ControlNavegacion>();
        puntaje = FindObjectOfType<ControlPuntaje>();
    }

    void Start()
    {
        btn_validar.GetComponent<Button>().interactable = false;
        btn_validar.GetComponent<Button>().onClick.AddListener(Check);
        primero = true;

    }

    public void Ready()
    {
        int contador = 0;
        for (int i = 0; i < _drags.Count; i++)
            if (_drags[i].GetComponent<M8L40_Drag>().stateDrop)
                contador++;

        if (contador == _drags.Count)
            btn_validar.GetComponent<Button>().interactable = true;
    }

    public void Check()
    {
        informacion.enabled = false;
        pregunta.enabled = false;
        btn_validar.GetComponent<Button>().interactable = false;
        for (int i = 0; i < _drags.Count; i++)
            _drags[i].GetComponent<M8L40_Drag>().op = false ;
        int conteo = 0;
        for (int i = 0; i < _drops.Count; i++)
        {
            if (_drops[i].GetComponent<M8L40_Drop>().respuesta && conteo == i)
            {
                player.GetComponent<Animator>().SetInteger("estado",i+1);
                conteo++;
            }
            else
            {
                if (!_drops[0].GetComponent<M8L40_Drop>().respuesta)
                    player.GetComponent<Animator>().SetInteger("estado",0);
            }
        }

        if (conteo == 5)
        {
           // print(conteo);
            StartCoroutine(tiempo_next());
            audio.PlayAudio(1);
            btn_validar.GetComponent<Image>().sprite = btn_validar.GetComponent<BehaviourSprite>()._right;
        }
        else
        {
            life.Refresh();
            StartCoroutine(tiempo());
            audio.PlayAudio(2);
            btn_validar.GetComponent<Image>().sprite = btn_validar.GetComponent<BehaviourSprite>()._wrong;
            conteo = 0;
            //informacion.enabled = true;
            //pregunta.enabled = true;
        }
    }

    IEnumerator tiempo()
    {
        yield return new WaitForSeconds(5f);
        player.SetActive(false);
        player.SetActive(true);
        foreach (var drag in _drags)
        {
            drag.transform.position = drag.GetComponent<M8L40_Drag>().posInicial;
            btn_validar.GetComponent<Image>().sprite = btn_validar.GetComponent<BehaviourSprite>()._default;
            drag.GetComponent<M8L40_Drag>().stateDrop = false;
        }
        for (int i = 0; i < _drags.Count; i++)
            _drags[i].GetComponent<M8L40_Drag>().op = true;
        informacion.enabled = true;
        pregunta.enabled = true;
    }

    public void ResetAll()
    {
        if (primero) {
            foreach (var drag in _drags)
                drag.transform.position = drag.GetComponent<M8L40_Drag>().posInicial;

            life.contador = 3;
            for (int i = 0; i < life.lifes.Count; i++)
            {

                life.lifes[i].GetComponent<Image>().sprite = life.lifes[i].GetComponent<BehaviourSprite>()._default;

            }
            for (int i = 0; i < _drags.Count; i++)
                _drags[i].GetComponent<M8L40_Drag>().op = true;
            informacion.enabled = true;
            pregunta.enabled = true;
        }
    }
    
    IEnumerator tiempo_next()
    {
        puntaje.IncreaseScore();
        yield return new WaitForSeconds(8f);
        navegacion.GoToLayout(layout);
    }
}
