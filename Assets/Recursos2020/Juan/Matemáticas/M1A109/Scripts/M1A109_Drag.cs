using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class M1A109_Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    M1A109_Manager _managerDragDrop;

    #region DefaultState

    [HideInInspector] public Vector2 _defaultPos;

    #endregion

    [HideInInspector] public Vector2 _currentPos;

    [Tooltip("Arrastre aquí los drop correctos")] [Header("Correct Drop")] public List<M1A109_Drop> _DropRight;
    [HideInInspector] [Header("Current Drop")] public GameObject _drop;

    [Header("States")]
    [HideInInspector] public bool inDrop;
    [HideInInspector] public bool swap;    
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

        _managerDragDrop = FindObjectOfType<M1A109_Manager>();
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
            }
        }
        else
        {
            //print(name+"intercambio");
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            UpdateCurrentPosition();
            SetSpriteState(false);
            GetComponent<M1A109_Drag>().swap = false;
        }
        
    }   

    /// Actualiza la posicion default del drag 
    public void UpdateDefaultPosition() => _defaultPos = GetComponent<RectTransform>().anchoredPosition;
    /// Actualiza la posicion actual del drag 
    public void UpdateCurrentPosition() => _currentPos = GetComponent<RectTransform>().anchoredPosition;      

   
    void SetSpriteState(bool state)
    {
        GetComponent<Image>().sprite = state ?
            GetComponent<BehaviourSprite>()._selection :
            GetComponent<BehaviourSprite>()._default;

    }

}
