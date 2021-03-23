using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8L40_Lifes : MonoBehaviour
{
    [Header("Lifes :")] public List<GameObject> lifes;
    
    [Header("a donde va ?:")] public int go ;
    public int contador = 2;
    public GameObject navbar;

    public enum tipo
    {
        normal,
        ultimo
    }

    [Header("Tipo de aplico :")] public tipo aplico;
    
    public ControlNavegacion navegacion;

    private void Awake()
    {

    }

    void Start()
    {
        foreach (var life in lifes)
            life.GetComponent<Image>().sprite = life.GetComponent<BehaviourSprite>()._selection;
    }

    public void Refresh()
    {
        contador--;
        for (int i = 0; i < lifes.Count; i++)
        {
            if (contador > i)
            {
                lifes[i].GetComponent<Image>().sprite = lifes[i].GetComponent<BehaviourSprite>()._selection;
            }
            else
            {
                lifes[i].GetComponent<Image>().sprite = lifes[i].GetComponent<BehaviourSprite>()._default;
            }
        }

        if (contador == 0)
        {
            navbar.SetActive(false);
            switch (aplico)
            {
                case tipo.normal:
                    StartCoroutine(time_next(4));
                    break;
                case tipo.ultimo:
                    StartCoroutine(x(4));
                    break;
            }
        }
            
        
        
        
    }

    IEnumerator time_next(float time)
    {
        yield return new WaitForSeconds(time);
        navegacion.Forward();
    }
    IEnumerator x(float time)
    {
        yield return new WaitForSeconds(time);
        navegacion.GoToLayout(go);
    }

}
