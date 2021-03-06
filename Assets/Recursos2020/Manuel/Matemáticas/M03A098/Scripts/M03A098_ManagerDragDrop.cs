using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class M03A098_ManagerDragDrop : MonoBehaviour
{
    ControlPuntaje _controlPuntaje;
    ControlNavegacion _controlNavegacion; 
    [HideInInspector] public ControlAudio _controlAudio;
    [HideInInspector] public Canvas canvas;
    
    [Header("Item List")]
    [HideInInspector]  public List<M03A098_Drag> _drags;
    [HideInInspector] public List<M03A098_Drop> _drops;

    [HideInInspector] public List<BehaviourDropGroup> _groups;
    
    [HideInInspector] public Dictionary<GameObject,bool> answers = new Dictionary<GameObject, bool>();
    int rights;
    int evaluated;

    [Header("Requiere Random")] public bool needRandom;

    [Header("Arrastre Boton Validar")] 
    public Button _validar;

    [Tooltip("Si el valor es 0 tomará validación por defecto")]
    [Header("Activacion Manual de Boton Validar")] [Range(0,10)] public int _customValidar; 

    [Header("Parent Drags")] public RectTransform _parentDrags;

    #region GeneralOptionsDragDrop

    public enum OperatingMethod
    {
        Default,
        Match,
        //Group,
        Create
    }
    
    [Header("Tipo de funcionamiento")] public OperatingMethod _OperatingMethod;

    public enum TypeValidation
    {
        Inmediata,
        Button
    }

    [Header("Tipo de validacion")] public TypeValidation _TypeValidation;
    
    [Flags]
    public enum TypeCalification
    {
        Drag,
        Drop,
        DragDrop,
        Group,
        Symbol,
        Multiple
    }

    [Header("Tipo de calificacion")] public TypeCalification _TypeCalification;
    [Header("Tipo de calificacion Multiple")] public UnityEvent _CallFunction;

    #endregion
    
    void Awake()
    {
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlPuntaje = FindObjectOfType<ControlPuntaje>();
        canvas = FindObjectOfType<Canvas>(); 
        _validar.gameObject.SetActive(_TypeValidation == TypeValidation.Button);
        _validar.onClick.AddListener(delegate { ValidarDragDrop(); });
        //StartCoroutine(StateBtnValidar());
            
    }

    void OnEnable()
    {
        if(needRandom && !GetComponent<BehaviourLayout>()._isEvaluated)
            StartCoroutine(RandomDrag());

        StartCoroutine(StateBtnValidar());
    }
    
    public IEnumerator StateBtnValidar()
    {
        yield return new WaitForSeconds(.1f);

        int i = 0;
        
        foreach (var drag in _drags)
            if (drag._drop != null)
                i++;
        
        //print(i+"//"+_drags.Count);

        if(_customValidar == 0)
            _validar.interactable = (i == _drags.Count);
        else
            _validar.interactable = (i >= _customValidar);
    }

    IEnumerator RandomDrag()
    {
        yield return new WaitForSeconds(.2f);

        GameObject randomTemp;
        Vector3 targetTemp;
        int statePos, placeTemp;
        int indexTemp;
        int rowTemp;

        statePos = 0;

        for (int j = 0; j < _drags.Count; j++)
        {
            int posnew = UnityEngine.Random.Range(0, _drags.Count - 1);

            if (statePos != posnew)
            {
                indexTemp = _drags[j].transform.GetSiblingIndex();
                rowTemp = _drags[j]._row;
                
                targetTemp = _drags[j].transform.localPosition;
                placeTemp = j;

                randomTemp = _drags[j].gameObject;
                
                _drags[j].transform.SetSiblingIndex(_drags[posnew].transform.GetSiblingIndex());
                _drags[j]._row = _drags[posnew]._row;

                
                _drags[j].transform.localPosition = _drags[posnew].transform.localPosition;
                
                
                _drags[posnew].transform.SetSiblingIndex(indexTemp);
                _drags[posnew]._row = rowTemp;
                
                _drags[posnew].transform.localPosition = targetTemp;
                _drags[j] = _drags[posnew];
                _drags[posnew] = randomTemp.GetComponent<M03A098_Drag>();
            }
        }

        foreach (var drag in _drags)
        {
            drag.UpdateDefaultPosition();
            drag.UpdateCurrentPosition();
        }
    }
    
    /// <summary>
    /// Agrega a un diccionario c/u drag y drop con su respectivo estado de respuesta
    /// </summary>
    void ValidarDragDrop()
    {
        _validar.interactable = false;

        foreach (M03A098_Drop drop in _drops)
        {
            drop.GetComponent<Image>().sprite = drop._correctDrag == drop._countDrag? 
                drop.GetComponent<BehaviourSprite>()._right : 
                drop.GetComponent<BehaviourSprite>()._wrong;
        }

        foreach (var drag in _drags)
            drag.GetComponent<M03A098_Drag>().enabled = false;
            
        SetPuntaje();

    }

    /*
    
    /// <summary>
    /// Recibe el drag y drop interactuados,y los agrega a un diccionario con su estado de respuesta 
    /// </summary>
    /// <param name="drag"></param>
    /// <param name="drop"></param>
    public void ImmediatelyValidation(M03A098_Drag drag,M03A098_Drop drop)
    {
         drag.GetComponent<M03A098_Drag>().enabled = false;
        
        answers.Add(drag.gameObject, drag._DropRight.Contains(drop));
        answers.Add(drop.gameObject,drop._drag.GetComponent<M03A098_Drag>()._DropRight.Contains(drop));
        evaluated++;
        SetTypeCalification();
 
    }

    /// <summary>
    /// Itera el diccionario y según el tipo de calificacion envia la imagen y respuesta
    /// </summary>
    void SetTypeCalification()
    {
        switch (_TypeCalification)
        {
            case TypeCalification.Drag:

                TypeCalificationDrag();
                SetPuntaje();

                break;

            case TypeCalification.Drop:

                TypeCalificationDrop();
                SetPuntaje();

                break;

            case TypeCalification.DragDrop:

                TypeCalificationDrag();
                TypeCalificationDrop();
                SetPuntaje();

                break;

            case TypeCalification.Symbol:

                TypeCalificationSymbol();
                SetPuntaje();

                break;

            case TypeCalification.Group:

                TypeCalificationGroup();
                SetPuntajeGroups();
                

                break;

            case TypeCalification.Multiple:

                _CallFunction.Invoke();

                break;
        }

    } 

    #region MethodsCalification

    public void TypeCalificationDrag()
    {
        foreach (var answer in answers)
            if (answer.Key.GetComponent<M03A098_Drag>())
                SetSpriteAnswer(answer.Key.GetComponent<Image>(), answer.Value);
    }

    public void TypeCalificationDrop()
    {
        foreach (var answer in answers)
            if (answer.Key.GetComponent<M03A098_Drop>())
                SetSpriteAnswer(answer.Key.GetComponent<Image>(), answer.Value);
    }

    public void TypeCalificationSymbol()
    {
        foreach (var answer in answers)
            if (answer.Key.GetComponent<M03A098_Drag>())
                SetSpriteAnswer(answer.Key.transform.GetChild(0).GetComponent<Image>(), answer.Value);
    }

    public void TypeCalificationGroup()
    {
        foreach (var group in _groups)
            SetSpriteAnswer(group.GetComponent<Image>(), (group._needRight == group._currentRight && group._currentWrong == 0));
    }
    
    #endregion

    
    
    
    /// <summary>
    /// Recibe la imagen y respuesta, para asignar el correspondiente sprite respuesta
    /// </summary>
    /// <param name="i"></param>
    /// <param name="state"></param>
    public void SetSpriteAnswer(Image i,bool state)
    {

        if (_TypeCalification == TypeCalification.Symbol || _TypeCalification == TypeCalification.Multiple)
            i.gameObject.SetActive(true);
        
        i.sprite = state? 
            i.GetComponent<BehaviourSprite>()._right : 
            i.GetComponent<BehaviourSprite>()._wrong;
    }

    */

    /// <summary>
    /// Asigna sprite default de la imagen recibida
    /// </summary>
    /// <param name="i"></param>
    void SetDefaultSprite(Image i)
    {
        i.sprite = i.GetComponent<BehaviourSprite>()._default;
    }

    void SetDefaultSymbol(Image i)
    {
        i.gameObject.SetActive(false);
        i.sprite = i.GetComponent<BehaviourSprite>()._default;
    }


    /// <summary>
    /// Obtiene las respuestas correstas e instancia el audio y puntaje correspondiente
    /// </summary>
    public void SetPuntaje()
    {
        rights = 0;

        foreach (M03A098_Drop drop in _drops)
        {
            if(drop._countDrag == drop._correctDrag)
                rights++;
        }

        _controlAudio.PlayAudio((rights == _drops.Count)? 1 : 2);
    
        _controlPuntaje.IncreaseScore(rights);
        _controlNavegacion.Forward(2);
          
            
    }

    public void ResetDragDrop()
    {
        rights = 0;

        foreach (var drag in _drags)
        {
            SetDefaultSprite(drag.GetComponent<Image>());
            drag.enabled = true;
            drag.GetComponent<M03A098_Drag>()._drop = null; 
            drag.GetComponent<RectTransform>().SetParent(_parentDrags);
            drag.GetComponent<RectTransform>().anchorMin = new Vector2(.5f,.5f);
            drag.GetComponent<RectTransform>().anchorMax = new Vector2(.5f,.5f); 
            drag.GetComponent<RectTransform>().anchoredPosition = drag.GetComponent<M03A098_Drag>()._defaultPos;
            drag.GetComponent<M03A098_Drag>().UpdateCurrentPosition();

        }


        foreach (var drop in _drops)
        {
            drop.GetComponent<M03A098_Drop>()._countDrag = drop.GetComponent<M03A098_Drop>()._initialValue;
            SetDefaultSprite(drop.GetComponent<Image>());
        }
        
    }

}
