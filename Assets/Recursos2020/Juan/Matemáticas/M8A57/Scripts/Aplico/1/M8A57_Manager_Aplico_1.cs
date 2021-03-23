using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8A57_Manager_Aplico_1 : MonoBehaviour
{
    [Header("Controles :")]
    public M8A57_Control control;  
    
    
    [Header("Validar :")] public Button validar;
    
    public ControlNavegacion navegacion;
   
    private void Start()
    {         
        validar.onClick.AddListener(Check);
    }    

    public void Check()
    {         
        control.contador++;
        print(control.contador);
        StartCoroutine(tiempo());
    }

    IEnumerator tiempo()
    {
        yield return new WaitForSeconds(2.1f);
        if (control.contador == 5) 
        { 
            navegacion.GoToLayout(8);
        }
        else
            navegacion.GoToLayout(2);
    }    
    
}
