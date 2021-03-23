using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]

public class M8A39_drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
{

    public M8A39_managerDrag _managerDragDrop;

    #region DefaultState

    [HideInInspector] public Vector2 _defaultPos;

    #endregion

    [HideInInspector] public Vector2 _currentPos;

    [Tooltip("Arrastre aquí los drop correctos")] [Header("Correct Drop")] public List<M8A39_drop> _DropRight;
    [Header("Current Drop")] public GameObject _drop;

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
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _managerDragDrop.canvas.scaleFactor;

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
                _rectTransform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                //print(name+"Did drop");
                inDrop = false;
                UpdateCurrentPosition();
                SetSpriteState(false);
                _rectTransform.localScale = _drop.GetComponent<RectTransform>().localScale;
            }
        }
        else
        {
            //print(name+"intercambio");
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            UpdateCurrentPosition();
            SetSpriteState(false);
            GetComponent<M8A39_drag>().swap = false;
        }

    }
    public void OnDrop(PointerEventData eventData)
    {
        //print("OnDrop"+name);

        //GameObject other = eventData.pointerDrag; // Get object drag

        //SwapCurrentPosition(other);
        //SwapIndexRow(other);
        //SwapDrop(other);
        //SwapDrag(other);
        //UpdateCurrentPosition();
        //SetSpriteState(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        inDrop = false;
        swap = false;

        if (_drop)
        {
            _drop.GetComponent<M8A39_drop>()._drag = null;
            _drop = null;
        }

        GetComponent<RectTransform>().anchoredPosition = _defaultPos;
        _rectTransform.localScale = new Vector3(1, 1, 1);
        UpdateCurrentPosition();
        StartCoroutine(_managerDragDrop.StateBtnValidar());

    }

    /// Actualiza la posicion default del drag 
    public void UpdateDefaultPosition()
    {

        _defaultPos = GetComponent<RectTransform>().anchoredPosition;

    }
    /// Actualiza la posicion actual del drag 
    public void UpdateCurrentPosition() => _currentPos = GetComponent<RectTransform>().anchoredPosition;

    /// Intercambia la posicion actual entre los drag
    void SwapCurrentPosition(GameObject other)
    {
        other.GetComponent<M8A39_drag>().swap = true; // Estado de intercambio 
        Vector2 a = other.GetComponent<BehaviourDrag>()._currentPos;
        other.GetComponent<RectTransform>().anchoredPosition = _currentPos;
        GetComponent<RectTransform>().anchoredPosition = a;
    }

    /// Intercambia la posicion default entre los drag 
    public void SwapDefaultPosition(GameObject other)
    {
        //print(other.name+"SwapDefault"+name);

        Vector2 x = other.GetComponent<M8A39_drag>()._defaultPos;
        other.GetComponent<M8A39_drag>()._defaultPos = _defaultPos;
        _defaultPos = x;
    }

    ///Intercambia el objeto drag asociado a los drop
    void SwapDrag(GameObject other)
    {
        //print(other.name+"Sobre"+name);

        GameObject m = other.GetComponent<M8A39_drag>()._drop;
        GameObject n = _drop;

        if (m)
            other.GetComponent<M8A39_drag>()._drop.GetComponent<M8A39_drop>()._drag = other;

        if (n)
            _drop.GetComponent<M8A39_drop>()._drag = gameObject;

    }

    /// Intercambia el objeto drop entre los drag
    void SwapDrop(GameObject other)
    {
        GameObject x = other.GetComponent<M8A39_drag>()._drop;
        other.GetComponent<M8A39_drag>()._drop = _drop;
        _drop = x;

        SwapDefaultPosition(other);
    }

    void SwapIndexRow(GameObject other)
    {
        if (_managerDragDrop._OperatingMethod == M8A39_managerDrag.OperatingMethod.Math)
        {
            int x = other.GetComponent<M8A39_drag>()._row;
            other.GetComponent<M8A39_drag>()._row = this._row;
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


