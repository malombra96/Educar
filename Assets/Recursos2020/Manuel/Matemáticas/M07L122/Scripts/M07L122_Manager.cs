using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class M07L122_Manager : MonoBehaviour
{
    [HideInInspector] public ControlAudio _controlAudio;

    ControlPuntaje _controlPuntaje;

    ControlNavegacion _controlNavegacion;

    public Button _validarBTN;

    #region General Methods

    void Awake ()
    {   
        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlPuntaje = FindObjectOfType<ControlPuntaje>();
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
    }
    void Start()
    {
        /// ManagerInput

        InitializeStateInput();
        _validarBTN.onClick.AddListener (GeneralValidar);

        /// ManagerToggle
        evaluated = 0;
        reset = false;

        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlPuntaje = FindObjectOfType<ControlPuntaje>();
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();

        StartCoroutine(GeneralStateValidar());
        _validarBTN.interactable = false;
    }

    public IEnumerator GeneralStateValidar()
    {
        yield return new WaitForSeconds(.2f);

        //// Input ////////////////
         
        bool s = true;

        foreach (var group in _groupInputField)
        {
            foreach (var inputField in group._inputFields)
            {
                if (inputField.GetComponent<M07L122_Input>()._isEmpty)
                {
                    s = false;
                    break;
                }
            }
        }

        //// Toggle ////////////////

        int n = 0;

        switch (_TypeSelect)
        {
            case TypeSelection.onexgroup:

            foreach (var group in _groupToggle)
                    if(group._dictionarySelection._toggle.Count == 1)
                         n++;

                break;

            case TypeSelection.variousxgroup:

                foreach (var group in _groupToggle)
                    if(group._dictionarySelection._toggle.Count >= 1)
                         n++;

                break;
        }

        _validarBTN.interactable = (n==_groupToggle.Count) && s;

    }

    void GeneralValidar()
    {
        _validarBTN.interactable = false;
        _controlAudio.PlayAudio(0);

        #region Toggle

        int[] rights = new int[_groupToggle.Count]; // Toggles group correctos
        int[] a = new int[_groupToggle.Count]; // Toggles individuales seleccionados y correctos [isOn=true && isRight]
        int[] b = new int[_groupToggle.Count]; // Toggles correctos x grupo
        int[] c = new int[_groupToggle.Count]; // Toggles selecciones [isOn=true]

        scoreTotal = 0;

        for (int j = 0; j < _groupToggle.Count; j++)
        {
            a[j] = 0;
            b[j] = _groupToggle[j]._sizeCorrect;

            for (int i = 0; i < _groupToggle[j].transform.childCount; i++)
            {
                _groupToggle[j].transform.GetChild(i).GetComponent<Toggle>().interactable = false;

                Image img = _groupToggle[j].transform.GetChild(i).GetComponent<Image>();
                bool isON = _groupToggle[j].transform.GetChild(i).GetComponent<Toggle>().isOn;
                bool state = _groupToggle[j].transform.GetChild(i).GetComponent<M07L122_Toggle>().isRight;

                if (isON)
                {
                    c[j]++;

                    if (state)
                        a[j]++;
                }


                if (_TypeQualify == TypeQualify.all)
                    SetSpriteAnswer(img, state);
                else
                    if (isON)
                    SetSpriteAnswer(img, state);

            }

            print($"Grupo #{j} >> isOn={c[j]},isON&isRight={a[j]},RightTotal={b[j]}");

            if (_TypeCalification == TypeCalification.grupo)
            {
                if (a[j] == c[j])
                    rights[j]++;

                if (a[j] == b[j])
                    scoreTotal += rights[j];
            }
            else
            {
                int d = a[j] - c[j];
                d += a[j];

                if (d > 0)
                    rights[j] = d;

                scoreTotal += rights[j];
            }

            rightTotal += b[j];

            print($"Grupo #{j} >> groupRight={rights[j]} TotalCorrectas={scoreTotal},CorrectasNeed={rightTotal}");
        }

        #endregion

        #region Input

        int[] contadorTemp = new int[_groupInputField.Count];
        _listAnswers = new ListAnswers[_groupInputField.Count];

        for (int i = 0; i < _groupInputField.Count; i++)
        {
            _listAnswers[i].inputFields = new InputField[_groupInputField[i]._inputFields.Count]; 
            _listAnswers[i].answers = new bool[_groupInputField[i]._inputFields.Count];

            if (_groupInputField[i]._TipoRespuestas == M07L122_GroupInput.TipoRespuestas.individual)
            {
                for (int j = 0; j < _groupInputField[i]._inputFields.Count; j++)
                {
                    InputField f = _groupInputField[i]._inputFields[j];

                    f.interactable = false;
                    f.GetComponent<M07L122_Input>()._isEnabled = false;

                    bool answer;
                    answer = (f.GetComponent<M07L122_Input>().respuestaCorrecta.Contains(f.GetComponent<InputField>().text));
                    f.GetComponent<M07L122_Input>()._isRight = answer;
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
                    f.GetComponent<M07L122_Input>()._isEnabled = false;

                    bool answer;
                    answer = f.GetComponent<M07L122_Input>()._isRight;

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

        #endregion

        int groupTemp = 0;

        for (int i = 0; i < _groupInputField.Count; i++)
        {
            if (contadorTemp[i] == _groupInputField[i]._inputFields.Count)
                groupTemp++;
        }

        if((groupTemp == _groupInputField.Count) && (scoreTotal == _groupToggle.Count))
        {
            _controlAudio.PlayAudio(1);   
            _controlPuntaje.IncreaseScore(); //scoreTotal+groupTemp

        }
        else
        {
            _controlAudio.PlayAudio(2);    
        }

        /* _controlAudio.PlayAudio(groupTemp == _groupInputField.Count ? 1 : 2);
        _controlAudio.PlayAudio(scoreTotal == _groupToggle.Count ? 1 : 2); */

        /* _controlPuntaje.IncreaseScore(scoreTotal);
        _controlPuntaje.IncreaseScore(groupTemp); */


        _controlNavegacion.Forward(2); 
    }

    public void GeneralReset()
    {
        reset = true;
        ResetSeleccionarToggle();
        InitializeStateInput();
    }

    #endregion

    #region ManagerInput

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

    [HideInInspector] public List<M07L122_GroupInput> _groupInputField;

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

    public void InitializeStateInput()
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

                inputField.GetComponent<M07L122_Input>()._isRight = false;
                inputField.GetComponent<M07L122_Input>()._isEnabled = true;
                inputField.GetComponent<Image>().color = colorFondoInicial;

            }
        }

        _listAnswers = new ListAnswers[0];

        //SetStateValidarBTN();
        //StartCoroutine(GeneralStateValidar());
        
    }

/*     public void CalificarInputs()
    {
        _validarBTN.interactable = false;
        _controlAudio.PlayAudio(0);

        int[] contadorTemp = new int[_groupInputField.Count];
        _listAnswers = new ListAnswers[_groupInputField.Count];

        for (int i = 0; i < _groupInputField.Count; i++)
        {
            _listAnswers[i].inputFields = new InputField[_groupInputField[i]._inputFields.Count]; 
            _listAnswers[i].answers = new bool[_groupInputField[i]._inputFields.Count];

            if (_groupInputField[i]._TipoRespuestas == M07L122_GroupInput.TipoRespuestas.individual)
            {
                for (int j = 0; j < _groupInputField[i]._inputFields.Count; j++)
                {
                    InputField f = _groupInputField[i]._inputFields[j];

                    f.interactable = false;
                    f.GetComponent<M07L122_Input>()._isEnabled = false;

                    bool answer;
                    answer = (f.GetComponent<M07L122_Input>().respuestaCorrecta.Contains(f.GetComponent<InputField>().text));
                    f.GetComponent<M07L122_Input>()._isRight = answer;
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
                    f.GetComponent<M07L122_Input>()._isEnabled = false;

                    bool answer;
                    answer = f.GetComponent<M07L122_Input>()._isRight;

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

        if (_TipoPuntuacion == TipoPuntuacion.grupo)
            _controlPuntaje.IncreaseScore(groupTemp);
        else
        {
            int total = 0;

            foreach (var grupoAnswers in contadorTemp)
                total += grupoAnswers;

            print(total);

            _controlPuntaje.IncreaseScore(total);
        }


        _controlAudio.PlayAudio(groupTemp == _groupInputField.Count ? 1 : 2);
        _controlNavegacion.Forward(2);

    } */

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


    #endregion

    #region  ManagerToggle

    [HideInInspector] public List<M07L122_GroupToggle> _groupToggle;
    
    int scoreTotal;
    int rightTotal; // Cantidad de toggles correctos, solo para calificacion individual
    int evaluated;

    /* [HideInInspector] */ public bool reset;

    #region Configure Parametres SelectionToggle
    
    public enum TypeValidation
    {
        inmediata,
        button
    }

    [Tooltip("Seleccione el tipo de validacion")] [Header("Tipo de Validacion")] public TypeValidation _typeVal;

    public enum TypeSelection
    {
        onexgroup,
        variousxgroup
    }

    [Tooltip("Seleccione el tipo de Selection Toggle")] [Header("Tipo de Selection")] public TypeSelection _TypeSelect;
    [Tooltip("Determine la cantidad de clicks/selecciones")] [Range(1,10)] public int _options;

    public enum TypeCalification 
    {
        individual,
        grupo
    }

    [Tooltip("Seleccione el tipo de calificaccion")] [Header("Tipo de Calificación")] public TypeCalification _TypeCalification;

    public enum TypeQualify
    {
        onlySelection,
        all
    }
    [Tooltip("Seleccione el tipo de validacion en las respuestas")] [Header("¿Como calificar?")] public TypeQualify _TypeQualify;

    public enum NeedSymbol
    {
        onlyToggle,
        withSymbol
    }

    [Tooltip("Seleccione si requiere califiacion x symbolo en las respuestas")] [Header("¿Necesita calificacion con simbolo?")] public NeedSymbol _NeedSymbol;
    
    #endregion
    
    /// <summary>
    /// Recibe la imagen y respuesta, para asignar el correspondiente sprite respuesta
    /// </summary>
    /// <param name="i"></param>
    /// <param name="state"></param>
    public void SetSpriteAnswer(Image i,bool state)
    {
        if(_NeedSymbol == M07L122_Manager.NeedSymbol.onlyToggle)
        {
            i.sprite = state? 
                i.GetComponent<BehaviourSprite>()._right : 
                i.GetComponent<BehaviourSprite>()._wrong;
        }
        else 
        {
            i.sprite = state? 
                i.GetComponent<BehaviourSprite>()._right : 
                i.GetComponent<BehaviourSprite>()._wrong;

            i.transform.GetChild(0).GetComponent<Image>().sprite = state?
                i.transform.GetChild(0).GetComponent<Image>().GetComponent<BehaviourSprite>()._right : 
                i.transform.GetChild(0).GetComponent<Image>().GetComponent<BehaviourSprite>()._wrong;

            i.transform.GetChild(0).gameObject.SetActive(true);
        }

        
    }

    /// <summary>
    /// Asigna sprite default de la imagen recibida
    /// </summary>
    /// <param name="i"></param>
    void SetDefaultSprite(Image i)
    {
        i.sprite = i.GetComponent<BehaviourSprite>()._default;

        if(_NeedSymbol == M07L122_Manager.NeedSymbol.withSymbol)
            i.transform.GetChild(0).gameObject.SetActive(false);    
    }

    public void ResetSeleccionarToggle()
    {
        _validarBTN.interactable = false;
        evaluated=0;
        reset=true;

        for (int j = 0; j < _groupToggle.Count; j++)
        {
            for (int i = 0; i < _groupToggle[j].transform.childCount; i++)
            {
                _groupToggle[j].transform.GetChild(i).GetComponent<Toggle>().interactable = true;
                _groupToggle[j].transform.GetChild(i).GetComponent<Toggle>().isOn = false;

                Image img = _groupToggle[j].transform.GetChild(i).GetComponent<Image>();
                SetDefaultSprite(img); 
                
            }
            
        }

        reset=false;
    }

    #endregion

}
