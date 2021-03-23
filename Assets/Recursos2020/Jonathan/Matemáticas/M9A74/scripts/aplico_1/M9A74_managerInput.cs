using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class M9A74_managerInput : MonoBehaviour
{
    public M9A7_rana _rana;
    public enum TipoCalificacion
    {
        texto,
        fondo,
        simbolo,

        multiple
    }


    [Header("Tipo de Calficacion")] public TipoCalificacion calificacion;
    [Header("Tipo de calificacion Multiple")] public UnityEvent _FunctionMultiple;

    public enum TipoPuntuacion
    {
        individual,
        grupo
    }

    [Header("Tipo de puntuacion")] public TipoPuntuacion _TipoPuntuacion;

    public List<M9A74_group> _groupInputField;

    [Header("Button Validar")] public Button botonValidar;

    [HideInInspector] public ControlAudio _controlAudio;

    ControlPuntaje _controlPuntaje;

    ControlNavegacion _controlNavegacion;

    [HideInInspector] public bool haCalificado;

    [Header("LightBox NumPad")] public GameObject _lightBox;


    [Header("Colors Setup")]

    public Color32 colorTextoInicial;
    public Color32 colorTextoCorrecto;
    public Color32 colorTextoIncorrecto;

    public Color32 colorFondoInicial;
    public Color32 colorFondoCorrecto;
    public Color32 colorFondoIncorrecto;

    public int contadorValidar;

    [Header("Lista Respuestas por grupo")] public ListAnswers[] _listAnswers;

    void Awake()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlPuntaje = FindObjectOfType<ControlPuntaje>();
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
    }

    void Start()
    {
        InitializeState();
        botonValidar.onClick.AddListener(CalificarInputs);
        botonValidar.interactable = false;
    }

    public void SetStateValidarBTN()
    {
        //bool s = true;

        //foreach (var group in _groupInputField)
        //{
        //    foreach (var inputField in group._inputFields)
        //    {
        //        if (inputField.GetComponent<M9A74_input>()._isEmpty)
        //        {
        //            s = false;
        //            break;
        //        }
        //    }
        //}



        //botonValidar.interactable = s;
    }

    private void Update()
    {
        if (contadorValidar < _groupInputField.Count) {
            if (_groupInputField[contadorValidar]._inputFields[0].GetComponent<M9A74_input>()._isEmpty)
            {
                botonValidar.interactable = false;
            }
            else
            {
                botonValidar.interactable = true;
            }
        }
        
    }

    void InitializeState()
    {
        foreach (var group in _groupInputField)
        {
            foreach (var inputField in group._inputFields)
            {
                inputField.GetComponent<InputField>().text = "";
                inputField.interactable = false;


                for (int i = 0; i < inputField.transform.childCount; i++)
                    if (inputField.transform.GetChild(i).GetComponent<BehaviourSprite>())
                        inputField.transform.GetChild(i).gameObject.SetActive(false);


                if (inputField.transform.GetChild(0).GetComponent<Text>())
                    inputField.transform.GetChild(0).GetComponent<Text>().color = colorTextoInicial;
                else
                    inputField.transform.GetChild(1).GetComponent<Text>().color = colorTextoInicial;

                inputField.GetComponent<M9A74_input>()._isRight = false;
                inputField.GetComponent<Image>().sprite = inputField.GetComponent<BehaviourSprite>()._default;
                inputField.GetComponent<M9A74_input>()._isEnabled = true;
                inputField.GetComponent<Image>().color = colorFondoInicial;

            }
        }

        _listAnswers = new ListAnswers[0];

        _groupInputField[0]._inputFields[0].GetComponent<InputField>().interactable = true;

        SetStateValidarBTN();
    }

    public void CalificarInputs()
    {
        
        botonValidar.interactable = false;
        _controlAudio.PlayAudio(0);

        InputField f = _groupInputField[contadorValidar]._inputFields[0];

        f.interactable = false;
        f.GetComponent<M9A74_input>()._isEnabled = false;

        bool answer;
        answer = (f.GetComponent<M9A74_input>().respuestaCorrecta.Contains(f.GetComponent<InputField>().text));
        SetTextAnswer(f.gameObject,answer);

        if (answer)
        {
            _controlAudio.PlayAudio(1);
            
            if (f.GetComponent<M9A74_input>().isLast)
            {   
                _controlPuntaje.IncreaseScore();
                _rana.Move(contadorValidar);
                contadorValidar++;
                _controlNavegacion.Forward(1.5f);
                
            }
            else {

                _rana.Move(contadorValidar);
                contadorValidar++;
                _groupInputField[contadorValidar]._inputFields[0].interactable = true;
            }
            //_groupInputField[contadorValidar].GetComponent<M9A74_group>().isQualifyed = true;
        }
        else
        {
            _controlAudio.PlayAudio(2);
            _controlNavegacion.Forward(1);
        }

        botonValidar.interactable = true;

    }


    void SetTextAnswer(GameObject gm, bool state) {
        if (state)
        {
            gm.GetComponent<Image>().sprite = gm.GetComponent<BehaviourSprite>()._right;
        }
        else {
            gm.GetComponent<Image>().sprite = gm.GetComponent<BehaviourSprite>()._wrong;
        }
    }


    public void resetAll()
    {
        InitializeState();
        _rana.initial();
        contadorValidar = 0;
        botonValidar.interactable = false;
    }

    public void Block() {
        botonValidar.interactable = false;
    }
}
