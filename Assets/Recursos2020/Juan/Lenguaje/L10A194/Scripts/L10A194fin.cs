using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L10A194fin : MonoBehaviour
{   
    ControlAudio controlAudio;
    ControlNavegacion controlNavegacion;
    ControlPuntaje controlPuntaje;

    public List<Image> claves;
    public Image cofre;
    public Image texto;
    public Button finalizar;

    // Start is called before the first frame update
    void Awake()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        controlNavegacion = FindObjectOfType<ControlNavegacion>();
        controlPuntaje = FindObjectOfType<ControlPuntaje>();

        finalizar.onClick.AddListener(next);
    }

    private void OnEnable()
    {
        print(controlPuntaje);
        if (controlPuntaje._rightAnswers == controlPuntaje.questions) 
        {           
            cofre.sprite = cofre.GetComponent<BehaviourSprite>()._right;
            texto.sprite = texto.GetComponent<BehaviourSprite>()._right;            
        }
        else
        {
            cofre.sprite = cofre.GetComponent<BehaviourSprite>()._wrong;
            texto.sprite = texto.GetComponent<BehaviourSprite>()._wrong;
        }
    }
    void next()
    {
        controlAudio.PlayAudio(0);
        controlNavegacion.Forward();
    }
}
