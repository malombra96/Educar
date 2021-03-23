using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M8A34modificarEscala : MonoBehaviour
{
    public RectTransform re_escalador;
    ControlNavegacion controlNavegacion;
    void Start()
    {
        controlNavegacion = FindObjectOfType<ControlNavegacion>();
        InvokeRepeating("scala", 0.1f, 0.1f);
    }

  
    void scala()
    {        
        GetComponent<RectTransform>().localScale = controlNavegacion._Layouts[controlNavegacion.LayoutActual()].GetComponent<RectTransform>().localScale;
    }
}
