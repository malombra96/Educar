using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boton_calculadora : MonoBehaviour
{
    Button boton;
    string textoBoton;
    manager_calculadora _manager;
    ControlAudio controlAudio;

    public enum TipoBoton
    {
        funcion,
        numero,
        operacion
    }

    [Header("Seleccion que tipo de boton es:")] public TipoBoton _tipo;

    // Start is called before the first frame update
    void Start()
    {
        _manager = FindObjectOfType<manager_calculadora>();
        boton = GetComponent<Button>();
        boton.onClick.AddListener(delegate { ClickBoton(boton.name); });
        controlAudio = GameObject.FindObjectOfType<ControlAudio>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickBoton(string texto)
    {
        controlAudio.PlayAudio(0);
        switch (_tipo)
        {
            case TipoBoton.funcion:
                _manager.AddFunction(texto);
                break;
            case TipoBoton.numero:
                _manager.AddText(texto);
                break;
            case TipoBoton.operacion:
                _manager.AddOperation(texto);
                break;
        }
    }
}
