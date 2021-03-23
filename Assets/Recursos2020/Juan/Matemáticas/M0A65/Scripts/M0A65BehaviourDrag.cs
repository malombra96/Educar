using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class M0A65BehaviourDrag : MonoBehaviour,IDragHandler,IEndDragHandler,IBeginDragHandler,IPointerClickHandler
{
    M0A65ManagerDragDrop _managerDragDrop;
    public GameObject drag;
    public bool mover;
    int x = 0;
    #region DefaultState

    [HideInInspector] public Vector2 _defaultPos;

    #endregion
    public bool amarillo, azul;
    [HideInInspector] public Vector2 _currentPos;

    [Tooltip("Arrastre aquí los drop correctos")] [Header("Correct Drop")] public List<M0A65BehaviourDrop> _DropRight;
    /*[HideInInspector]*/ [Header("Current Drop")] public GameObject _drop;

    [Header("States")]
    /*[HideInInspector] */
    public bool inDrop;
    [HideInInspector] public bool swap;

    [Header("Math Setup")]
    [Tooltip("Ingrese el index correspondiente a la fila o columna")] public int _row;


    int indexSibling;
    RectTransform _rectTransform;
    Image _image;

    private void Awake()
    {
        inDrop = false;
        swap = false;
        drag.GetComponent<CanvasGroup>().blocksRaycasts = true;
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        mover=true;
        _defaultPos = _rectTransform.anchoredPosition;

        UpdateCurrentPosition();

        _managerDragDrop = FindObjectOfType<M0A65ManagerDragDrop>();
        _managerDragDrop._drags.Add(this);
    }       
    public void OnBeginDrag(PointerEventData eventData)
    {     
     
        _managerDragDrop._controlAudio.PlayAudio(0);
        
        inDrop = false;
        UpdateCurrentPosition();

        indexSibling = _rectTransform.GetSiblingIndex(); // Obtiene el index layout respecto a los demas objetos del mismo nivel
        var max = _rectTransform.parent.transform.childCount; // Obtiene el index max
        _rectTransform.SetSiblingIndex(max - 1); // Posiciona el objeto sobre todos los demas

        GetComponent<CanvasGroup>().blocksRaycasts = false;
        SetSpriteState(true);

        if (x == 0 && mover)
        {
            x++;
            drag.name = "Drag";
            Instantiate(drag, transform.parent);            
        }      
    }

    public void OnDrag(PointerEventData eventData)
    {        
        if(_drop)
        {           
            _drop.GetComponent<M0A65BehaviourDrop>()._group.reAcomodar(_drop);              
        }       
        if(mover)
        {            
            _rectTransform.anchoredPosition += eventData.delta / _managerDragDrop.canvas.scaleFactor;  
            _drop=null;                                
        }
      
    }

    public void OnEndDrag(PointerEventData eventData)
    {        
      
        if (!swap)
        {
            if (!inDrop)
            {
                //print(name+"return");
                _rectTransform.anchoredPosition = _currentPos;
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                SetSpriteState(false);                 
                Destroy(drag);
                x = 0;
            }
            else
            {                
                _drop.GetComponent<M0A65BehaviourDrop>()._group.bloquear(_drop);
                inDrop = false;                
                UpdateCurrentPosition();
                SetSpriteState(false);
            }
        }
        else
        {
            //print(name+"intercambio");
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            UpdateCurrentPosition();
            SetSpriteState(false);
            GetComponent<M0A65BehaviourDrag>().swap = false;
        }                   
      
    }
    public void OnDrop(PointerEventData eventData)
    {
        //print("OnDrop"+name);

        GameObject other = eventData.pointerDrag; // Get object drag

        SwapCurrentPosition(other);
        SwapIndexRow(other);
        SwapDrop(other);
        SwapDrag(other);
        UpdateCurrentPosition();
        SetSpriteState(false);
        print("fdgh");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        inDrop = false;
        swap = false;        
       //mover = false;
        print("1");

        if (_drop)
        {            
            print("2");
            _drop.GetComponent<M0A65BehaviourDrop>()._group.reAcomodar(_drop);
            if (mover)
            {
                _drop.GetComponent<M0A65BehaviourDrop>()._drag = null;
                _drop = null;               
                x = 0;
                GetComponent<RectTransform>().anchoredPosition = _defaultPos;
                UpdateCurrentPosition();
                StartCoroutine(_managerDragDrop.StateBtnValidar());
                Destroy(drag);
            }
        }    

    }

    /// Actualiza la posicion default del drag 
    public void UpdateDefaultPosition() => _defaultPos = GetComponent<RectTransform>().anchoredPosition;
    /// Actualiza la posicion actual del drag 
    public void UpdateCurrentPosition() =>  _currentPos = GetComponent<RectTransform>().anchoredPosition;     

    /// Intercambia la posicion actual entre los drag
    void SwapCurrentPosition(GameObject other)
    {
        other.GetComponent<M0A65BehaviourDrag>().swap = true; // Estado de intercambio 
        Vector2 a = other.GetComponent<M0A65BehaviourDrag>()._currentPos;
        other.GetComponent<RectTransform>().anchoredPosition = _currentPos;
        GetComponent<RectTransform>().anchoredPosition = a;
    }

    /// Intercambia la posicion default entre los drag 
    public void SwapDefaultPosition(GameObject other)
    {
        //print(other.name+"SwapDefault"+name);

        Vector2 x = other.GetComponent<M0A65BehaviourDrag>()._defaultPos;
        other.GetComponent<M0A65BehaviourDrag>()._defaultPos = _defaultPos;
        _defaultPos = x;
    }

    ///Intercambia el objeto drag asociado a los drop
    void SwapDrag(GameObject other)
    {
        //print(other.name+"Sobre"+name);

        GameObject m = other.GetComponent<M0A65BehaviourDrag>()._drop;
        GameObject n = _drop;

        if (m)
            other.GetComponent<M0A65BehaviourDrag>()._drop.GetComponent<M0A65BehaviourDrop>()._drag = other;

        if (n)
            _drop.GetComponent<M0A65BehaviourDrop>()._drag = gameObject;

    }

    /// Intercambia el objeto drop entre los drag
    void SwapDrop(GameObject other)
    {
        GameObject x = other.GetComponent<M0A65BehaviourDrag>()._drop;
        other.GetComponent<M0A65BehaviourDrag>()._drop = _drop;
        _drop = x;

        SwapDefaultPosition(other);
    }

    void SwapIndexRow(GameObject other)
    {
        if (_managerDragDrop._OperatingMethod == M0A65ManagerDragDrop.OperatingMethod.Match)
        {
            int x = other.GetComponent<M0A65BehaviourDrag>()._row;
            other.GetComponent<M0A65BehaviourDrag>()._row = this._row;
            this._row = x;
        }

    }

    void SetSpriteState(bool state)
    {
        GetComponent<Image>().sprite = state ?
            GetComponent<BehaviourSprite>()._selection :
            GetComponent<BehaviourSprite>()._default;

    }

}
