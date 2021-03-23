using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;
using Unity.VideoHelper;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class L5A260ManagerSeleccionTexto : MonoBehaviour
{
    [HideInInspector] public ControlAudio controlAudio;
    ControlNavegacion controlNavegacion;
    ControlPuntaje controlPuntaje;

    public List<GameObject> managers;
    public GameObject listaPreguntas;
    /*[HideInInspector]*/ public int pregunta;
    public List<Text> texts;
    public List<Toggle> toggles;
    public bool animacionEntrada;
    
    //Cronometro
    public GameObject _cronometro;
    bool tiempoCorre;
    [HideInInspector] public int minuto = 3;
    [HideInInspector] public int segundo;

    public Color32 _default;
    public Color32 _ove;
    public Color32 _seleccion;
    public Color32 _correcto;
    public Color32 _incorrecto;

    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        controlNavegacion = FindObjectOfType<ControlNavegacion>();
        controlPuntaje = FindObjectOfType<ControlPuntaje>();            
        foreach(var toggle in toggles)
            toggle.onValueChanged.AddListener(delegate { calificar(toggle.GetComponent<L5A260TextoSeleccionado>().correcto); });
    }
    void Update()
    {
        if (transform.GetChild(1).gameObject.activeSelf && !GetComponent<BehaviourLayout>()._isEvaluated)
        {
            GetComponent<BehaviourLayout>()._isEvaluated = true;
            tiempoCorre = true;            
            StartCoroutine(Cronometro());
        }        
    }
    private void OnEnable()
    {        
        for (int x = 0; x < pregunta; x++)
        {
            listaPreguntas.transform.GetChild(x).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            listaPreguntas.transform.GetChild(x).GetComponent<Image>().sprite =
                listaPreguntas.transform.GetChild(x).GetComponent<BehaviourSprite>()._right;
        }
        if (transform.GetChild(transform.childCount - 1).gameObject.name == "Portada")
        {
            animacionEntrada = true;
            GetComponent<Animator>().speed = 0;
        }
        else if (!animacionEntrada)
        {            
            GetComponent<Animator>().speed = 1;
            StartCoroutine(activarActividad());
        }
    }
    void OnDisable()
    {
        GetComponent<BehaviourLayout>()._isEvaluated = animacionEntrada;
        if (animacionEntrada)
            GetComponent<Animator>().enabled = false;
        tiempoCorre = false;
    }
    IEnumerator activarActividad()
    {       
        yield return new WaitForSeconds(2);
        animacionEntrada = true;        
        GetComponent<Animator>().speed = 0;        
    }
    public IEnumerator calificar(bool b)
    {
        controlAudio.PlayAudio(0);
        tiempoCorre = false;
        
        foreach (var text in texts)
            text.raycastTarget = false;

        foreach (var toggle in toggles)
        {
            toggle.interactable = false;
            toggle.GetComponent<Image>().raycastTarget = false;
            if (toggle.isOn)
                toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._selection;
        }

        yield return new WaitForSeconds(1f);
        if (texts.Count > 0)
        {
            foreach (var text in texts)
            {
                if (text.GetComponent<L5A260TextoSeleccionado>().isOn)
                {
                    text.color = (b ? _correcto : _incorrecto);
                    break;
                }
            }
        }
        else
        {            
            foreach (var toggle in toggles)
            {
                if (toggle.isOn)
                {                    
                    toggle.GetComponent<Image>().sprite = b ? 
                        toggle.GetComponent<BehaviourSprite>()._right : 
                        toggle.GetComponent<BehaviourSprite>()._wrong;
                    break;
                }
            }
        }
        
        listaPreguntas.transform.GetChild(pregunta).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        listaPreguntas.transform.GetChild(pregunta).GetComponent<Image>().sprite =
            listaPreguntas.transform.GetChild(pregunta).GetComponent<BehaviourSprite>()._right;
        if (b)
            controlPuntaje.IncreaseScore();

        GetComponent<Animator>().speed = 1;
        pregunta++;
        foreach (var manager in managers)
        {
            manager.GetComponent<L5A260ManagerSeleccionTexto>().minuto = minuto;
            manager.GetComponent<L5A260ManagerSeleccionTexto>().pregunta = pregunta;
            manager.GetComponent<L5A260ManagerSeleccionTexto>().segundo = segundo;
            StartCoroutine(manager.GetComponent<L5A260ManagerSeleccionTexto>().Cronometro());
        }

        GetComponent<Animator>().Play(b ? "correcto" : "incorrecto");
        controlAudio.PlayAudio(b ? 1 : 2);
        controlNavegacion.Forward(2);
    }
    IEnumerator Cronometro()
    {
        yield return new WaitForSeconds(1f);
        string tiempo;
        if (tiempoCorre)
        {            
            if (segundo == 00)
            {
                segundo = 59;
                minuto--;
                tiempo = Convert.ToString(segundo);
            }
            else
            {
                segundo--;
                if (segundo < 10)
                    tiempo = "0" + segundo;
                else
                    tiempo = Convert.ToString(segundo);
            }
            if (minuto > 0)
            {
                _cronometro.transform.GetChild(0).GetComponent<Text>().text = minuto + ":" + tiempo;
            }
            else
                _cronometro.transform.GetChild(0).GetComponent<Text>().text = tiempo;

            if (minuto == 0 && segundo == 0)
            {
                controlNavegacion.GoToDesempeno();
                tiempoCorre = false;
            }
            else
                StartCoroutine(Cronometro());
        }
        else
        {
            if (segundo < 10)
                tiempo = "0" + segundo;
            else
                tiempo = Convert.ToString(segundo);

            if (minuto > 0)
            {
                _cronometro.transform.GetChild(0).GetComponent<Text>().text = minuto + ":" + tiempo;
            }
            else
                _cronometro.transform.GetChild(0).GetComponent<Text>().text = tiempo;
        }
    }

    public void revision()
    {
        tiempoCorre = false;
        GetComponent<Animator>().enabled = false;
        transform.GetChild(1).gameObject.SetActive(true);
        if (transform.GetChild(transform.childCount - 1).gameObject.name == "Portada")                
            transform.GetChild(transform.childCount - 1).gameObject.SetActive(false);

    }
    public void resetSeleccionTexto()
    {
        GetComponent<Animator>().enabled = true;        
        GetComponent<BehaviourLayout>()._isEvaluated = false;
        animacionEntrada = false;
        minuto = 3;
        segundo = 0;
        tiempoCorre = false;
        pregunta = 0;
        for (int x = 0; x < listaPreguntas.transform.childCount; x++)
        {
            listaPreguntas.transform.GetChild(x).GetComponent<Image>().color = new Color32(255, 255, 255, 100);
            listaPreguntas.transform.GetChild(x).GetComponent<Image>().sprite =
                listaPreguntas.transform.GetChild(x).GetComponent<BehaviourSprite>()._wrong;
        }
        
        if (texts.Count > 0)
        {
            foreach (var text in texts)
            {
                text.raycastTarget = true;
                text.color = _default;
                text.GetComponent<L5A260TextoSeleccionado>().isOn = false;
            }
        }
        else
        {
            foreach (var toggle in toggles)
            {
                toggle.GetComponent<Image>().raycastTarget = true;
                toggle.isOn = false;
                toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._default;
                toggle.interactable = true;
            }
        }

        GetComponent<Animator>().Rebind();
        if (transform.GetChild(transform.childCount-1).gameObject.name == "Portada")
        {            
            GetComponent<Animator>().speed = 0;            
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
        }        
    }
}
