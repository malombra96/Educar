using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class M10L33_managerInput : MonoBehaviour
{
    
    public int level;
    public GameObject Element;

    M10L33_manager _general;
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

    public List<M10L33_groupInput> _groupInputField;

    [Header("Button Validar")] public Button botonValidar;

    [HideInInspector] public ControlAudio _controlAudio;

    ControlPuntaje _controlPuntaje;

    

    [HideInInspector] public bool haCalificado;

    [Header("LightBox NumPad")] public GameObject _lightBox;


    [Header("Colors Setup")]

    public Color32 colorTextoInicial;
    public Color32 colorTextoCorrecto;
    public Color32 colorTextoIncorrecto;

    public Color32 colorFondoInicial;
    public Color32 colorFondoCorrecto;
    public Color32 colorFondoIncorrecto;

    public GameObject rightImage, wrongImage, background;
    M10L33_player _player;



    void Awake()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlPuntaje = FindObjectOfType<ControlPuntaje>();
        _player = GameObject.FindObjectOfType<M10L33_player>();
        _general = GameObject.FindObjectOfType<M10L33_manager>();

    }

    void Start()
    {
        InitializeState();
        botonValidar.onClick.AddListener(CalificarInputs);
    }

    public void SetStateValidarBTN()
    {
        bool s = true;

        foreach (var group in _groupInputField)
        {
            foreach (var inputField in group._inputFields)
            {
                if (inputField.GetComponent<M10L33_inputField>()._isEmpty)
                {
                    s = false;
                    break;
                }
            }
        }



        botonValidar.interactable = s;
    }

    void InitializeState()
    {
        background.SetActive(true);
        rightImage.SetActive(false);
        wrongImage.SetActive(false);
        foreach (var group in _groupInputField)
        {
            foreach (var inputField in group._inputFields)
            {
                inputField.GetComponent<InputField>().text = "";
                inputField.interactable = true;

                if (inputField.transform.GetChild(1).GetComponent<BehaviourSprite>())
                    inputField.transform.GetChild(1).gameObject.SetActive(false);
                else
                    inputField.transform.GetChild(2).gameObject.SetActive(false);

                if (inputField.transform.GetChild(0).GetComponent<Text>())
                    inputField.transform.GetChild(0).GetComponent<Text>().color = colorTextoInicial;
                else
                    inputField.transform.GetChild(1).GetComponent<Text>().color = colorTextoInicial;

                inputField.GetComponent<M10L33_inputField>()._isEnabled = true;
                inputField.GetComponent<Image>().color = colorFondoInicial;

            }
        }

        SetStateValidarBTN();
    }

    public void CalificarInputs()
    {
        botonValidar.interactable = false;
        _controlAudio.PlayAudio(0);

        int[] contadorTemp = new int[_groupInputField.Count];

        for (int i = 0; i < _groupInputField.Count; i++)
        {
            foreach (var input in _groupInputField[i]._inputFields)
            {
                input.interactable = false;
                input.GetComponent<M10L33_inputField>()._isEnabled = false;
                bool answer = (input.GetComponent<M10L33_inputField>().respuestaCorrecta.Contains(input.GetComponent<InputField>().text));

                switch (calificacion)
                {
                    case TipoCalificacion.texto: TipoCalificationTexto(); break;

                    case TipoCalificacion.fondo: TipoCalificationFondo(); break;

                    case TipoCalificacion.simbolo: TipoCalificationSimbolo(); break;

                    case TipoCalificacion.multiple: _FunctionMultiple.Invoke(); break;

                }

                if (answer)
                    contadorTemp[i]++;
            }
        }

        int groupTemp = 0;

        for (int i = 0; i < _groupInputField.Count; i++)
        {
            if (contadorTemp[i] == _groupInputField[i]._inputFields.Count)
                groupTemp++;
        }



/*
        if (_TipoPuntuacion == TipoPuntuacion.grupo)
            _controlPuntaje.IncreaseScore(groupTemp);
        else
        {
            int total = 0;

            foreach (var grupoAnswers in contadorTemp)
                total += grupoAnswers;

            print(total);

            _controlPuntaje.IncreaseScore(total);
        }*/


       // _controlAudio.PlayAudio(groupTemp == _groupInputField.Count ? 1 : 2);

        if (groupTemp == _groupInputField.Count)
        {
          
            _controlAudio.PlayAudio(1);
            _general.AddGifts(level);
            _controlPuntaje.IncreaseScore();
            StartCoroutine(x(true));
        }
        else {
           
            _controlAudio.PlayAudio(2);

            
            StartCoroutine(x(false));
        }


        

    }

    IEnumerator x(bool valueImage) {
        
            yield return new WaitForSeconds(1);
            
        if (valueImage)
            {
                background.SetActive(false);
                rightImage.SetActive(true);
            }
            else
            {
                background.SetActive(false);
                wrongImage.SetActive(true);
                _general.RestLifes();
        }
            yield return new WaitForSeconds(2);
            Element.GetComponent<M10L33_element>().GetQuestion();
            _player.canMove = true;
        _player.x = 0;

        rightImage.SetActive(false);
            wrongImage.SetActive(false);
            background.transform.parent.gameObject.SetActive(false);
        
        
    }

    public void TipoCalificationTexto()
    {
        foreach (var group in _groupInputField)
        {
            foreach (var input in group._inputFields)
            {
                bool answer = (input.GetComponent<M10L33_inputField>().respuestaCorrecta.Contains(input.GetComponent<InputField>().text));
                SetTextAnswer(input.transform.GetChild(1).GetComponent<Text>(), answer);
            }
        }
    }

    public void TipoCalificationFondo()
    {
        foreach (var group in _groupInputField)
        {
            foreach (var input in group._inputFields)
            {
                bool answer = (input.GetComponent<M10L33_inputField>().respuestaCorrecta.Contains(input.GetComponent<InputField>().text));
                SetFondoAnswer(input.GetComponent<Image>(), answer);
            }
        }

    }

    public void TipoCalificationSimbolo()
    {
        foreach (var group in _groupInputField)
        {
            foreach (var input in group._inputFields)
            {
                bool answer = (input.GetComponent<M10L33_inputField>().respuestaCorrecta.Contains(input.GetComponent<InputField>().text));
                SetSymbolAnswer(input.transform.GetChild(2).gameObject, answer);
                input.transform.GetChild(2).gameObject.SetActive(true);
            }
        }
    }

    void SetFondoAnswer(Image image, bool state) => image.color = state ? colorFondoCorrecto : colorFondoIncorrecto;

    void SetSymbolAnswer(GameObject symbol, bool state)
    {
        Image i = symbol.GetComponent<Image>();
        BehaviourSprite bh = symbol.GetComponent<BehaviourSprite>();
        i.sprite = state ? bh._right : bh._wrong;
    }

    void SetTextAnswer(Text text, bool state) => text.color = state ? colorTextoCorrecto : colorTextoIncorrecto;


    public void resetAll()
    {
        InitializeState();
    }
}
