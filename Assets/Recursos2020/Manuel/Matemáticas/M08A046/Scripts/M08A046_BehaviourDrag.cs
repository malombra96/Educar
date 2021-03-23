using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class M08A046_BehaviourDrag : MonoBehaviour, IBeginDragHandler,IDragHandler, IEndDragHandler
{
    M08A046_ManagerDragDrop _managerDragDrop;

    #region DefaultState

    [HideInInspector]  public Vector2 _defaultPos;

    #endregion
    
    [HideInInspector] public Vector2 _currentPos;

    [Header("Create Setup")] public bool ItsInfinite;

    int indexSibling;
    RectTransform _rectTransform;
    Image _image;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();

        _defaultPos = _rectTransform.anchoredPosition;
        
        UpdateCurrentPosition();
        
        _managerDragDrop = FindObjectOfType<M08A046_ManagerDragDrop>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _managerDragDrop._controlAudio.PlayAudio(0);

        UpdateCurrentPosition();
        
        indexSibling = _rectTransform.GetSiblingIndex(); // Obtiene el index layout respecto a los demas objetos del mismo nivel
        var max = _rectTransform.parent.transform.childCount; // Obtiene el index max
        _rectTransform.SetSiblingIndex(max-1); // Posiciona el objeto sobre todos los demas

        GetComponent<CanvasGroup>().blocksRaycasts = false;
        SetSpriteState(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta/_managerDragDrop.canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition = _currentPos;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        SetSpriteState(false);
    }

    /// Actualiza la posicion default del drag 
    public void UpdateDefaultPosition() => _defaultPos = GetComponent<RectTransform>().anchoredPosition;
    /// Actualiza la posicion actual del drag 
    public void UpdateCurrentPosition() => _currentPos = GetComponent<RectTransform>().anchoredPosition;
    void SetSpriteState(bool state)
    {
        GetComponent<Image>().sprite = state? 
            GetComponent<BehaviourSprite>()._selection : 
            GetComponent<BehaviourSprite>()._default;
       
    }
}
