using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class M8A57_Manager_3 : MonoBehaviour
{

    [Header("Ruleta :")] public Animator ruleta;

    [Header("Button Validar")] public Button botonValidar;

    [HideInInspector] public ControlPuntaje puntaje;
    [HideInInspector] public ControlNavegacion navegacion;
    private double muestra;

    void Awake ()
    {
        puntaje = FindObjectOfType<ControlPuntaje>();
        navegacion = FindObjectOfType<ControlNavegacion>();
        muestra = puntaje._rightAnswers;
        botonValidar.onClick.AddListener(Ready);
    }

    public void Ready()
    {
        StartCoroutine(tiempo_demora());
    }

    public void Check()
    {
        if ((muestra + 2) == puntaje._rightAnswers)
        {
            ruleta.Play("Right");
            StartCoroutine(tiempo());
        }
        else
        {
            ruleta.Play("Wrong");
            StartCoroutine(tiempo());
        }
    }

    IEnumerator tiempo()
    {
        yield return new WaitForSeconds(2f);
        navegacion.Forward();
    }
    IEnumerator tiempo_demora()
    {
        yield return new WaitForSeconds(1f);
        Check();
    }
}
