using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L6A278Pesonajes : MonoBehaviour
{
    ControlAudio controlAudio;    
    ControlNavegacion controlNavegacion;

    public List<Toggle> toggles;
    public List<L6A278ManagerSeleccion> aplicos;
    public L5A258ManagerTextoPrint managerTextoPrint;
    public L6A278fin fin;
    public Button inicio;
    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        controlNavegacion = FindObjectOfType<ControlNavegacion>();

        foreach (var x in toggles)
            x.onValueChanged.AddListener(delegate { seleccionPersonaje(); });

        inicio.onClick.AddListener(iniciar);
        inicio.interactable = false;
    }
    void seleccionPersonaje()
    {
        controlAudio.PlayAudio(0);
        int i = 0;
        foreach(var x in toggles)
        {
            if (x.isOn)
            {
                i++;
                x.GetComponent<Image>().sprite = x.GetComponent<BehaviourSprite>()._selection;
            }
            else
                x.GetComponent<Image>().sprite = x.GetComponent<BehaviourSprite>()._default;
        }
        inicio.interactable = (i > 0);
    }
    void iniciar()
    {
        controlAudio.PlayAudio(0);
        inicio.interactable = false;

        foreach (var x in toggles)
        {
            if (x.isOn)
            {
                foreach (var aplico in aplicos)
                {
                    aplico.personaje.GetComponent<Image>().sprite = x.GetComponent<BehaviourSprite>()._wrong;
                    aplico.personaje.GetComponent<BehaviourSprite>()._right = x.GetComponent<BehaviourSprite>()._right;
                    aplico.personaje.GetComponent<BehaviourSprite>()._wrong = x.GetComponent<BehaviourSprite>()._wrong;                    
                }
                managerTextoPrint.personaje.GetComponent<Image>().sprite = x.GetComponent<BehaviourSprite>()._right;

                fin.personaje.GetComponent<Image>().sprite = x.GetComponent<BehaviourSprite>()._wrong;
                fin.personaje.GetComponent<BehaviourSprite>()._right = x.GetComponent<BehaviourSprite>()._right;
                fin.personaje.GetComponent<BehaviourSprite>()._wrong = x.GetComponent<BehaviourSprite>()._wrong;
            }
        }
        
        controlNavegacion.Forward();
    }
    public void ResetAll()
    {
        inicio.interactable = false;
        foreach (var x in toggles)
        {
            x.isOn = false;
            x.GetComponent<Image>().sprite = x.GetComponent<BehaviourSprite>()._default;
        }
    }
}
