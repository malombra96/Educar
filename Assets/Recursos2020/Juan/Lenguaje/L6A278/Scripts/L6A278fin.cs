using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L6A278fin : MonoBehaviour
{
    ControlAudio controlAudio;
    ControlPuntaje controlPuntaje;
    ControlNavegacion controlNavegacion;

    public GameObject personaje;
    public Button finalizar;
    private void Awake()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        controlPuntaje = FindObjectOfType<ControlPuntaje>();
        controlNavegacion = FindObjectOfType<ControlNavegacion>();

        finalizar.onClick.AddListener(next);
    }
    private void OnEnable()
    {
        if (controlPuntaje.questions == controlPuntaje._rightAnswers)
        {
            personaje.GetComponent<Image>().sprite = personaje.GetComponent<BehaviourSprite>()._right;
            personaje.transform.GetChild(0).GetComponent<Image>().sprite = personaje.transform.GetChild(0).GetComponent<BehaviourSprite>()._right;
        }
        else
        {
            personaje.GetComponent<Image>().sprite = personaje.GetComponent<BehaviourSprite>()._wrong;
            personaje.transform.GetChild(0).GetComponent<Image>().sprite = personaje.transform.GetChild(0).GetComponent<BehaviourSprite>()._wrong;
        }
        
    }
    void next()
    {
        controlAudio.PlayAudio(0);
        controlNavegacion.Forward();
    }
}
