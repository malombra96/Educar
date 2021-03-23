using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M1L70Opcion : MonoBehaviour
{
    public bool respuestaCorrecta;
    ControlAudio controlAudio;
    private void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        GetComponent<Toggle>().onValueChanged.AddListener(delegate { entrar(); });
    }

    private void Update()
    {
        if (GetComponent<Toggle>().isOn == false)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = transform.GetChild(0).GetComponent<BehaviourSprite>()._default;
        }      
    }

    void entrar()
    {
        controlAudio.PlayAudio(0);
        transform.GetChild(0).GetComponent<Image>().sprite = transform.GetChild(0).GetComponent<BehaviourSprite>()._selection;
    }

}
