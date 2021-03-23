using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class M8A127_drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
{
    M8A127_managerDrag _M8A127_managerDrag;

    #region DefaultState

    [HideInInspector] public Vector2 _defaultPos;

    #endregion

    [HideInInspector] public Vector2 _currentPos;

    [Tooltip("Arrastre aquí los drop correctos")] [Header("Correct Drop")] public List<M8A127_Drop> _DropRight;
    [HideInInspector] [Header("Current Drop")] public GameObject _drop;

    [Header("States")]
    [HideInInspector] public bool inDrop;
    [HideInInspector] public bool swap;

    [Header("Create Setup")] public bool ItsInfinite;
    Vector2 _createPos;

    [Header("Math Setup")]
    [Tooltip("Ingrese el index correspondiente a la fila o columna")] public int _row;


    [Header("Dragging on pivot")] public bool _pivotDraging;

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

        _M8A127_managerDrag = FindObjectOfType<M8A127_managerDrag>();
        _M8A127_managerDrag._drags.Add(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _M8A127_managerDrag._controlAudio.PlayAudio(0);

        inDrop = false;
        UpdateCurrentPosition();

        indexSibling = _rectTransform.GetSiblingIndex(); // Obtiene el index layout respecto a los demas objetos del mismo nivel
        var max = _rectTransform.parent.transform.childCount; // Obtiene el index max
        _rectTransform.SetSiblingIndex(max - 1); // Posiciona el objeto sobre todos los demas

        GetComponent<CanvasGroup>().blocksRaycasts = false;
        SetSpriteState(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_pivotDraging)
            _rectTransform.anchoredPosition += eventData.delta / _M8A127_managerDrag.canvas.scaleFactor;
        else
        {
            float Y = Display.main.systemHeight / 2;
            float X = Display.main.systemWidth / 2;

            Vector2 newEventData = new Vector2((eventData.position.x - X), (eventData.position.y - Y)) / _M8A127_managerDrag.canvas.scaleFactor;
            _rectTransform.anchoredPosition = newEventData;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //print("EndDrag"+name);

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
                //print(name+"Did drop");
                //inDrop = false;
                UpdateCurrentPosition();
                SetSpriteState(false);

                if (ItsInfinite && _M8A127_managerDrag._OperatingMethod == M8A127_managerDrag.OperatingMethod.Create)
                    CreateDrag();
            }
        }
        else
        {
            //print(name+"intercambio");
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            UpdateCurrentPosition();
            SetSpriteState(false);
            GetComponent<M8A127_drag>().swap = false;
        }

    }
    public void OnDrop(PointerEventData eventData)
    {
        //print("OnDrop"+name);

        GameObject other = eventData.pointerDrag; // Get object drag

        if (other.GetComponent<M8A127_drag>() && _M8A127_managerDrag._OperatingMethod != M8A127_managerDrag.OperatingMethod.Create)
        {
            SwapCurrentPosition(other);
            SwapIndexRow(other);
            SwapStateDrop(other);
            SwapDrop(other);
            SwapDrag(other);
            UpdateCurrentPosition();
            SetSpriteState(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        inDrop = false;
        swap = false;

        if (_drop)
        {
            _drop.GetComponent<M8A127_Drop>()._drag = null;
            _drop = null;
        }

        if (_M8A127_managerDrag._OperatingMethod == M8A127_managerDrag.OperatingMethod.Create)
        {
            if (_M8A127_managerDrag._drags.Contains(this) && !ItsInfinite)
            {
                _M8A127_managerDrag._drags.Remove(this);
                Destroy(this.gameObject, .3f);
            }
        }

        GetComponent<RectTransform>().anchoredPosition = _defaultPos;
        UpdateCurrentPosition();
        StartCoroutine(_M8A127_managerDrag.StateBtnValidar());

    }

    /// Actualiza la posicion default del drag 
    public void UpdateDefaultPosition() => _defaultPos = GetComponent<RectTransform>().anchoredPosition;
    /// Actualiza la posicion actual del drag 
    public void UpdateCurrentPosition() => _currentPos = GetComponent<RectTransform>().anchoredPosition;

    /// Intercambia la posicion actual entre los drag
    void SwapCurrentPosition(GameObject other)
    {
        other.GetComponent<M8A127_drag>().swap = true; // Estado de intercambio 
        Vector2 a = other.GetComponent<M8A127_drag>()._currentPos;
        other.GetComponent<RectTransform>().anchoredPosition = _currentPos;
        GetComponent<RectTransform>().anchoredPosition = a;
    }

    /// Intercambia la posicion default entre los drag 
    public void SwapDefaultPosition(GameObject other)
    {
        //print(other.name+"SwapDefault"+name);

        Vector2 x = other.GetComponent<M8A127_drag>()._defaultPos;
        other.GetComponent<M8A127_drag>()._defaultPos = _defaultPos;
        _defaultPos = x;
    }

    ///Intercambia el objeto drag asociado a los drop
    void SwapDrag(GameObject other)
    {
        //print(other.name+"Sobre"+name);

        GameObject m = other.GetComponent<M8A127_drag>()._drop;
        GameObject n = _drop;

        if (m)
            other.GetComponent<M8A127_drag>()._drop.GetComponent<M8A127_Drop>()._drag = other;

        if (n)
            _drop.GetComponent<M8A127_Drop>()._drag = gameObject;

    }

    /// Intercambia el objeto drop entre los drag
    void SwapDrop(GameObject other)
    {
        GameObject x = other.GetComponent<M8A127_drag>()._drop;
        other.GetComponent<M8A127_drag>()._drop = _drop;
        _drop = x;

        SwapDefaultPosition(other);
    }

    void SwapIndexRow(GameObject other)
    {
        if (_M8A127_managerDrag._OperatingMethod == M8A127_managerDrag.OperatingMethod.Match)
        {
            int x = other.GetComponent<M8A127_drag>()._row;
            other.GetComponent<M8A127_drag>()._row = this._row;
            this._row = x;
        }

    }

    void SwapStateDrop(GameObject other)
    {
        bool x = other.GetComponent<M8A127_drag>().inDrop;
        other.GetComponent<M8A127_drag>().inDrop = this.inDrop;
        this.inDrop = x;
    }



    void CreateDrag()
    {
        GameObject aux = Instantiate(gameObject, _M8A127_managerDrag.transform);
        aux.name = name;

        ItsInfinite = false;
        InicializatNewDrag(aux);

    }

    void InicializatNewDrag(GameObject aux)
    {
        aux.GetComponent<RectTransform>().anchoredPosition = _defaultPos;
        aux.GetComponent<M8A127_drag>()._defaultPos = aux.GetComponent<RectTransform>().anchoredPosition;
        aux.GetComponent<M8A127_drag>().inDrop = false;
        aux.GetComponent<M8A127_drag>().swap = false;
        aux.GetComponent<M8A127_drag>()._drop = null;
        aux.GetComponent<M8A127_drag>().ItsInfinite = true;

        aux.transform.GetChild(0).gameObject.SetActive(false);
    }

    void SetSpriteState(bool state)
    {
        GetComponent<Image>().sprite = state ?
            GetComponent<BehaviourSprite>()._selection :
            GetComponent<BehaviourSprite>()._default;

    }

}
