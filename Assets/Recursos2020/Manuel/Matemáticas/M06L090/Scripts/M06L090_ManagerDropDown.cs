using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class M06L090_ManagerDropDown : MonoBehaviour
{
    #region  DropDown
   
   [Header("Colors State")] 
   public Color32 _rightColor;
   public Color32 _wrongColor;
   
   [HideInInspector] public List<M06L090_BehaviourDropDown> _dropdowns;

   int correct;

   #endregion
   
    #region InputField

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

    [HideInInspector] public List<M06L090_GroupInput> _groupInputField;

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


    #endregion


    void Awake()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlPuntaje = FindObjectOfType<ControlPuntaje>();
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
    }
    private void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();

        if (botonValidar)
            botonValidar.onClick.AddListener(EvaluateDropDown);

        InitializeState();
    }

    //////////////////////////////////// DROP DOWN //////////////////////////////////////////////////////////////////////////////////////////////

    void EvaluateDropDown()
    {
        foreach (var dropdown in _dropdowns)
        {
            _controlAudio.PlayAudio(0);
            botonValidar.interactable = false;
            dropdown.GetComponent<Dropdown>().interactable = false;

            switch (dropdown._type)
            {
                case M06L090_BehaviourDropDown.Type.text:

                    SetTextDropDown(dropdown.GetComponent<Dropdown>().captionText, dropdown._indexCurrent == dropdown._indexRight);

                    break;

                case M06L090_BehaviourDropDown.Type.image:

                    SetSpriteDropDown(dropdown.GetComponent<Image>(), dropdown._indexCurrent == dropdown._indexRight);

                    break;
            }
        }

        bool state = (correct == _dropdowns.Count);
        CalificarInputs(state);
 

   }

    /// <summary>
    /// Realiza el cambio de estado [right or wrong] para la imagen recibida
    /// </summary>
    /// <param name="select"></param>
    /// <param name="state"></param>
    void SetSpriteDropDown(Image select, bool state)
    {
        switch (state)
        {
            case true:

                select.color = _rightColor;
                correct++;

                break;

            case false:

                select.color = _wrongColor;

                break;
        }
    }

    /// <summary>
    /// Realiza el cambio de estado [right or wrong] para el texto recibido
    /// </summary>
    /// <param name="select"></param>
    /// <param name="state"></param>
    void SetTextDropDown(Text select, bool state)
    {
        switch (state)
        {
            case true:

                select.color = _rightColor;
                correct++;

                break;

            case false:

                select.color = _wrongColor;

                break;
        }
    }

    void DefaultTextDropDown(Text select) => select.color = Color.black;

    void DefaultSpriteDropDown(Image select) => select.color = Color.white;

    public void ResetManagerDropDown()
    {
        foreach (var dropdown in _dropdowns)
        {
            var bh = dropdown.GetComponent<M06L090_BehaviourDropDown>();
            var dd = dropdown.GetComponent<Dropdown>();

            dropdown.reset = true;

            dd.value = 0;
            bh._indexCurrent = dd.value;

            if (bh._type == M06L090_BehaviourDropDown.Type.text)
            {
                dd.captionText.text = dd.options[0].text;
                DefaultTextDropDown(dd.captionText);
            }
            else
            {
                dd.captionImage.sprite = dd.options[0].image;
                DefaultSpriteDropDown(dd.GetComponent<Image>());
            }


            bh.state = false;
            dd.interactable = true;
            dropdown.reset = false;
        }

        botonValidar.interactable = false;
        correct = 0;
    }

   ///////////////////////////////////////////////////// INPUTFIELD /////////////////////////////////////////////////////////////////////////////

    public void SetStateValidarBTN()
    {
        bool s = true;
        bool t = true;

        foreach (var group in _groupInputField)
        {
            foreach (var inputField in group._inputFields)
            {
                if (inputField.GetComponent<M06L090_BehaviourInputField>()._isEmpty)
                {
                    s = false;
                    break;
                }
            }
        }

        foreach (var dropdown in _dropdowns)
        {
            if (dropdown.state)
                t = true;
            else
            {
                t = false;
                break;
            }

        }

        

        botonValidar.interactable = s&t;
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

                inputField.GetComponent<M06L090_BehaviourInputField>()._isRight = false;
                inputField.GetComponent<M06L090_BehaviourInputField>()._isEnabled = true;
                inputField.GetComponent<Image>().color = colorFondoInicial;

            }
        }

        _listAnswers = new ListAnswers[0];

        SetStateValidarBTN();
    }

    public void CalificarInputs(bool dropdownResult)
    {
        int[] contadorTemp = new int[_groupInputField.Count];
        _listAnswers = new ListAnswers[_groupInputField.Count];

        for (int i = 0; i < _groupInputField.Count; i++)
        {
            _listAnswers[i].inputFields = new InputField[_groupInputField[i]._inputFields.Count]; 
            _listAnswers[i].answers = new bool[_groupInputField[i]._inputFields.Count];

            if (_groupInputField[i]._TipoRespuestas == M06L090_GroupInput.TipoRespuestas.individual)
            {
                for (int j = 0; j < _groupInputField[i]._inputFields.Count; j++)
                {
                    InputField f = _groupInputField[i]._inputFields[j];

                    f.interactable = false;
                    f.GetComponent<M06L090_BehaviourInputField>()._isEnabled = false;

                    bool answer;
                    answer = (f.GetComponent<M06L090_BehaviourInputField>().respuestaCorrecta.Contains(f.GetComponent<InputField>().text));
                    f.GetComponent<M06L090_BehaviourInputField>()._isRight = answer;
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
                    f.GetComponent<M06L090_BehaviourInputField>()._isEnabled = false;

                    bool answer;
                    answer = f.GetComponent<M06L090_BehaviourInputField>()._isRight;

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

        if (_TipoPuntuacion == TipoPuntuacion.grupo && dropdownResult)
            _controlPuntaje.IncreaseScore(groupTemp);


        bool inputResult = (groupTemp == _groupInputField.Count);

        _controlAudio.PlayAudio( (inputResult && dropdownResult)? 1 : 2);
        _controlNavegacion.Forward(2);

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
        ResetManagerDropDown();
    }
}
