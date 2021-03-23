using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L10A194SeleccionRespuesta : MonoBehaviour
{
    ControlAudio controlAudio;
    ControlNavegacion controlNavegacion;
    ControlPuntaje controlPuntaje;

    public List<L10A194SeleccionRespuesta> seleccionRespuestas;
    public List<Toggle> opciones;
    public int actividad;
    public List<Image> claves;
    public List<Sprite> claveDefault;
    public List<Sprite> claveSeleccion;
    public Button validar;
    public L10A194fin fin;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        controlNavegacion = FindObjectOfType<ControlNavegacion>();
        controlPuntaje = FindObjectOfType<ControlPuntaje>();

        foreach (var opcion in opciones)
            opcion.onValueChanged.AddListener(delegate { Seleccionar(); });

        validar.onClick.AddListener(calificar);
        validar.interactable = false;
    }
    
    void Seleccionar()
    {
        controlAudio.PlayAudio(0);
        int seleccionados = 0;
        foreach (var opcion in opciones)
        {
            opcion.GetComponent<Image>().sprite = opcion.isOn ?
                opcion.GetComponent<BehaviourSprite>()._selection :
                opcion.GetComponent<BehaviourSprite>()._default;

            if (opcion.isOn)
                seleccionados++;
        }

        validar.interactable = (seleccionados > 0);
    }
    void calificar()
    {
        controlAudio.PlayAudio(0);
        validar.interactable = false;        

        foreach (var opcion in opciones)
        {
            opcion.interactable = false;
            if (opcion.isOn)
            {
                index = opcion.transform.GetSiblingIndex();

                opcion.GetComponent<Image>().sprite = 
                    opcion.GetComponent<L10A194Opcion>().correcto ? 
                    opcion.GetComponent<BehaviourSprite>()._right : 
                    opcion.GetComponent<BehaviourSprite>()._wrong;                

                controlAudio.PlayAudio(opcion.GetComponent<L10A194Opcion>().correcto ? 1 : 2);

                if (opcion.GetComponent<L10A194Opcion>().correcto)
                    controlPuntaje.IncreaseScore();
            }
        }

        claves[actividad].sprite = claveSeleccion[index];
        fin.claves[actividad].sprite = claveSeleccion[index];

        foreach (var seleccionRespuesta in seleccionRespuestas)
                seleccionRespuesta.claves[actividad].sprite = seleccionRespuesta.claveSeleccion[index];

        controlNavegacion.Forward(2);
    }
    public void ResetAll()
    {
        index = 0;
        validar.interactable = false;
        
        foreach (var opcion in opciones)
        {
            opcion.interactable = true;
            opcion.GetComponent<Image>().sprite = opcion.GetComponent<BehaviourSprite>()._default;
        }

        for (int x = 0; x < claves.Count; x++)
        {
            claves[x].sprite = claveDefault[x];
            fin.claves[x].sprite = claveDefault[x];
        }      
    }
}
