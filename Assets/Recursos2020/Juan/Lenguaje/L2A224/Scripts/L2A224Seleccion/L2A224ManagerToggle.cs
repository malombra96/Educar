using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L2A224ManagerToggle : MonoBehaviour
{
    public L2A224SeleccionActividad volver;
    public Toggle[] toggles;
    public Button validar;
    ControlAudio controlAudio;    
    ControlPuntaje controlPuntaje;

    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        controlPuntaje = FindObjectOfType<ControlPuntaje>();
        foreach (var toggle in toggles)
            toggle.onValueChanged.AddListener(delegate { Seleccionado(); });

        validar.onClick.AddListener(calificar);
        validar.interactable = false;
    }

    // Update is called once per frame
    void Seleccionado()
    {
        int i = 0;
        foreach (var toggle in toggles)
        {
            if (toggle.isOn)
            {
                controlAudio.PlayAudio(0);
                toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._selection;
                i++;
            }
            else
                toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._default;
        }
        validar.interactable = (i > 0) ? true : false;
    }
    void calificar()
    {
        controlAudio.PlayAudio(0);
        validar.interactable = false;
        int x = 0;
        foreach (var toggle in toggles)
        {
            toggle.interactable = false;
            if (toggle.isOn)
            {
                if (toggle.GetComponent<L22A224Toggle>().correcto)
                    toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._right;
                else
                {
                    toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._wrong;
                    x++;
                }
            }                      
        }
        controlAudio.PlayAudio(x == 0 ? 1 : 2);
        controlPuntaje.IncreaseScore(x == 0 ? 1 : 0);
        volver.VolverAlMenu();
    }
    public void ResetAll()
    {
        validar.interactable = false;
        foreach (var toggle in toggles)
        {
           toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._default;
           toggle.isOn = false;
            toggle.interactable = true;
        }
    }
}
