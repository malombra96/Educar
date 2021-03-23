using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
public class M10A61_boton : MonoBehaviour
{
    // Start is called before the first frame update
    Button boton;
    string textoBoton;
    public M10A61_mangerCalculadora _manager;
    ControlAudio ControlAudio;

    public enum TipoBoton
    {
        funcion,
        numero,
        operacion
    }

    [Header("Seleccion que tipo de boton es:")] public TipoBoton _tipo;

    void Start()
    {
        ControlAudio = GameObject.FindObjectOfType<ControlAudio>();
        textoBoton = gameObject.name;
        boton = GetComponent<Button>();
        boton.onClick.AddListener(delegate { ClickBoton(textoBoton); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickBoton(string texto) {
        ControlAudio.PlayAudio(0);
        switch (_tipo) {
            case TipoBoton.funcion:
                _manager.AddFunction(texto);
                break;
            case TipoBoton.numero:
                _manager.AddNumber(texto);
                break;
            case TipoBoton.operacion:
                _manager.AddNumber(texto);
                break;
        }
    }
}
