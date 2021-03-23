using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M1L70ControlAplico1 : MonoBehaviour
{
    public Toggle[] opcion;
    public Button validar;
    public Animator cañon;

    ControlNavegacion controlNavegacion;
    ControlAudio controlAudio;
    ControlPuntaje controlPuntaje;
    int x = 0;
    // Start is called before the first frame update
    void Start()
    {
        validar.interactable = false;
        controlNavegacion = FindObjectOfType<ControlNavegacion>();
        controlAudio = FindObjectOfType<ControlAudio>();
        controlPuntaje = FindObjectOfType<ControlPuntaje>();
        validar.onClick.AddListener(calicar);
    }

    private void Update()
    {
        for(int i = 0; i < opcion.Length; i++)
        {
            if (opcion[i].isOn)
            {
                if (x == 0)
                {
                    Invoke("activarBoton", 1.30f);
                    x++;
                }
                
                if (i == 0)
                {
                    cañon.SetBool("Derecha", false);
                    cañon.SetBool("Izquierda", true);
                }
                else if (i == 1)
                {
                    cañon.SetBool("Derecha", true);
                    cañon.SetBool("Izquierda", false);
                }                
            }
        }
    }

    void calicar()
    {       
        validar.interactable = false;
        controlAudio.PlayAudio(0);
        cañon.speed = 0;
        for (int i = 0; i < opcion.Length; i++)
        {
            if (opcion[i].GetComponent<M1L70Opcion>().respuestaCorrecta && opcion[i].isOn)
            {
                opcion[i].transform.GetChild(0).GetComponent<Image>().sprite = opcion[i].transform.GetChild(0).GetComponent<BehaviourSprite>()._right;
                controlPuntaje.IncreaseScore(1);
                controlAudio.PlayAudio(1);                
            }
            else if(opcion[i].isOn)
            {
                opcion[i].transform.GetChild(0).GetComponent<Image>().sprite = opcion[i].transform.GetChild(0).GetComponent<BehaviourSprite>()._wrong;
                controlAudio.PlayAudio(2);
            }
        }
        controlNavegacion.Forward(2);
    }
    void activarBoton()
    {
        validar.interactable = true;       
    }
    public void resetAll()
    {
        cañon.speed = 1;
        cañon.SetBool("Derecha", false);
        cañon.SetBool("Izquierda", false);
        x = 0;
        for (int i = 0; i < opcion.Length ; i++)
        {
            opcion[i].isOn = false;
            opcion[i].transform.GetChild(0).GetComponent<Image>().sprite = opcion[i].transform.GetChild(0).GetComponent<BehaviourSprite>()._default;            
        }
    }
}
