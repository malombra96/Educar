using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class L9A279Manager : MonoBehaviour,IPointerClickHandler
{
    [HideInInspector] public ControlAudio controlAudio;
    ControlPuntaje controlPuntaje;
    ControlNavegacion controlNavegacion;

    /*[HideInInspector]*/ public List<GameObject> opciones;
    public GameObject barraVida;
    public Button validar;
    public L9A279Mira mira;
    [HideInInspector] public int puntoVida;
    GameObject padre;
    // Start is called before the first frame update
    void Start()
    {        
        controlAudio = FindObjectOfType<ControlAudio>();
        controlPuntaje = FindObjectOfType<ControlPuntaje>();
        controlNavegacion = FindObjectOfType<ControlNavegacion>();
        //padre = transform.GetComponentInParent<GameObject>();
        validar.onClick.AddListener(califivar);
        validar.interactable = false;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        mira.mover = true;
        validar.interactable = false;
        mira.GetComponent<Image>().sprite = mira.GetComponent<BehaviourSprite>()._default;
        
        foreach (var opcion in opciones)
        {
            opcion.GetComponent<Image>().raycastTarget = true;
            opcion.GetComponent<L9A279opcion>().seleccionado = false;
        }       
    }
    private void OnEnable()
    {
        padre = transform.GetComponentInParent<Canvas>().gameObject;
        if (gameObject.activeSelf)
        {
            for (int i = 0; i < barraVida.transform.childCount; i++)
                barraVida.transform.GetChild(i).gameObject.SetActive(false);

            if (padre.transform.GetChild(transform.GetSiblingIndex() - 1).GetComponent<L9A279Manager>())
                puntoVida = padre.transform.GetChild(transform.GetSiblingIndex() - 1).GetComponent<L9A279Manager>().puntoVida;
            else
                puntoVida = 10;

            for (int i = 0; i < puntoVida; i++)
                barraVida.transform.GetChild(i).gameObject.SetActive(true);
        }

    }
    public void activarBoton()
    {
        int x = 0;
        foreach (var opcion in opciones)
        {
            if (opcion.GetComponent<L9A279opcion>().seleccionado)
                x++;
        }

        validar.interactable = (x == 1 ? true : false);
    }
    void califivar()
    {
        controlAudio.PlayAudio(0);
        int x = 0;
        foreach (var opcion in opciones)
        {
            opcion.GetComponent<Image>().raycastTarget = false;
            if (opcion.GetComponent<L9A279opcion>().seleccionado)
            {
                if (opcion.GetComponent<L9A279opcion>().correcto)
                {
                    x++;
                    opcion.GetComponent<Animator>().Play("Explocion Correcta");
                    opcion.transform.GetChild(2).GetComponent<Image>().sprite = opcion.transform.GetChild(2).GetComponent<BehaviourSprite>()._right;
                }
                else
                {
                    opcion.GetComponent<Animator>().Play("Explocion Incorrecta");
                    opcion.transform.GetChild(2).GetComponent<Image>().sprite = opcion.transform.GetChild(2).GetComponent<BehaviourSprite>()._wrong;
                    for (int i = 0; i < 3; i++)
                    {
                        puntoVida--;                        
                        if (puntoVida > 0)
                            barraVida.transform.GetChild(puntoVida).gameObject.SetActive(false);
                    }
                }

            }
        }
        controlPuntaje.IncreaseScore(x);
        controlAudio.PlayAudio(x == 1 ? 1 : 2);
        if (puntoVida < 0)
            controlNavegacion.GoToLayout(13);
        else
            controlNavegacion.Forward(2);
    }
    public void resetAll()
    {
        puntoVida = 10;
        foreach (var opcion in opciones)
        {
            opcion.GetComponent<Image>().raycastTarget = true;
            opcion.GetComponent<L9A279opcion>().seleccionado = false;
            opcion.transform.GetChild(2).GetComponent<Image>().sprite = opcion.transform.GetChild(2).GetComponent<BehaviourSprite>()._default;
        }
        mira.GetComponent<Image>().sprite = mira.GetComponent<BehaviourSprite>()._default;
        if (gameObject.name == "Actividad_2 (1)")
        {
            mira.mover = true;
            transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
        }
        else
        {
            mira.mover = true;
            if (transform.GetChild(transform.childCount - 1).gameObject.name == "Instruccion")
                transform.GetChild(transform.childCount - 1).gameObject.SetActive(false);
        }
    }   
}
