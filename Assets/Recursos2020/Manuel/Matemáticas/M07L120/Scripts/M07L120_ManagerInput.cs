using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class M07L120_ManagerInput : MonoBehaviour
{
    [Header("Object Line")] public GameObject _line;

    [Header("Manager PopUp")] public ManagerPopUp _managerPopupA;
    [Header("Manager PopUp")] public ManagerPopUp _managerPopupB;

    [Header("Ecuation")] public GameObject _ecuation;

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

    /* [HideInInspector] */ public List<M07L120_GroupInput> _groupInputField;

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

    [Header("Lista Respuestas por grupo")] public ListAnswers[] _listAnswers;

    void Awake ()
    {   
        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlPuntaje = FindObjectOfType<ControlPuntaje>();
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
    } 

    void Start()
    {
        InitializeState();
        botonValidar.onClick.AddListener (CalificarInputs);
    }

    public void SetStateValidarBTN()
    {
        bool s = true;

        foreach (var group in _groupInputField)
        {
            foreach (var inputField in group._inputFields)
            {
                if (inputField.GetComponent<M07L120_Input>()._isEmpty)
                {
                    s = false;
                    break;
                }
            }
        }

        

        botonValidar.interactable = s;
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
                    if(inputField.transform.GetChild(i).GetComponent<BehaviourSprite>())
                        inputField.transform.GetChild(i).gameObject.SetActive(false);
                    

                if (inputField.transform.GetChild(0).GetComponent<Text>())
                    inputField.transform.GetChild(0).GetComponent<Text>().color = colorTextoInicial;
                else
                    inputField.transform.GetChild(1).GetComponent<Text>().color = colorTextoInicial;

                inputField.GetComponent<M07L120_Input>()._isRight = false;
                inputField.GetComponent<M07L120_Input>()._isEnabled = true;
                inputField.GetComponent<Image>().color = colorFondoInicial;

            }
        }

        _listAnswers = new ListAnswers[0];
        _ecuation.SetActive(false);
        _line.SetActive(false);

        SetStateValidarBTN();
        botonValidar.interactable = false;
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

            if (_groupInputField[i]._TipoRespuestas == M07L120_GroupInput.TipoRespuestas.individual)
            {
                for (int j = 0; j < _groupInputField[i]._inputFields.Count; j++)
                {
                    InputField f = _groupInputField[i]._inputFields[j];

                    f.interactable = false;
                    f.GetComponent<M07L120_Input>()._isEnabled = false;

                    bool answer;
                    answer = (f.GetComponent<M07L120_Input>().respuestaCorrecta.Contains(f.GetComponent<InputField>().text));
                    f.GetComponent<M07L120_Input>()._isRight = answer;
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
                    f.GetComponent<M07L120_Input>()._isEnabled = false;

                    bool answer;
                    answer = f.GetComponent<M07L120_Input>()._isRight;

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

            case TipoCalificacion.fondo: TipoCalificationFondo(); break;

            case TipoCalificacion.simbolo: TipoCalificationSimbolo(); break;

            case TipoCalificacion.multiple: _FunctionMultiple.Invoke();  break;
        } 

        int groupTemp = 0;

        for (int i = 0; i < _groupInputField.Count; i++)
        {
            if (contadorTemp[i] == _groupInputField[i]._inputFields.Count)
                groupTemp++;
        }

        /* if (_TipoPuntuacion == TipoPuntuacion.grupo)
            _controlPuntaje.IncreaseScore(groupTemp);
        else
        {
            int total = 0;

            foreach (var grupoAnswers in contadorTemp)
                total += grupoAnswers;

            print(total);

            _controlPuntaje.IncreaseScore(total);
        } */

        _controlPuntaje.IncreaseScore(groupTemp);
        _controlAudio.PlayAudio(groupTemp == _groupInputField.Count ? 1 : 2);

        if(groupTemp == _groupInputField.Count)
        {
            StartCoroutine(SetForward());
        }
        else
            _controlNavegacion.Forward(2); 
    }

    IEnumerator SetForward()
    {
        yield return new WaitForSeconds(2);
        _managerPopupA.ClosePopUp();
        _managerPopupB.ClosePopUp();
        StateToggles(false);
        _line.SetActive(true);
        _ecuation.SetActive(true);
        _controlNavegacion.Forward(2); 
    }

    void StateToggles(bool state)
    {
        foreach (Toggle t in _managerPopupA._toggle)
            t.interactable = state;

        foreach (Toggle t in _managerPopupB._toggle)
            t.interactable = state;
    }



    public void TipoCalificationTexto()
    {
        foreach (var group in _listAnswers)
            for (int i = 0; i < group.inputFields.Length; i++)
                SetTextAnswer(group.inputFields[i].transform.GetChild(1).GetComponent<Text>(), group.answers[i]);
        
    }

    public void TipoCalificationFondo()
    {
        foreach (var group in _listAnswers)
            for (int i = 0; i < group.inputFields.Length; i++)
                SetFondoAnswer(group.inputFields[i].GetComponent<Image>(), group.answers[i]);

        
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

    void SetFondoAnswer(Image image,bool state) => image.color = state? colorFondoCorrecto : colorFondoIncorrecto;

    void SetSymbolAnswer(GameObject symbol,bool state)
    {
        Image i = symbol.GetComponent<Image>();
        BehaviourSprite bh = symbol.GetComponent<BehaviourSprite>();
        i.sprite = state? bh._right : bh._wrong;
    }

    void SetTextAnswer(Text text,bool state) => text.color = state? colorTextoCorrecto : colorTextoIncorrecto;


    public void resetAll()
    {
        InitializeState();
        _managerPopupA.ClosePopUp();
        _managerPopupB.ClosePopUp();
        StateToggles(true);

    }
}
