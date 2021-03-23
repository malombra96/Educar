using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class M09A060_ManagerST2 : MonoBehaviour
{
    ControlNavegacion _controlNavegacion;
    [HideInInspector] public ControlAudio _controlAudio;
    ControlPuntaje _controlPuntaje;

    [HideInInspector] public List<M09A060_BehaviourTG2> _groupToggle;
    
    int scoreTotal;
    int rightTotal; // Cantidad de toggles correctos, solo para calificacion individual
    int evaluated;

    [HideInInspector] public bool reset;

    #region Configure Parametres SelectionToggle
    
    public enum TypeValidation
    {
        inmediata,
        button
    }

    [Tooltip("Seleccione el tipo de validacion")] [Header("Tipo de Validacion")] public TypeValidation _typeVal;
    public Button _validarBTN;

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
    
    M09A060_ManagerInputField _ManagerInputField;
    private void Start()
    {
        evaluated = 0;
        reset = false;

        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlPuntaje = FindObjectOfType<ControlPuntaje>();
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();

        _ManagerInputField = GetComponent<M09A060_ManagerInputField>();

        //_validarBTN.gameObject.SetActive(true);
        _validarBTN.onClick.AddListener(EvaluateGroupsToggle);

        StartCoroutine(StateBtnValidar());
        
    }

    /// Determina si la seleccion de toggles es la necesaria para habilitar el boton Validar
    public IEnumerator StateBtnValidar()
    {
        yield return new WaitForSeconds(.2f);
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

        _validarBTN.interactable = (n==_groupToggle.Count);  
        
    }

    void EvaluateGroupsToggle()
    {
        _validarBTN.interactable = false;

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
                bool state = _groupToggle[j].transform.GetChild(i).GetComponent<M09A060_BehaviourT2>().isRight;

                if(isON)
                {
                    c[j]++;

                    if(state)
                        a[j]++;
                }
                    

                if(_TypeQualify == TypeQualify.all)
                    SetSpriteAnswer(img,state);
                else
                    if(isON)
                        SetSpriteAnswer(img,state);      
                
            }

            print($"Grupo #{j} >> isOn={c[j]},isON&isRight={a[j]},RightTotal={b[j]}");
            
            if(_TypeCalification == TypeCalification.grupo)
            {
                if(a[j]==c[j])
                    rights[j]++;

                if(a[j] == b[j])
                    scoreTotal += rights[j];
            }
            else
            {
                int d = a[j]-c[j];
                d += a[j];

                if(d > 0)
                    rights[j] = d;

                scoreTotal += rights[j];
            }

            rightTotal += b[j];

            print($"Grupo #{j} >> groupRight={rights[j]} TotalCorrectas={scoreTotal},CorrectasNeed={rightTotal}");
        }

        SetPuntaje();
    }

    public void EvaluateGroupsToggleImmediately(M09A060_BehaviourTG2 completed)
    {
        bool answer = false;
        int indexGroup = _groupToggle.IndexOf(completed);

        for (int i = 0; i < _groupToggle[indexGroup].transform.childCount; i++)
        {
            _groupToggle[indexGroup].transform.GetChild(i).GetComponent<Toggle>().interactable = false;

            Image img = _groupToggle[indexGroup].transform.GetChild(i).GetComponent<Image>();
            bool state = _groupToggle[indexGroup].transform.GetChild(i).GetComponent<M09A060_BehaviourT2>().isRight;
            bool isON = _groupToggle[indexGroup].transform.GetChild(i).GetComponent<Toggle>().isOn;

            if(_TypeQualify == TypeQualify.all)
                    SetSpriteAnswer(img,state);
                else
                    if(isON)
                        SetSpriteAnswer(img,state);

        }

        Toggle t = _groupToggle[indexGroup].GetComponent<ToggleGroup>().ActiveToggles().FirstOrDefault();
        answer = (t.isOn && t.GetComponent<M09A060_BehaviourT2>().isRight);

        string z = ("Grupo" + _groupToggle[indexGroup].name +"select"+t.name+"is"+ answer);
        //print(z);

        SetPuntaje(answer);
    }

    //// Instancia el puntaje y audio, al validar por boton
    void SetPuntaje()
    {
        _ManagerInputField.CalificarInputs();

        if(_TypeCalification == TypeCalification.grupo)
            _controlAudio.PlayAudio( ((scoreTotal ==_groupToggle.Count) && _ManagerInputField.stateAnswers)? 1:2);
        else
            _controlAudio.PlayAudio( ((scoreTotal ==rightTotal) && _ManagerInputField.stateAnswers)? 1:2);
            
        
        _controlPuntaje.IncreaseScore(scoreTotal);
        _controlNavegacion.Forward(2);
    }

    /// Instancia el puntaje y audio, al seleccionar validacion inmediata
    void SetPuntaje(bool answer)
    {
        evaluated++;

        if(answer)
        {
            _controlAudio.PlayAudio(1);
            _controlPuntaje.IncreaseScore();
        }
        else
        {
            _controlAudio.PlayAudio(2);
        }

        if(evaluated == _groupToggle.Count)
            _controlNavegacion.Forward(2);
        
    }

    /// <summary>
    /// Recibe la imagen y respuesta, para asignar el correspondiente sprite respuesta
    /// </summary>
    /// <param name="i"></param>
    /// <param name="state"></param>
    public void SetSpriteAnswer(Image i,bool state)
    {
        if(_NeedSymbol == M09A060_ManagerST2.NeedSymbol.onlyToggle)
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

        if(_NeedSymbol == M09A060_ManagerST2.NeedSymbol.withSymbol)
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

}
