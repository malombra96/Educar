using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M08L037T_ManagerStep : MonoBehaviour
{
    ControlAudio _controlAudio;
    ControlNavegacion _controlNavegacion;
    ControlPuntaje _controlPuntaje;

    [Header("General Setup")] public GameObject[] _steps;

    [Header("Ecuacion/Igualdad")] public string _igualdad;
    [Header("Ecuacion de la forma [x+y=c]")] public string _equation;

    [Header("Desigualdad")] public string _desigualdad;

    [Header("Coordenadas")]
    public Vector2 _pointA;
    public Vector2 _pointB;


    [Header("Setup Line")]
    public GameObject _line;
    public Toggle _lineSelect;

    public string _logicOperathor;

    [Header("Setup Line")]
    public Image _region;
    public Toggle _regionSelect;
    public bool _compliesWithInequality;

    [Header("BTN Validar")] public Button _validar;

    [HideInInspector] public bool _inReset;

    void Awake()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlPuntaje = FindObjectOfType<ControlPuntaje>();
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();

        _validar.onClick.AddListener(Validar);
        _validar.interactable = false;

        _inReset = false;
    }

    void Start() => InicializarSteps();

    void InicializarSteps()
    {
        for (int i = 0; i < _steps.Length; i++)
            _steps[i].SetActive(i==0);
    }

    public void SetWarning(GameObject warning, bool state) => warning.SetActive(state);

    public void SetNextStep(int n)
    {
        for (int i = 0; i < _steps.Length; i++)
            _steps[i].SetActive(i<=n);
    }

    public void SetAnswerSprite(Image image,bool state)
    {
        image.sprite = state? 
            image.GetComponent<BehaviourSprite>()._right : 
            image.GetComponent<BehaviourSprite>()._wrong; 
    }

    void Validar()
    {
        _validar.interactable = false;

        bool a = false;
        bool b = false;

        #region LogicOperahor & TypeLine

        if(_logicOperathor == "≥" || _logicOperathor == "≤")
        {
            if(_lineSelect.name == "ContinuousLine")
            {
                _line.GetComponent<LineRenderer>().material = _line.GetComponent<M08L037T_MaterialState>()._right;
                _lineSelect.GetComponent<Image>().sprite = _lineSelect.GetComponent<BehaviourSprite>()._right;
                a=true;
            }
            else
            {
                _line.GetComponent<LineRenderer>().material = _line.GetComponent<M08L037T_MaterialState>()._wrong;
                _lineSelect.GetComponent<Image>().sprite = _lineSelect.GetComponent<BehaviourSprite>()._wrong;
            }
        }
        else if(_logicOperathor == ">" || _logicOperathor == "<")
        {
            if(_lineSelect.name == "DottedLine")
            {
                _line.GetComponent<LineRenderer>().material = _line.GetComponent<M08L037T_MaterialState>()._right;
                _lineSelect.GetComponent<Image>().sprite = _lineSelect.GetComponent<BehaviourSprite>()._right;
                a=true;
            }
            else
            {
                _line.GetComponent<LineRenderer>().material = _line.GetComponent<M08L037T_MaterialState>()._wrong;
                _lineSelect.GetComponent<Image>().sprite = _lineSelect.GetComponent<BehaviourSprite>()._wrong;
            }
        }

        #endregion

        #region Inequality & TypeRegion

        if(_compliesWithInequality)
        {
            if(_regionSelect.name == "Yes")
            {
                _region.GetComponent<Image>().color = _region.GetComponent<M08L037T_ColorState>()._right;
                _regionSelect.GetComponent<Image>().sprite = _regionSelect.GetComponent<BehaviourSprite>()._right;
                b=true;
            }
            else
            {
                _region.GetComponent<Image>().color = _region.GetComponent<M08L037T_ColorState>()._wrong;
                _regionSelect.GetComponent<Image>().sprite = _regionSelect.GetComponent<BehaviourSprite>()._wrong;
            }
        }
        else 
        {
            if(_regionSelect.name == "No")
            {
                _region.GetComponent<Image>().color = _region.GetComponent<M08L037T_ColorState>()._right;
                _regionSelect.GetComponent<Image>().sprite = _regionSelect.GetComponent<BehaviourSprite>()._right;
                b=true;
            }
            else
            {
                _region.GetComponent<Image>().color = _region.GetComponent<M08L037T_ColorState>()._wrong;
                _regionSelect.GetComponent<Image>().sprite = _regionSelect.GetComponent<BehaviourSprite>()._wrong;
            }
        }

        #endregion

        _controlAudio.PlayAudio(a&b? 1:2);
        _controlPuntaje.IncreaseScore(a&b? 1:0);
        _controlNavegacion.Forward(2);

        
    }

    public void ResetGeneral()
    {
        _inReset = true;

        foreach (var step in _steps)
        {
            if (step.GetComponent<M08L037T_StepA>())
                step.GetComponent<M08L037T_StepA>().ResetA();
            else if (step.GetComponent<M08L037T_StepB>())
                step.GetComponent<M08L037T_StepB>().ResetB();
            else if (step.GetComponent<M08L037T_StepC>())
                step.GetComponent<M08L037T_StepC>().ResetC();
            else if (step.GetComponent<M08L037T_StepD>())
                step.GetComponent<M08L037T_StepD>().ResetD();
        }
        

        _line = null;
        _lineSelect = null;
        _logicOperathor = "";
        _region = null;
        _regionSelect = null;
        _compliesWithInequality = false;

        InicializarSteps();
        
        _controlPuntaje = FindObjectOfType<ControlPuntaje>();
        _controlPuntaje.resetScore();

        _inReset = false;
    }
}
