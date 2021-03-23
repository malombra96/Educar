using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L9A282ControlCaja : MonoBehaviour
{
    ControlAudio controlAudio;
    ControlNavegacion controlNavegacion;
    ControlPuntaje controlPuntaje;
    public GameObject informacion;

    public List<L9A282ControlCaja> Controls;
    public bool ultimo;
    public bool seleccion3D;
    public Image pregunta;
    public Button validar, clave;    
    public GameObject caja;
    public GameObject escenario3D;
    public RectTransform cronometro;
    public List<Toggle> toggles;

    public Color32 correcto;
    public Color32 incorrecto;

    int wrong = 0;
    int right = 0;
    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        controlNavegacion = FindObjectOfType<ControlNavegacion>();
        controlPuntaje = FindObjectOfType<ControlPuntaje>();

        if (!seleccion3D)
        {
            clave.onClick.AddListener(actividad);
            escenario3D.SetActive(false);
        }

        validar.onClick.AddListener(delegate { calificar(); });

        foreach (var toggle in toggles)
            toggle.onValueChanged.AddListener(delegate { seleccionado(); });

        validar.interactable = false;
        if (informacion)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            informacion.SetActive(true);
        }
    }
    private void OnEnable()
    {
        if (seleccion3D)
            escenario3D.SetActive(true);
        else
            escenario3D.SetActive(false);
    }
    private void OnDisable()
    {
        if (seleccion3D)
            escenario3D.SetActive(false);
    }
    public void revision()
    {
        if (informacion)
            informacion.SetActive(false);
        if (caja)
            caja.GetComponent<Animator>().enabled = false;

        if (seleccion3D)
            escenario3D.SetActive(false);
    }
    void actividad()
    {
        controlAudio.PlayAudio(0);
        caja.SetActive(true);
    }
    void seleccionado()
    {
        int i = 0;
        controlAudio.PlayAudio(0);
        foreach (var toggle in toggles)
        {
            if (toggle.isOn)
            {
                i++;
                toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._selection;
                if (seleccion3D)
                    toggle.transform.GetChild(0).GetComponent<Image>().sprite = toggle.transform.GetChild(0).GetComponent<BehaviourSprite>()._selection;
            }
            else
            {
                toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._default;
                if (seleccion3D)
                    toggle.transform.GetChild(0).GetComponent<Image>().sprite = toggle.transform.GetChild(0).GetComponent<BehaviourSprite>()._default;
            }
        }
        if(!seleccion3D)
            validar.interactable = (i >= 4);        
        else
            validar.interactable = (i == 1);
    }    
    void calificar()
    {
        validar.interactable = false;        

        foreach (var toggle in toggles)
        {
            toggle.interactable = false;
            if (toggle.isOn)
            {
                if (toggle.GetComponent<L9A282Toggles>().correcto)                
                    right++;               
                else
                    wrong++;

                toggle.GetComponent<Image>().sprite = 
                    toggle.GetComponent<L9A282Toggles>().correcto ? 
                    toggle.GetComponent<BehaviourSprite>()._right : 
                    toggle.GetComponent<BehaviourSprite>()._wrong;

                if (seleccion3D)
                {
                    pregunta.color = toggle.GetComponent<L9A282Toggles>().correcto ? correcto : incorrecto;
                    foreach (var control in Controls)
                    {
                        for (int i = 0; i < control.pregunta.transform.parent.childCount; i++)
                        {
                            if (i == pregunta.transform.GetSiblingIndex())
                                control.pregunta.transform.parent.GetChild(i).GetComponent<Image>().color = pregunta.color;
                        }
                    }
                }
                if(caja)
                    caja.GetComponent<Animator>().enabled = false;

            }
        }
        
        controlAudio.PlayAudio(wrong == 0 ? 1 : 2);
        if (wrong == 0)
            controlPuntaje.IncreaseScore();
        if(!ultimo)
            controlNavegacion.Forward(2);
        else
        {
            if (controlPuntaje.questions != controlPuntaje._rightAnswers)
                controlNavegacion.GoToLayout(9, 2);
            else
                controlNavegacion.Forward(2);
        }
    }
    public void Closed() => caja.SetActive(false);
    public void resetAll()
    {
        validar.interactable = false;
        right = 0;
        wrong = 0;
        if (caja)
        {
            caja.GetComponent<Animator>().enabled = true;
            caja.SetActive(false);
        }

        validar.interactable = false;
        if (seleccion3D)
        {
            for (int i = 0; i < pregunta.transform.parent.childCount; i++)            
                pregunta.transform.parent.GetChild(i).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            
            escenario3D.SetActive(false);
        }
        foreach (var toggle in toggles)
        {
            toggle.isOn = false;
            toggle.interactable = true;            
            toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._default;

            if (seleccion3D)
                toggle.transform.GetChild(0).GetComponent<Image>().sprite = toggle.transform.GetChild(0).GetComponent<BehaviourSprite>()._default;
        }
        if (informacion)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            informacion.SetActive(true);
        }
    }
}
