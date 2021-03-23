using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M4A114ControlSeleccionYTexto : MonoBehaviour
{
    [HideInInspector] public ControlAudio controlAudio;
    ControlPuntaje controlPuntaje;
    ControlNavegacion controlNavegacion;

    [Header("Mini juego camion")]
    public GameObject camion;
    public GameObject instruccionPc;
    public GameObject instruccionMovil;
    public bool necesitaInstrccion;

    [Header("Aplico")]
    public GameObject actividad;
    public GameObject Teclado;
    public List<Toggle> toggles;
    public Button validar;
    public bool animacion;
    public List<InputField> inputFields;
    [HideInInspector] public List<M4A114ObjetoMovible> drags;
    public List<M4A114Drop> drops;
    bool activar;
    bool revisar;

    [Header("colores de input")]
    public Color32 colorDefault;
    public Color32 colorCorrecto;
    public Color32 colorIncorrecto;

    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        controlNavegacion = FindObjectOfType<ControlNavegacion>();
        controlPuntaje = FindObjectOfType<ControlPuntaje>();
        activar = true;
        //actividad.SetActive(false);
        
        foreach (var toggle in toggles)
        {
            toggle.interactable = false;
            toggle.onValueChanged.AddListener(delegate { activarValidar(); });
        }
        foreach (var input in inputFields)
        {
            input.GetComponent<Image>().raycastTarget = false;
            input.interactable = false;
            input.textComponent.color = colorDefault;
            input.GetComponent<M4A114OpcionesInput>().ControlSeleccionYTexto = this;
        }

        validar.onClick.AddListener(calificar);
        validar.interactable = false;
    }

    private void OnEnable()
    {       
        if (necesitaInstrccion)
        {
            camion.GetComponent<M4A114Camion>().mover = false;
            if (Application.isMobilePlatform)
            {
                instruccionMovil.SetActive(true);
                instruccionPc.SetActive(false);
            }
            else
            {
                instruccionPc.SetActive(true);
                instruccionMovil.SetActive(false);
            }            
        }

        if (revisar)
        {
            if (necesitaInstrccion)
            {
                instruccionPc.SetActive(false);
                instruccionMovil.SetActive(false);
            }
            actividad.SetActive(true);
        }
    }
    private void OnDisable()
    {
        if (animacion)
        {
            var temp = actividad.transform.GetChild(2).GetChild(actividad.transform.GetChild(2).childCount - 1);
            temp.GetComponent<RectTransform>().anchoredPosition = temp.GetComponent<M4A114ObjetoMovible>().posDefault;

            if (temp.GetComponent<M4A114ObjetoMovible>().individial)
            {
                actividad.transform.GetChild(2).GetChild(0).GetComponent<Animator>().enabled = false;
                actividad.transform.GetChild(2).GetChild(1).GetComponent<Animator>().enabled = false;
            }
            else
                actividad.transform.GetChild(2).GetComponent<Animator>().enabled = false;
        }        
    }

    public void activarOpciones(int i)
    {
        int x = 0;
        if (!animacion)
        {
            foreach(var drag in drags)
            {
                if (drag.indrop)
                    x++;
            }

            if (x == i)
            {
                foreach (var drag in drags)
                    drag.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }         
        
        foreach (var toggle in toggles)
            toggle.interactable = (x == i);

        foreach (var input in inputFields)
            input.interactable = (x == i);
    }
    public void activarOpciones()
    {
        foreach (var toggle in toggles)
            toggle.interactable = (true);

        foreach (var input in inputFields)
            input.interactable = (true);
    }
    public void apagarIntruccion()
    {
        controlAudio.PlayAudio(0);
        camion.GetComponent<M4A114Camion>().mover = true;

        if (Application.isMobilePlatform)        
            instruccionMovil.SetActive(false);        
        else        
            instruccionPc.SetActive(false);
    }    
    public IEnumerator activarActividad()
    {
        //controlAudio.PlayAudio(0);
        yield return new WaitForSeconds(0.5f);
        actividad.SetActive(true);
    }
    public void modoRevision()
    {
        revisar = true;
        camion.GetComponent<M4A114Camion>().mover = false;
    }
    public void activarValidar()
    {
        int x = 0;
        if(activar)
        foreach (var toggle in toggles) 
        {
            if (toggle.isOn)
            {
                x++;
                toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._selection;                
            }
            else
                toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._default;
        }
        foreach (var input in inputFields)
        {
            if (input.text != "")            
                x++;
        }
        validar.interactable = (x == 3);
    }    
    void calificar()
    {
        controlAudio.PlayAudio(0);
        validar.interactable = false;
        activar = false;

        int right = 0;
        foreach(var toggle in toggles)
        {
            toggle.interactable = false;
            toggle.GetComponent<Image>().raycastTarget = false;
            if (toggle.isOn)
            {
                if (toggle.GetComponent<M4A114Opciones>().esCorrecto)
                {
                    right++;
                    toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._right;
                }
                else
                    toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._wrong;                    
            }
        }

        for (int x = 0;x < inputFields.Count;x++)
        {
            var input = inputFields[x];
            input.interactable = false;

            if (input.GetComponent<M4A114OpcionesInput>().numeroCorrecto.Count > 1) 
            {
                for (int i = 0; i < input.GetComponent<M4A114OpcionesInput>().numeroCorrecto.Count; i++)
                {
                    var correcto = input.GetComponent<M4A114OpcionesInput>().numeroCorrecto[i];
                    if (input.text == correcto)
                    {
                        
                        input.textComponent.color = colorCorrecto;

                        if (inputFields[x + 1].GetComponent<InputField>().text == inputFields[x + 1].GetComponent<M4A114OpcionesInput>().numeroCorrecto[i])
                        {
                            right += 2;
                            inputFields[x + 1].GetComponent<InputField>().textComponent.color = colorCorrecto;
                        }
                        else
                            inputFields[x + 1].GetComponent<InputField>().textComponent.color = colorIncorrecto;

                        break;
                    }
                    else
                    {
                        input.textComponent.color = colorIncorrecto;                       
                        inputFields[x + 1].GetComponent<InputField>().textComponent.color = colorIncorrecto;
                    }
                }
                break;
            }
            else
            {
                if (input.text == input.GetComponent<M4A114OpcionesInput>().numeroCorrecto[0])
                {
                    right++;
                    input.textComponent.color = colorCorrecto;
                   
                }
                else
                    input.textComponent.color = colorIncorrecto;
            }            
        }

        if (right == 3)
            controlPuntaje.IncreaseScore();

        controlAudio.PlayAudio(right == 3 ? 1 : 2);
        controlNavegacion.Forward(2);
    }    
    public void resetAll()
    {
        validar.interactable = false;
        activar = true;
        
        if (camion.GetComponent<M4A114Camion>().defaulPos.x != 0 && camion.GetComponent<M4A114Camion>().defaulPos.y != 0)
            camion.GetComponent<RectTransform>().anchoredPosition = camion.GetComponent<M4A114Camion>().defaulPos;   

        if (necesitaInstrccion)
        {
            camion.GetComponent<M4A114Camion>().mover = false;
            if (Application.isMobilePlatform)
            {
                instruccionMovil.SetActive(true);
                instruccionPc.SetActive(false);
            }
            else
            {
                instruccionPc.SetActive(true);
                instruccionMovil.SetActive(false);
            }
        }
        else
            camion.GetComponent<M4A114Camion>().mover = true;

        if (animacion)
        {            
            var temp = actividad.transform.GetChild(2).GetChild(actividad.transform.GetChild(2).childCount - 1);

            temp.GetComponent<M4A114ObjetoMovible>().x = 0;
            if (temp.GetComponent<M4A114ObjetoMovible>().posDefault.x != 0 && temp.GetComponent<M4A114ObjetoMovible>().posDefault.y != 0)
                temp.GetComponent<RectTransform>().anchoredPosition = temp.GetComponent<M4A114ObjetoMovible>().posDefault;

            if (temp.GetComponent<M4A114ObjetoMovible>().individial)
            {
                actividad.transform.GetChild(2).GetChild(0).GetComponent<Animator>().enabled = true;
                actividad.transform.GetChild(2).GetChild(0).GetComponent<Animator>().Rebind();
                
                actividad.transform.GetChild(2).GetChild(1).GetComponent<Animator>().enabled = true;
                actividad.transform.GetChild(2).GetChild(1).GetComponent<Animator>().Rebind();                
            }
            else
            {
                actividad.transform.GetChild(2).GetComponent<Animator>().enabled = true;
                actividad.transform.GetChild(2).GetComponent<Animator>().Rebind();                      
            }
            temp.GetComponent<Rigidbody2D>().simulated = true;
        }
        else
        {
            foreach (var drag in drags)
            {
                drag.indrop = false;
                drag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                drag.GetComponent<RectTransform>().anchoredPosition = drag.posDefault;
            }
            foreach (var drop in drops)
                drop._drag = null;
        }

        foreach (var toggle in toggles)
        {
            toggle.isOn = false;
            toggle.interactable = false;
            toggle.GetComponent<Image>().raycastTarget = true;
            toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._default;
        }
        foreach (var input in inputFields)
        {
            input.text = "";
            input.interactable = false;
            input.textComponent.color = colorDefault;            
        }        
        actividad.SetActive(false);
    }        
    
}
