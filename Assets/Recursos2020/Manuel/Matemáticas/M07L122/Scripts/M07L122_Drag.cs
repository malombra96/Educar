using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class M07L122_Drag : MonoBehaviour, IBeginDragHandler,IDragHandler, IEndDragHandler, IPointerClickHandler
{
    ControlAudio _controlAudio;

    [HideInInspector]  public Vector2 _defaultPos; 
    [HideInInspector] public Vector2 _currentPos;
    [HideInInspector]  [Header("Current Drop")] public GameObject _drop;

    [Header("States")]
    [HideInInspector] public bool inDrop;
    [Header("Dragging on pivot")] public bool _pivotDraging;
   
    int indexSibling;
    RectTransform _rectTransform;
    Image _image;

    private void Awake()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();

        inDrop = false;

        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();

        _defaultPos = _rectTransform.anchoredPosition;
        
        UpdateCurrentPosition();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _controlAudio.PlayAudio(0);

        inDrop = false;
        UpdateCurrentPosition();
        
        indexSibling = _rectTransform.GetSiblingIndex(); // Obtiene el index layout respecto a los demas objetos del mismo nivel
        var max = _rectTransform.parent.transform.childCount; // Obtiene el index max
        _rectTransform.SetSiblingIndex(max-1); // Posiciona el objeto sobre todos los demas

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!_pivotDraging)
            _rectTransform.anchoredPosition += eventData.delta/FindObjectOfType<Canvas>().scaleFactor;
        else
        {
            float Y = Display.main.systemHeight / 2;
            float X = Display.main.systemWidth / 2;

            Vector2 newEventData = new Vector2((eventData.position.x - X), (eventData.position.y - Y)) / FindObjectOfType<Canvas>().scaleFactor;
            _rectTransform.anchoredPosition = newEventData;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!inDrop)
        {
            _rectTransform.anchoredPosition = _currentPos;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        else
            UpdateCurrentPosition();

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _controlAudio.PlayAudio(0);

        inDrop = false;

        if(_drop)
        {
            _drop.GetComponent<M07L122_Drop>()._drag = null;
            _drop = null;
        }
            
        GetComponent<RectTransform>().anchoredPosition = _defaultPos;
        UpdateCurrentPosition();

        GetComponent<RectTransform>().localEulerAngles = new Vector3(0,0,0);
        GetComponent<Image>().color = new Color32(255,255,255,255);

    }

    /// Actualiza la posicion default del drag 
    public void UpdateDefaultPosition() => _defaultPos = GetComponent<RectTransform>().anchoredPosition;
    /// Actualiza la posicion actual del drag 
    public void UpdateCurrentPosition() => _currentPos = GetComponent<RectTransform>().anchoredPosition;
    
}

