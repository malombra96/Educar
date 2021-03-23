using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class L3A227Drag : MonoBehaviour,IDragHandler,IEndDragHandler,IBeginDragHandler,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler,IDropHandler
{
    Canvas canvas;
    L3A227Manager manager;

    public GameObject dropCorrecto;
    /*[HideInInspector] */public GameObject _drop;
    /*[HideInInspector] */public GameObject lanzador;

    public RectTransform target;
    [HideInInspector] public Vector2 posDefault, posActual;    
    [HideInInspector] public int x;
    [HideInInspector] public int i;
    // Start is called before the first frame update
    void Start()
    {
        char[] c = name.ToCharArray();       
        i =  c[6];

        canvas = FindObjectOfType<Canvas>();
        posDefault = GetComponent<RectTransform>().anchoredPosition;
        manager = FindObjectOfType<L3A227Manager>();        
    }

    void Update()
    {
        if (target && _drop)
            GetComponent<RectTransform>().anchoredPosition = Vector3.MoveTowards(GetComponent<RectTransform>().anchoredPosition, target.anchoredPosition, 5f);

        if (target)
        {
            if (Vector2.Distance(GetComponent<RectTransform>().anchoredPosition, target.anchoredPosition) < 0.1f)
            {
                _drop = target.gameObject;
                _drop.GetComponent<L3A227UltimaPos>().tengoDrag = true;
                posActual = target.anchoredPosition;                
                target = null;
                StartCoroutine(manager.calificar());
            }               
        }

    }
    public void OnDrag(PointerEventData eventData)
    {
        GetComponent<RectTransform>().anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        manager.controlAudio.PlayAudio(0);
        GetComponent<Image>().sprite = GetComponent<BehaviourSprite>()._selection;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        transform.SetAsLastSibling();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        GetComponent<Image>().sprite = GetComponent<BehaviourSprite>()._default;

        if (!_drop)
            GetComponent<RectTransform>().anchoredPosition = posDefault;
        else
            GetComponent<RectTransform>().anchoredPosition = posActual;
    }
    public void OnDrop(PointerEventData eventData)
    {
        var _drag = eventData.pointerDrag;
        if (_drag.GetComponent<L3A227Drag>() && _drop)
        {
            manager._drop.drag = _drag.GetComponent<L3A227Drag>();
            _drag.GetComponent<L3A227Drag>()._drop = _drop;
            _drag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            _drag.GetComponent<L3A227Drag>().posActual = GetComponent<RectTransform>().anchoredPosition;

            _drop = null;
            lanzador = null;            
            posActual = posDefault;
            GetComponent<RectTransform>().anchoredPosition = posDefault;
        }        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        manager.controlAudio.PlayAudio(0);        
        target = null;
        
        if (_drop)
        {
            manager._drop.x--;
            manager._drop.drag = null;
            manager.botonLanzar.interactable = false;                        
            _drop = null;
        }
        if (lanzador)
        {
            lanzador.GetComponent<L3A227Drop>().drag = null;
            lanzador = null;
        }
        GetComponent<RectTransform>().anchoredPosition = posDefault;                
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GetComponent<CanvasGroup>().blocksRaycasts)
            GetComponent<Image>().sprite = GetComponent<BehaviourSprite>()._disabled;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (GetComponent<CanvasGroup>().blocksRaycasts)
            GetComponent<Image>().sprite = GetComponent<BehaviourSprite>()._default;
    }
    
}
