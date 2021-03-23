using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8A57_Ruleta : MonoBehaviour
{
    public int contador;
    public bool ruleta;
    float t;
    [Header("Images :")] public List<GameObject> opciones;
    [HideInInspector] public List<GameObject> preguntas;
    [Header("boton :")] public Button boton;
    void Awake()
    {
        contador = 0;
        ruleta = true;
        foreach (var opcione in opciones)
            opcione.GetComponent<Image>().sprite = opcione.GetComponent<BehaviourSprite>()._default;

        boton.onClick.AddListener(change);               
    }

    private void OnEnable()
    {
        Repite();
    }
   
    void Repite()
    {      
        if (ruleta)
        {
            t = .8f;          
            StartCoroutine(tiempo());
        }
    }

    IEnumerator tiempo()
    {     
        yield return new WaitForSeconds(t);
        if (ruleta)
        {            
            aumetarContador();

            for (int i = 0; i < opciones.Count; i++)
            {         
                if(opciones[contador].GetComponent<M8A57_Go2>().preguntaContestada)
                {                    
                    i++;
                }
                if (i == contador && !opciones[i].GetComponent<M8A57_Go2>().preguntaContestada)
                {
                    opciones[i].GetComponent<Image>().sprite = opciones[i].GetComponent<BehaviourSprite>()._selection;
                }                
                else
                {
                    opciones[i].GetComponent<Image>().sprite = opciones[i].GetComponent<BehaviourSprite>()._default;
                }
            }
            Repite();
        }       
        
    }
    void aumetarContador()
    {
        contador++;
        if (contador >= opciones.Count)
        {
            contador = 0;
        }
        if (opciones[contador].GetComponent<M8A57_Go2>().preguntaContestada)
        {
            aumetarContador();            
        }       
        
    }
    void change()
    {
        ruleta = false;
        t = .1f;
        StopCoroutine(tiempo());        
        StartCoroutine(opciones[contador].GetComponent<M8A57_Go2>().tiempo(.8f));
    }
}
