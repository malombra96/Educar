using System;
using UnityEngine;
using UnityEngine.UI;


public class Behaviour_Character : MonoBehaviour
{
    [HideInInspector] public ControlAudio _controlAudio;

    [HideInInspector] public Manager_WordSearch _manager;

    [HideInInspector] public int grupo, coordenadaX, coordenadaY;

    [HideInInspector] public bool pintado;

    [HideInInspector] public Char letra;

    [HideInInspector] public bool tieneRandom;

    [HideInInspector]  public bool calificado;

    [HideInInspector] public Color32 fondoSeleccionar,seleccionarLetra;
    [HideInInspector] public Color32 fondoDefault, defaultLetra;
    [HideInInspector] public Color32 fondoVerdadero,verdaderoLetra;

    
    private void Awake()
    {   
        // genera random de characteres 
        if (tieneRandom)
        {
            letra = (char)('A'+ UnityEngine.Random.Range(0, 26));
            transform.GetChild(0).GetComponent<Text>().text = letra.ToString();
            grupo = 100;
        }
        else
        {
            transform.GetChild(0).GetComponent<Text>().text = char.ToUpper(letra).ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Toggle>().onValueChanged.AddListener(delegate {
            CambiarEstado(GetComponent<Toggle>());
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (calificado)
        {
            gameObject.GetComponent<Toggle>().interactable = false;   
            gameObject.GetComponent<Image>().color = fondoVerdadero;
            transform.GetChild(0).GetComponent<Text>().color = verdaderoLetra;
        }
       
    }

    public void CambiarEstado(Toggle boton)
    {
        _controlAudio.PlayAudio(0);
        if (boton.isOn)
        {
            gameObject.GetComponent<Image>().color = fondoSeleccionar;
            transform.GetChild(0).GetComponent<Text>().color = seleccionarLetra;
            _manager.AñadirALista(boton.gameObject);
        }
        else
        {
            
            gameObject.GetComponent<Image>().color = fondoDefault;
            transform.GetChild(0).GetComponent<Text>().color = defaultLetra;
            _manager.EliminarRegistro();
        }
    }
}
