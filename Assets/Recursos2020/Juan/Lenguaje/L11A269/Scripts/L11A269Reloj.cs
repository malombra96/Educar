using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L11A269Reloj : MonoBehaviour
{    
    ControlNavegacion _controlNavegacion;
    ControlAudio _controlAudio;
    public GameObject reloj;

    int minitos = 1;
    int segundos = 30;
    public bool correrReloj;
    public Button validar;
    // Start is called before the first frame update
    void Start()
    {
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
        _controlAudio = FindObjectOfType<ControlAudio>();        
                
        validar.onClick.AddListener(delegate { desactivarReloj(); });
    }
    void OnEnable()
    {       
        StartCoroutine(cronometro());
    }    
    public void desactivarReloj() => correrReloj = false;
    IEnumerator cronometro()
    {
        yield return new WaitForSeconds(1);

        if (segundos != 0)
            segundos--;
        else if (minitos == 1)
        {
            minitos = 0;
            segundos = 59;
        }
        else
        {
            _controlAudio.PlayAudio(2);
            _controlNavegacion.Forward();
            correrReloj = false;
        }
        if (correrReloj)
        {
            if (minitos != 0)
            {
                if (segundos >= 10)
                    reloj.transform.GetChild(0).GetComponent<Text>().text = Convert.ToString(minitos + ":" + segundos);
                else
                    reloj.transform.GetChild(0).GetComponent<Text>().text = Convert.ToString(minitos + ":0" + segundos);
            }
            else
            {
                if (segundos >= 10)
                    reloj.transform.GetChild(0).GetComponent<Text>().text = Convert.ToString(":" + segundos);
                else
                    reloj.transform.GetChild(0).GetComponent<Text>().text = Convert.ToString(":0" + segundos);
            }
            StartCoroutine(cronometro());
        }
    }
    public void resetReloj()
    {
        correrReloj = false;
        minitos = 1;
        segundos = 30;
        reloj.transform.GetChild(0).GetComponent<Text>().text = Convert.ToString(minitos + ":" + 30);
        correrReloj = true;
    }
}
