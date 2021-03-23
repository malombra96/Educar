using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class M10A38BehaviourDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
{
    M10A38ManagerDragDrop _managerDragDrop;
    [Header("Grupo")] public M10A38BehaviourDropGroup _group;
    public string soy;    
    public bool correcto;
    [HideInInspector] public bool mover = true;
    #region DefaultState

    [HideInInspector] public Vector2 _defaultPos;

    #endregion

    [HideInInspector] public Vector2 _currentPos;


    [HideInInspector] [Header("Current Drop")] public GameObject _drop;

    [Header("States")]
    [HideInInspector] public bool inDrop;
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

        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();

        _defaultPos = _rectTransform.anchoredPosition;

        UpdateCurrentPosition();

        _managerDragDrop = FindObjectOfType<M10A38ManagerDragDrop>();
        _managerDragDrop._drags.Add(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(mover)
        {
            _managerDragDrop._controlAudio.PlayAudio(0);

            inDrop = false;
            UpdateCurrentPosition();

            indexSibling = _rectTransform.GetSiblingIndex(); // Obtiene el index layout respecto a los demas objetos del mismo nivel
            var max = _rectTransform.parent.transform.childCount; // Obtiene el index max
            _rectTransform.SetSiblingIndex(max - 1); // Posiciona el objeto sobre todos los demas

            GetComponent<CanvasGroup>().blocksRaycasts = false;
            SetSpriteState(true);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(mover)
            _rectTransform.anchoredPosition += eventData.delta / _managerDragDrop.canvas.scaleFactor;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //print("EndDrag"+name);
         if(mover)
         {
            if (!swap)
            {
                if (!inDrop)
                {
                    //print(name+"return");
                    _rectTransform.anchoredPosition = _currentPos;
                    GetComponent<CanvasGroup>().blocksRaycasts = true;
                    SetSpriteState(false);
                }
                else
                {
                    //_group.x--;
                    //print(name+"Did drop");
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
                GetComponent<M10A38BehaviourDrag>().swap = false;
            }
         }
    }
    public void OnDrop(PointerEventData eventData)
    {
        //print("OnDrop"+name);
        if(mover)
        {
            GameObject other = eventData.pointerDrag; // Get object drag
            M10A38BehaviourDropGroup x = other.GetComponent<M10A38BehaviourDrag>()._group;
            other.GetComponent<M10A38BehaviourDrag>()._group = _group;
            if (x)
                _group = x;
            else
                _group = null;

            SwapCurrentPosition(other);
            SwapIndexRow(other);
            SwapDrop(other);
            SwapDrag(other);
            UpdateCurrentPosition();
            SetSpriteState(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(mover)
        {
            inDrop = false;
            swap = false;        
            if (_drop)
            {
                _group.x--;
                _drop.GetComponent<M10A38BehaviourDrop>()._drag = null;
                _drop = null; 
                GetComponent<RectTransform>().anchoredPosition = _defaultPos;
                UpdateCurrentPosition();
                StartCoroutine(_managerDragDrop.StateBtnValidar());
                _group = null;
            }       
       
        }

    }

    /// Actualiza la posicion default del drag 
    public void UpdateDefaultPosition() => _defaultPos = GetComponent<RectTransform>().anchoredPosition;
    /// Actualiza la posicion actual del drag 
    public void UpdateCurrentPosition() => _currentPos = GetComponent<RectTransform>().anchoredPosition;

    /// Intercambia la posicion actual entre los drag
    void SwapCurrentPosition(GameObject other)
    {
        if (_drop && other.GetComponent<M10A38BehaviourDrag>()._drop)
        {            
            other.GetComponent<M10A38BehaviourDrag>().swap = true; // Estado de intercambio 
            Vector2 a = other.GetComponent<M10A38BehaviourDrag>()._currentPos;
            other.GetComponent<RectTransform>().anchoredPosition = _currentPos;
            GetComponent<RectTransform>().anchoredPosition = a;
        }
        else if (_drop)
        {
            other.GetComponent<M10A38BehaviourDrag>().swap = true; // Estado de intercambio 
            //Vector2 a = other.GetComponent<M10A38BehaviourDrag>()._currentPos;
            other.GetComponent<RectTransform>().anchoredPosition = _currentPos;
            GetComponent<RectTransform>().anchoredPosition = _defaultPos;
        }
    }

    /// Intercambia la posicion default entre los drag 
    //public void SwapDefaultPosition(GameObject other)
    //{
    //    print("13");
    //    Vector2 x = other.GetComponent<M10A38BehaviourDrag>()._defaultPos;
    //    other.GetComponent<M10A38BehaviourDrag>()._defaultPos = _defaultPos;
    //    _defaultPos = x;
    //}

    ///Intercambia el objeto drag asociado a los drop
    void SwapDrag(GameObject other)
    {
        //print(other.name+"Sobre"+name);

        GameObject m = other.GetComponent<M10A38BehaviourDrag>()._drop;
        GameObject n = _drop;

        if (m)
            other.GetComponent<M10A38BehaviourDrag>()._drop.GetComponent<M10A38BehaviourDrop>()._drag = other;

        if (n)
            _drop.GetComponent<M10A38BehaviourDrop>()._drag = gameObject;

    }

    /// Intercambia el objeto drop entre los drag
    void SwapDrop(GameObject other)
    {
        if (_drop)
        {
            GameObject x = other.GetComponent<M10A38BehaviourDrag>()._drop;
            other.GetComponent<M10A38BehaviourDrag>()._drop = _drop;
            _drop = x;

            //SwapDefaultPosition(other);
        }
    }

    void SwapIndexRow(GameObject other)
    {
        if (_managerDragDrop._OperatingMethod == M10A38ManagerDragDrop.OperatingMethod.Math)
        {
            int x = other.GetComponent<M10A38BehaviourDrag>()._row;
            other.GetComponent<M10A38BehaviourDrag>()._row = this._row;
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
