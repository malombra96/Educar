using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M10A41_Manager_Aplico_3 : MonoBehaviour
{
    [Header("Boton Validar :")] public Button btn_validar;

    [Header("Respuesta : ")] public GameObject respuesta;

    private M10A41_Grid casilla;
    public RectTransform mira;
    private Vector2 posInicialRespuesta;
    
    [HideInInspector] public ControlAudio _controlAudio;

    ControlPuntaje _controlPuntaje;

    ControlNavegacion _controlNavegacion;
    void Awake ()
    {   
        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlPuntaje = FindObjectOfType<ControlPuntaje>();
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
    } 

    void Start()
    {
        mira = FindObjectOfType<M10A41_Mira>().GetComponent<RectTransform>();
        respuesta.SetActive(false);
        btn_validar.interactable = false;
        btn_validar.onClick.AddListener(Check);
        posInicialRespuesta = new Vector2(432, -320);
    }

    public void Ready(M10A41_Grid opcion)
    {
        casilla = opcion;
        btn_validar.interactable = true;
    }
    
    public void Check()
    {
        mira.GetComponent<M10A41_Mira>().MoverAlClick = false;
        respuesta.SetActive(true);
        btn_validar.interactable = false;
        if (casilla.Right)
        {
            respuesta.GetComponent<Image>().sprite = respuesta.GetComponent<BehaviourSprite>()._default;
            respuesta.GetComponent<RectTransform>().anchoredPosition = mira.GetComponent<RectTransform>().anchoredPosition;

            _controlPuntaje.IncreaseScore();
            _controlAudio.PlayAudio(1);
            StartCoroutine(Next(3));
        }
        else
        {
            respuesta.GetComponent<Image>().sprite = respuesta.GetComponent<BehaviourSprite>()._selection;
            _controlAudio.PlayAudio(2);            
            casilla.transform.GetChild(0).GetComponent<Animator>().Play("Explosion");
            StartCoroutine(Next(3.3f));
        }
    }

    public void ResetAll()
    {
        if (mira)
        {
            mira.GetComponent<M10A41_Mira>().MoverAlClick = true;
            mira.GetComponent<M10A41_Mira>().Mover = true;
        }
        respuesta.SetActive(false);
        btn_validar.interactable = false;
        respuesta.GetComponent<RectTransform>().anchoredPosition = posInicialRespuesta;
    }
    

    IEnumerator Next(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        _controlNavegacion.Forward();
    }
}
