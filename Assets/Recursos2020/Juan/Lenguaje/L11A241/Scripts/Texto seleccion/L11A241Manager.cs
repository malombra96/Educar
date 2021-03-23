using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L11A241Manager : MonoBehaviour
{
    [HideInInspector] public ControlAudio controlAudio;
    ControlNavegacion controlNavegacion;
    ControlPuntaje controlPuntaje;
    public Button validar;
    public List<L11A241SoyUnToggle> toggles;
    int rigth;
    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        controlNavegacion = FindObjectOfType<ControlNavegacion>();
        controlPuntaje = FindObjectOfType<ControlPuntaje>();
        foreach (var toggle in toggles)
        {
            if (toggle.correcto)
                rigth++;
        }

        validar.onClick.AddListener(calificar);
        validar.interactable = false;
    }
        
    public void seleccion()
    {
        int i = 0;        
        foreach (var toggle in toggles)
        {
            if (toggle.isOn)
                i++;
        }
        validar.interactable = (i > 0);
    }
    void calificar()
    {
        int i = 0;
        validar.interactable = false;
        foreach (var toggle in toggles)
        {
            toggle.interactuable = false;
            if (toggle.isOn)
            {
                if (toggle.correcto)
                    i++;
                else
                    i--;

                toggle.estadoCalificar();
            }                           
        }
        if (i == rigth)
            controlPuntaje.IncreaseScore();

        controlAudio.PlayAudio(i == rigth ? 1 : 2);
        controlNavegacion.Forward(2);
    }
    public void resetAll()
    {
        validar.interactable = false;
        foreach (var toggle in toggles)
        {
            toggle.isOn = false;            
            toggle.estadoDefault();
            toggle.interactuable = true;
        }
    }
}
