using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class L2A224ManagerInputField : MonoBehaviour
{
    public L2A224SeleccionActividad volver;
    public enum TipoCalificacion
    {
        texto,       
    }

    [Header("Tipo de Calficacion")] public TipoCalificacion calificacion;
    [Header("Tipo de calificacion Multiple")] public UnityEvent _FunctionMultiple;

    public enum TipoPuntuacion
    {
        individual,
        grupo
    }

    [Header("Tipo de puntuacion")] public TipoPuntuacion _TipoPuntuacion;

    [HideInInspector] public List<L2A224BehaviourGroupInputField> _groupInputField;

    [Header("Button Validar")] public Button botonValidar;

    [HideInInspector] public ControlAudio _controlAudio;

    ControlPuntaje _controlPuntaje;    

    [HideInInspector] public bool haCalificado;

    [Header("LightBox NumPad")] public GameObject _lightBox;


    [Header("Colors Setup")]

    public Color32 colorTextoInicial;
    public Color32 colorTextoCorrecto;
    public Color32 colorTextoIncorrecto;

    [Header("Lista Respuestas por grupo")] public ListAnswers[] _listAnswers;

    void Awake()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlPuntaje = FindObjectOfType<ControlPuntaje>();        
    }

    void Start()
    {
        InitializeState();
        botonValidar.onClick.AddListener(CalificarInputs);
        _lightBox.SetActive(false);
    }

    public void SetStateValidarBTN()
    {
        bool s = true;

        foreach (var group in _groupInputField)
        {
            foreach (var inputField in group._inputFields)
            {
                if (inputField.GetComponent<L2A224BehaviourInputField>()._isEmpty)
                {
                    s = false;
                    break;
                }
            }
        }
        botonValidar.interactable = s;
    }
    private void OnDisable()
    {
        _lightBox.SetActive(false);
    }
    public void InitializeState()
    {
        foreach (var group in _groupInputField)
        {
            foreach (var inputField in group._inputFields)
            {
                inputField.GetComponent<InputField>().text = "";
                inputField.interactable = true;


                for (int i = 0; i < inputField.transform.childCount; i++)
                    if (inputField.transform.GetChild(i).GetComponent<BehaviourSprite>())
                        inputField.transform.GetChild(i).gameObject.SetActive(false);


                if (inputField.transform.GetChild(0).GetComponent<Text>())
                    inputField.transform.GetChild(0).GetComponent<Text>().color = colorTextoInicial;
                else
                    inputField.transform.GetChild(1).GetComponent<Text>().color = colorTextoInicial;

                inputField.GetComponent<L2A224BehaviourInputField>()._isRight = false;
                inputField.GetComponent<L2A224BehaviourInputField>()._isEnabled = true;        

            }
        }

        _listAnswers = new ListAnswers[0];

        SetStateValidarBTN();
    }

    public void CalificarInputs()
    {
        botonValidar.interactable = false;
        _controlAudio.PlayAudio(0);

        int[] contadorTemp = new int[_groupInputField.Count];
        _listAnswers = new ListAnswers[_groupInputField.Count];

        for (int i = 0; i < _groupInputField.Count; i++)
        {
            _listAnswers[i].inputFields = new InputField[_groupInputField[i]._inputFields.Count];
            _listAnswers[i].answers = new bool[_groupInputField[i]._inputFields.Count];

            if (_groupInputField[i]._TipoRespuestas == L2A224BehaviourGroupInputField.TipoRespuestas.individual)
            {
                for (int j = 0; j < _groupInputField[i]._inputFields.Count; j++)
                {
                    InputField f = _groupInputField[i]._inputFields[j];

                    f.interactable = false;
                    f.GetComponent<L2A224BehaviourInputField>()._isEnabled = false;

                    bool answer;
                    answer = (f.GetComponent<L2A224BehaviourInputField>().respuestaCorrecta.Contains(f.GetComponent<InputField>().text));
                    _listAnswers[i].inputFields[j] = f;
                    _listAnswers[i].answers[j] = answer;


                    if (answer)
                        contadorTemp[i]++;
                }
            }
            else
            {
                _groupInputField[i].EvaluateGroup();

                for (int j = 0; j < _groupInputField[i]._inputFields.Count; j++)
                {
                    InputField f = _groupInputField[i]._inputFields[j];

                    f.interactable = false;
                    f.GetComponent<L2A224BehaviourInputField>()._isEnabled = false;

                    bool answer;
                    answer = f.GetComponent<L2A224BehaviourInputField>()._isRight;

                    _listAnswers[i].inputFields[j] = f;
                    _listAnswers[i].answers[j] = answer;


                    if (answer)
                        contadorTemp[i]++;
                }
            }
        }

        switch (calificacion)
        {
            case TipoCalificacion.texto: TipoCalificationTexto(); break;           
        }

        int groupTemp = 0;

        for (int i = 0; i < _groupInputField.Count; i++)
        {
            if (contadorTemp[i] == _groupInputField[i]._inputFields.Count)
                groupTemp++;
        }

        if (_TipoPuntuacion == TipoPuntuacion.grupo)
            _controlPuntaje.IncreaseScore(groupTemp);
        else
        {
            int total = 0;

            foreach (var grupoAnswers in contadorTemp)
                total += grupoAnswers;


            _controlPuntaje.IncreaseScore(total);
        }


        _controlAudio.PlayAudio(groupTemp == _groupInputField.Count ? 1 : 2);
        volver.VolverAlMenu();

    }

    public void TipoCalificationTexto()
    {
        foreach (var group in _listAnswers)
            for (int i = 0; i < group.inputFields.Length; i++)
                SetTextAnswer(group.inputFields[i].transform.GetChild(1).GetComponent<Text>(), group.answers[i]);

    }   

    public void TipoCalificationSimbolo()
    {
        foreach (var group in _listAnswers)
            for (int i = 0; i < group.inputFields.Length; i++)
            {
                SetSymbolAnswer(group.inputFields[i].transform.GetChild(2).gameObject, group.answers[i]);
                group.inputFields[i].transform.GetChild(2).gameObject.SetActive(true);
            }

    }    

    void SetSymbolAnswer(GameObject symbol, bool state)
    {
        Image i = symbol.GetComponent<Image>();
        BehaviourSprite bh = symbol.GetComponent<BehaviourSprite>();
        i.sprite = state ? bh._right : bh._wrong;
    }

    void SetTextAnswer(Text text, bool state) 
    { 
        text.color = state ? colorTextoCorrecto : colorTextoIncorrecto;
    }


    public void resetAll()
    {
        InitializeState();
    }
}
