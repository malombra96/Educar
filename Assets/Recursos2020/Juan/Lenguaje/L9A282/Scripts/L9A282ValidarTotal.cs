using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L9A282ValidarTotal : MonoBehaviour
{
    ControlPuntaje controlPuntaje;
    ControlAudio controlAudio;
    ControlNavegacion controlNavegacion;
    public GameObject carcel;
    public Button finalizar;
    // Start is called before the first frame update
    void Awake()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        controlPuntaje = FindObjectOfType<ControlPuntaje>();
        controlNavegacion = FindObjectOfType<ControlNavegacion>();
        finalizar.onClick.AddListener(next);
    }

    // Update is called once per frame
    private void OnEnable()
    {
        if (controlPuntaje.questions != controlPuntaje._rightAnswers)
        {
            controlNavegacion.Forward();
            carcel.SetActive(false);            
        }
        else
            carcel.SetActive(true);
    }    
    void next()
    {
        controlAudio.PlayAudio(0);
        controlNavegacion.Forward();
    }
}
