using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class M08L029_DragBotle : MonoBehaviour,IBeginDragHandler,IDragHandler, IEndDragHandler
{
    [Header("Canvas")] public Canvas canvas;

    [HideInInspector]  public Vector2 _defaultPos;
    
    [HideInInspector] public Vector2 _currentPos;

    [Header("States")]
    [HideInInspector]  public bool inDrop;
    
    int indexSibling;
    RectTransform _rectTransform;
    Image _image;

    private void Awake()
    {
        inDrop = false;

        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();

        _defaultPos = _rectTransform.anchoredPosition;
        
        UpdateCurrentPosition();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        inDrop = false;
        UpdateCurrentPosition();
        
        indexSibling = _rectTransform.GetSiblingIndex(); // Obtiene el index layout respecto a los demas objetos del mismo nivel
        var max = _rectTransform.parent.transform.childCount; // Obtiene el index max
        _rectTransform.SetSiblingIndex(max-1); // Posiciona el objeto sobre todos los demas

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta/canvas.scaleFactor;

        GameObject drop = eventData.pointerEnter;

        if(drop.GetComponent<M08L029_DropBox>())
            drop.GetComponent<M08L029_DropBox>().PaintBox(name);
            
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition = _currentPos;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    /// Actualiza la posicion actual del drag 
    public void UpdateCurrentPosition() => _currentPos = GetComponent<RectTransform>().anchoredPosition;
}
