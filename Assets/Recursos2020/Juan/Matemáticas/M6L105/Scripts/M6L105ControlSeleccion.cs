using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M6L105ControlSeleccion : MonoBehaviour
{
    ControlAudio controlAudio;
    ControlPuntaje controlPuntaje;
    ControlNavegacion controlNavegacion;

    public GameObject toggles;
    public Button validar;
    public List<float> tiempo;
    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        controlNavegacion = FindObjectOfType<ControlNavegacion>();
        controlPuntaje = FindObjectOfType<ControlPuntaje>();

        for (int x = 0; x < toggles.transform.childCount; x++)
        {
            var toggle = toggles.transform.GetChild(x).GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(delegate { Seleccion(); });
        }
        validar.onClick.AddListener(delegate { StartCoroutine(Calificar()); });
        validar.interactable = false;
    }

    // Update is called once per frame
    public void Seleccion()
    {
        int activar = 0;
        controlAudio.PlayAudio(0);

        for (int x = 0; x < toggles.transform.childCount; x++)
        {
            var toggle = toggles.transform.GetChild(x).GetComponent<Toggle>();           
            if (toggle.isOn)
            {                
                activar++;
                toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._selection;                
            }
            else
                toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._default;
        }
        validar.interactable = (activar == 1 ? true : false);
    }
    IEnumerator Calificar()
    {
        controlAudio.PlayAudio(0);
        validar.interactable = false;
        for (int x = 0; x < toggles.transform.childCount; x++)
        {
            var toggle = toggles.transform.GetChild(x).GetComponent<Toggle>();
            toggle.interactable = false;
        }        

        for (int x = 0; x < toggles.transform.childCount; x++)
        {
            var toggle = toggles.transform.GetChild(x).GetComponent<Toggle>();            
            if (toggle.isOn)
            {                                
                if (toggle.GetComponent<M6L105Respuesta>().Correcta)
                {
                    GetComponent<Animator>().Play("Posicion " + Convert.ToString(x));
                    controlPuntaje.IncreaseScore(1);

                    yield return new WaitForSeconds(tiempo[x]);
                    toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._right;
                    GetComponent<Animator>().speed = 0;
                    GetComponent<Animator>().enabled = false;
                    controlAudio.PlayAudio(1);
                    controlNavegacion.Forward(2);
                }
                else
                {
                    controlAudio.PlayAudio(2);
                    controlNavegacion.Forward(2);
                    toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._wrong;
                }
            }            
        }
    }    

    public void resetAll()
    {
        GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().speed = 1;
        GetComponent<Animator>().Play("Polloquieto");

        for (int x = 0; x < toggles.transform.childCount; x++)
        {
            var toggle = toggles.transform.GetChild(x).GetComponent<Toggle>();
            toggle.interactable = true;
            toggle.isOn = false;
            toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._default;
        }
    }
}
