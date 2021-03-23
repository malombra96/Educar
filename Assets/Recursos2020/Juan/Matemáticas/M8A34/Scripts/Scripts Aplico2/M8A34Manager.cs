using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class M8A34Manager : MonoBehaviour
{
    public GameObject acelerometro, pregunta, jugador, portada;
    ControlPuntaje controlPuntaje;
    ControlNavegacion controlNavegacion;
    Animator aguja;
    public Text puntos;
    M8A34ControlTotal controlTotal;
    public Animator carretera;
    bool mover = true;
    public bool activar, activar2;
    float acelerar;
    public Button salir;    
    void Start()
    {
        controlPuntaje = FindObjectOfType<ControlPuntaje>();
        controlNavegacion = FindObjectOfType<ControlNavegacion>();
        controlTotal = FindObjectOfType<M8A34ControlTotal>();
        jugador = FindObjectOfType<M8A34Jugador>().gameObject;

        jugador.transform.position = new Vector3(0, jugador.transform.position.y, jugador.transform.position.z);
        jugador.GetComponent<M8A34Jugador>().cambio();      
        aguja = acelerometro.GetComponent<Animator>();
        pregunta.transform.SetAsLastSibling();
        salir.onClick.AddListener(resetAll);
        puntos.text = (controlTotal.puntos_2 * 20) + "/200";
    }

    // Update is called once per frame
    public void activadorJugador() => activar = true;
    void FixedUpdate()
    {       

        carretera.speed = acelerar*2;
        if (controlTotal.vidas == 0)
            StartCoroutine(tiempocambio());

        if (mover && activar)
        {
            puntos.text = (controlTotal.puntos_2 * 20) + "/200";
            if (Application.isMobilePlatform)
            {                
                acelerar = SimpleInput.GetAxis("Vertical");
            }
            else
                acelerar = Input.GetAxis("Vertical");

            if (acelerar > 0.1)
            {
                aguja.speed = acelerar;
                aguja.SetFloat("direccion", acelerar);                
            }
            else if (acelerar < 0.5f)
            {
                aguja.Play("M8A34AcelerometroAumento", -1, float.NegativeInfinity);
                aguja.SetFloat("direccion", -1f);
                aguja.speed = 0.8f;
                
            }
        }
        else
        {
            acelerar = 0;
            aguja.SetFloat("direccion", 0);
            aguja.Play("M8A34AcelerometroAumento", 1, float.PositiveInfinity);
        }
    }
    IEnumerator tiempocambio()
    {
        yield return new WaitForSeconds(2.1f);
        controlNavegacion.GoToLayout(17);
    }
    public void puntaje()
    {
        controlTotal.puntos_2++;
        puntos.text = (controlTotal.puntos_2 * 20) + "/200";
    }

    public void ActivarPregunta()
    {
        mover = false;
        pregunta.SetActive(true);
    }
    public void desactivar()
    {
        activar = false;       
    }
    public void resetAll()
    {
        carretera.Rebind();
        if (activar2)
            activar = false;
        if (jugador)
        {
            jugador.GetComponent<M8A34Jugador>().cambio();
            jugador.GetComponent<M8A34Jugador>().m = true;
            jugador.GetComponent<M8A34Jugador>().audioSource.time = 0; ;
        }

        if (controlPuntaje)
        {
            int puntosTemp = Convert.ToInt32(controlPuntaje._rightAnswers - controlTotal.puntos_2);

            if (puntosTemp >= 0) 
            {
                controlPuntaje._rightAnswers = 0;
                controlPuntaje.IncreaseScore(Convert.ToInt32(controlTotal.puntos_1)); 
            }            
        }
       
        if (controlTotal)
        {
            controlTotal.resetVidas();
            controlTotal.puntos_2 = 0;
            print(controlTotal.puntos_2);
        }

        puntos.text = (controlTotal.puntos_2 * 20) + "/200";

        portada.SetActive(true);
        pregunta.GetComponent<ManagerSeleccionarToggle>().ResetSeleccionarToggle();
        pregunta.gameObject.SetActive(false);
        mover = true;
    }
}
