﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class M10A8BehaviourDrag : MonoBehaviour, IBeginDragHandler,IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
{
    M10A8ManagerDragDrop _managerDragDrop;

    #region DefaultState

    [HideInInspector] public Vector2 _defaultPos;

    #endregion

    [HideInInspector] public Vector2 _currentPos;

    [Tooltip("Arrastre aquí los drop correctos")] [Header("Correct Drop")] public List<M10A8BehaviourDrop> _DropRight;
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

        _managerDragDrop = FindObjectOfType<M10A8ManagerDragDrop>();
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
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_pivotDraging)
            _rectTransform.anchoredPosition += eventData.delta / _managerDragDrop.canvas.scaleFactor;
        else
        {
            float Y = Display.main.systemHeight / 2;
            float X = Display.main.systemWidth / 2;

            Vector2 newEventData = new Vector2((eventData.position.x - X), (eventData.position.y - Y)) / _managerDragDrop.canvas.scaleFactor;
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

                if (ItsInfinite && _managerDragDrop._OperatingMethod == M10A8ManagerDragDrop.OperatingMethod.Create)
                    CreateDrag();
            }
        }
        else
        {
            //print(name+"intercambio");
            GetComponent<CanvasGroup>().blocksRaycasts = true;           
            UpdateCurrentPosition();
            SetSpriteState(false);
            GetComponent<M10A8BehaviourDrag>().swap = false;
        }

    }
    public void OnDrop(PointerEventData eventData)
    {
        //print("OnDrop"+name);

        GameObject other = eventData.pointerDrag; // Get object drag

        if (other.GetComponent<M10A8BehaviourDrag>() && _managerDragDrop._OperatingMethod != M10A8ManagerDragDrop.OperatingMethod.Create)
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
            _drop.GetComponent<M10A8BehaviourDrop>()._drag = null;
            _drop = null;
        }

        if (_managerDragDrop._OperatingMethod == M10A8ManagerDragDrop.OperatingMethod.Create)
        {
            if (_managerDragDrop._drags.Contains(this) && !ItsInfinite)
            {
                _managerDragDrop._drags.Remove(this);
                Destroy(this.gameObject, .3f);
            }
        }

        GetComponent<RectTransform>().anchoredPosition = _defaultPos;
        UpdateCurrentPosition();
        StartCoroutine(_managerDragDrop.StateBtnValidar());

    }

    /// Actualiza la posicion default del drag 
    public void UpdateDefaultPosition() => _defaultPos = GetComponent<RectTransform>().anchoredPosition;
    /// Actualiza la posicion actual del drag 
    public void UpdateCurrentPosition() => _currentPos = GetComponent<RectTransform>().anchoredPosition;

    /// Intercambia la posicion actual entre los drag
    void SwapCurrentPosition(GameObject other)
    {
        other.GetComponent<M10A8BehaviourDrag>().swap = true; // Estado de intercambio 

        Vector2 a = other.GetComponent<M10A8BehaviourDrag>()._currentPos;
        other.GetComponent<RectTransform>().anchoredPosition = _currentPos;

        if (other.GetComponent<M10A8BehaviourDrag>()._drop)            
            GetComponent<RectTransform>().anchoredPosition = a;
        else
            GetComponent<RectTransform>().anchoredPosition = _defaultPos;
    }

    /// Intercambia la posicion default entre los drag 
    //public void SwapDefaultPosition(GameObject other)
    //{
    //    //print(other.name+"SwapDefault"+name);

    //    Vector2 x = other.GetComponent<M10A8BehaviourDrag>()._defaultPos;
    //    other.GetComponent<M10A8BehaviourDrag>()._defaultPos = _defaultPos;
    //    _defaultPos = x;
    //}

    ///Intercambia el objeto drag asociado a los drop
    void SwapDrag(GameObject other)
    {
        //print(other.name+"Sobre"+name);

        GameObject m = other.GetComponent<M10A8BehaviourDrag>()._drop;
        GameObject n = _drop;

        if (m)
            other.GetComponent<M10A8BehaviourDrag>()._drop.GetComponent<M10A8BehaviourDrop>()._drag = other;

        if (n)
            _drop.GetComponent<M10A8BehaviourDrop>()._drag = gameObject;

    }

    /// Intercambia el objeto drop entre los drag
    void SwapDrop(GameObject other)
    {
        GameObject x = other.GetComponent<M10A8BehaviourDrag>()._drop;
        other.GetComponent<M10A8BehaviourDrag>()._drop = _drop;
        _drop = x;

        //SwapDefaultPosition(other);
    }

    void SwapIndexRow(GameObject other)
    {
        if (_managerDragDrop._OperatingMethod == M10A8ManagerDragDrop.OperatingMethod.Match)
        {
            int x = other.GetComponent<M10A8BehaviourDrag>()._row;
            other.GetComponent<M10A8BehaviourDrag>()._row = this._row;
            this._row = x;
        }

    }

    void SwapStateDrop(GameObject other)
    {
        bool x = other.GetComponent<M10A8BehaviourDrag>().inDrop;
        other.GetComponent<M10A8BehaviourDrag>().inDrop = this.inDrop;
        this.inDrop = x;
    }



    void CreateDrag()
    {
        GameObject aux = Instantiate(gameObject, _managerDragDrop.transform);
        aux.name = name;

        ItsInfinite = false;
        InicializatNewDrag(aux);

    }

    void InicializatNewDrag(GameObject aux)
    {
        aux.GetComponent<RectTransform>().anchoredPosition = _defaultPos;
        aux.GetComponent<M10A8BehaviourDrag>()._defaultPos = aux.GetComponent<RectTransform>().anchoredPosition;
        aux.GetComponent<M10A8BehaviourDrag>().inDrop = false;
        aux.GetComponent<M10A8BehaviourDrag>().swap = false;
        aux.GetComponent<M10A8BehaviourDrag>()._drop = null;
        aux.GetComponent<M10A8BehaviourDrag>().ItsInfinite = true;

        aux.transform.GetChild(0).gameObject.SetActive(false);
    }

    void SetSpriteState(bool state)
    {
        GetComponent<Image>().sprite = state ?
            GetComponent<BehaviourSprite>()._selection :
            GetComponent<BehaviourSprite>()._default;

    }
}
