using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L2A225Seleccionpersonaje : MonoBehaviour
{    
    public Button inicio;
    public GameObject toggles;
    ControlNavegacion controlNavegacion;
    ControlAudio controlAudio;
    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        controlNavegacion = FindObjectOfType<ControlNavegacion>();

        for (int x = 0; x < toggles.transform.childCount; x++)
            toggles.transform.GetChild(x).GetComponent<Toggle>().onValueChanged.AddListener(delegate { seleccion(); });
        inicio.onClick.AddListener(next);
        inicio.interactable = false;
    }

    // Update is called once per frame
    void seleccion()
    {
        int i = 0;
        controlAudio.PlayAudio(0);
        for (int x = 0; x < toggles.transform.childCount; x++)
        {
            if (toggles.transform.GetChild(x).GetComponent<Toggle>().isOn)
            {                
                toggles.transform.GetChild(x).GetComponent<Image>().sprite = toggles.transform.GetChild(x).GetComponent<BehaviourSprite>()._selection;
                i++;
            }
            else
                toggles.transform.GetChild(x).GetComponent<Image>().sprite = toggles.transform.GetChild(x).GetComponent<BehaviourSprite>()._default;
        }
        inicio.interactable = (i > 0 ? true : false);
    }
    void next()
    {
        controlAudio.PlayAudio(0);
        inicio.interactable = false;
        controlNavegacion.Forward();
    }
    public void resetAll()
    {        
        for (int x = 0; x < toggles.transform.childCount; x++)
        {
            toggles.transform.GetChild(x).GetComponent<Toggle>().isOn = false;
            toggles.transform.GetChild(x).GetComponent<Image>().sprite = toggles.transform.GetChild(x).GetComponent<BehaviourSprite>()._default;
        }
    }
}
