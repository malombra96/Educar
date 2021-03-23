using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class M09A075_ManagerSeleccionarToggle : MonoBehaviour
{
    ControlNavegacion _controlNavegacion;
    [HideInInspector] public ControlAudio _controlAudio;
    ControlPuntaje _controlPuntaje;

    M09A075_ManagerTrailSelect _managerT;

    [HideInInspector] public List<M09A075_BehaviourToggleGroup> _groupToggle;
    
    int scoreTotal;
    int rightTotal; // Cantidad de toggles correctos, solo para calificacion individual
    int evaluated;

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
    
    #endregion
    
    #region Parametres Game

    [Header("Player")] public GameObject _player;

    Animator _animator;
    [Header("Position X")] public int _posX;

    [Header("Options Numbers")] public GameObject[] _numberOptions;

    [Header("Range Integer")] [Range(5,55)] public int _setRange;

    [Header("Player Position")] public Vector2 posPlayer;


    #endregion
    private void Start()
    {
        evaluated = 0;
        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlPuntaje = FindObjectOfType<ControlPuntaje>();
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
        _animator = _player.GetComponent<Animator>();
    
        SetValidar();
        StartCoroutine(StateBtnValidar());

    }

    void OnEnable()
    {   
        _managerT = FindObjectOfType<M09A075_ManagerTrailSelect>();
        _managerT.bgTrail.anchoredPosition = new Vector2(_posX,0);
    }
    
    /// Determina el estado del boton validar
    void SetValidar()
    {
        switch (_typeVal)
        {
            case TypeValidation.button:

                _validarBTN.gameObject.SetActive(true);
                _validarBTN.onClick.AddListener(EvaluateGroupsToggle);
                    
                break;

            case TypeValidation.inmediata:

               _validarBTN.gameObject.SetActive(false);

                break;
        }
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
                    if(group._dictionarySelection._toggle.Count == _options)
                         n++;

                break;
        }

        _validarBTN.interactable = (n==_groupToggle.Count);  
        
    }

    void EvaluateGroupsToggle()
    {
        _validarBTN.interactable = false;

        int[] rights = new int[_groupToggle.Count];
        int[] a = new int[_groupToggle.Count];
        int[] b = new int[_groupToggle.Count];

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
                bool state = _groupToggle[j].transform.GetChild(i).GetComponent<M09A075_BehaviourToggle>().isRight;

                if(isON && state)
                    a[j]++;

                if(_TypeQualify == TypeQualify.all)
                    SetSpriteAnswer(img,state);
                else
                    if(isON)
                        SetSpriteAnswer(img,state);      
                
            }
            
            if(_TypeCalification == TypeCalification.grupo)
            {
                if(a[j]==b[j])
                    rights[j]++;

                scoreTotal += rights[j];
            }
            else
            {
                rights[j] = a[j];
                scoreTotal += rights[j];
            }

            rightTotal += b[j];
        }

        SetPuntaje();
    }

    public void EvaluateGroupsToggleImmediately(M09A075_BehaviourToggleGroup completed)
    {
        bool answer = false;
        int indexGroup = _groupToggle.IndexOf(completed);

        for (int i = 0; i < _groupToggle[indexGroup].transform.childCount; i++)
            _groupToggle[indexGroup].transform.GetChild(i).GetComponent<Toggle>().interactable = false;

        Toggle t = _groupToggle[indexGroup].GetComponent<ToggleGroup>().ActiveToggles().FirstOrDefault();
        answer = (t.isOn && t.GetComponent<M09A075_BehaviourToggle>().isRight);

        SetAnimation(t.name,answer,completed);
        
    }

    void SetSprite(M09A075_BehaviourToggleGroup completed)
    {
        int indexGroup = _groupToggle.IndexOf(completed);
        int indexToggle = 0;
        Transform option;

        for (int i = 0; i < _groupToggle[indexGroup].transform.childCount; i++)
        {
            //_groupToggle[indexGroup].transform.GetChild(i).GetComponent<Toggle>().interactable = false;

            Image img = _groupToggle[indexGroup].transform.GetChild(i).GetComponent<Image>();
            bool state = _groupToggle[indexGroup].transform.GetChild(i).GetComponent<M09A075_BehaviourToggle>().isRight;
            bool isON = _groupToggle[indexGroup].transform.GetChild(i).GetComponent<Toggle>().isOn;

            if (_TypeQualify == TypeQualify.all)
            {
                SetSpriteAnswer(img, state);
            }
            else
            {
                if (isON)
                {
                    indexToggle = i;
                    SetSpriteAnswer(img, state);
                }
            }
                
        }

        option = _numberOptions[indexToggle].transform;

        Toggle t = _groupToggle[indexGroup].GetComponent<ToggleGroup>().ActiveToggles().FirstOrDefault();
        bool a = (t.isOn && t.GetComponent<M09A075_BehaviourToggle>().isRight);

        for (int i = 0; i < option.childCount; i++)
        {
            option.GetChild(i).GetComponent<Image>().sprite = a? 
                option.GetChild(i).GetComponent<BehaviourSprite>()._right : 
                option.GetChild(i).GetComponent<BehaviourSprite>()._wrong;
        }

        _managerT._range = _setRange;
    }

    void SetAnimation(string s, bool answer,M09A075_BehaviourToggleGroup completed)
    {
        if (s.Contains("+"))
        {
            switch (s.Split('+')[1])
            {
                case "0": _animator.SetInteger("state",3); break;
                case "1": _animator.SetInteger("state",1); break;
                case "2": _animator.SetInteger("state",2); break;
            }
        }
        else
        {
            switch (s.Split('-')[1])
            {
                case "1": _animator.SetInteger("state",-1); break;
                case "2": _animator.SetInteger("state",-2); break;
            }
        }

        StartCoroutine(DelayXPuntaje(completed,answer));
    }

    IEnumerator DelayXPuntaje(M09A075_BehaviourToggleGroup completed,bool answer)
    {
        yield return new WaitForSeconds(3);
        SetSprite(completed);
        SetPuntaje(answer);

    }

    //// Instancia el puntaje y audio, al validar por boton
    void SetPuntaje()
    {
        if(_TypeCalification == TypeCalification.grupo)
            _controlAudio.PlayAudio(scoreTotal ==_groupToggle.Count? 1:2);
        else
            _controlAudio.PlayAudio(scoreTotal == rightTotal? 1:2);
        
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
            _managerT.nLifes--;
            _controlAudio.PlayAudio(2);
        }

        if(_managerT.nLifes == -1)
            _controlNavegacion.GoToLayout(9,2);
        else if(evaluated == _groupToggle.Count)
            _controlNavegacion.Forward(2);
        
    }

    /// <summary>
    /// Recibe la imagen y respuesta, para asignar el correspondiente sprite respuesta
    /// </summary>
    /// <param name="i"></param>
    /// <param name="state"></param>
    public void SetSpriteAnswer(Image i,bool state)
    {
        i.sprite = state? 
            i.GetComponent<BehaviourSprite>()._right : 
            i.GetComponent<BehaviourSprite>()._wrong;
    }

    /// <summary>
    /// Asigna sprite default de la imagen recibida
    /// </summary>
    /// <param name="i"></param>
    void SetDefaultSprite(Image i)
    {
        i.sprite = i.GetComponent<BehaviourSprite>()._default;
    }

    public void ResetSeleccionarToggle()
    {
        _validarBTN.interactable = true;
        evaluated=0;

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

        for (int i = 0; i < _numberOptions.Length; i++)
            for (int j = 0; j < _numberOptions[i].transform.childCount; j++)
                _numberOptions[i].transform.GetChild(j).GetComponent<Image>().sprite = _numberOptions[i].transform.GetChild(j).GetComponent<BehaviourSprite>()._default;

        if(_animator)
            _animator.SetInteger("state",0);  

        _player.GetComponent<RectTransform>().anchoredPosition = posPlayer;
        
    }
}
