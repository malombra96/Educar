using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class M6L116Drag : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler,IDropHandler
{
    M6L116Manager manager;
    [HideInInspector] public Canvas canvas;
    [HideInInspector] public Transform padre;
    [HideInInspector] public Vector2 posDefault;
    [HideInInspector] public Vector2 posFinal;

    public GameObject dropCorrecto;
    /*[HideInInspector] */public GameObject drop;
    [HideInInspector] public bool inDrop;
    public bool cambioTransform;
    public bool posAlSoltar;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<M6L116Manager>();
        canvas = FindObjectOfType<Canvas>();
        posDefault = GetComponent<RectTransform>().anchoredPosition;
        manager.Drags.Add(this);

        if (cambioTransform)
            padre = manager.transform.GetChild(2).GetChild(0).GetComponent<Transform>();        
    }

    public void OnDrag(PointerEventData eventData)
    {
        GetComponent<RectTransform>().anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        manager.controlAudio.PlayAudio(0);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        GetComponent<Image>().sprite = GetComponent<BehaviourSprite>()._selection;

        if (cambioTransform)
            transform.SetParent(manager.transform);
        transform.SetAsLastSibling();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        GetComponent<Image>().sprite = GetComponent<BehaviourSprite>()._default;
        if (!inDrop)
        {
            if (cambioTransform)
                transform.SetParent(padre);
            GetComponent<RectTransform>().anchoredPosition = posDefault;
        }
        else if (drop)
            GetComponent<RectTransform>().anchoredPosition = posFinal;

        StartCoroutine(manager.activarValidar());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        manager.controlAudio.PlayAudio(0);
        inDrop = false;
        posFinal = Vector3.zero;
        drop = null;

        if (cambioTransform)
            transform.SetParent(padre);
        GetComponent<RectTransform>().anchoredPosition = posDefault;
        StartCoroutine(manager.activarValidar());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(GetComponent<CanvasGroup>().blocksRaycasts)
            GetComponent<Image>().sprite = GetComponent<BehaviourSprite>()._disabled;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (GetComponent<CanvasGroup>().blocksRaycasts)
            GetComponent<Image>().sprite = GetComponent<BehaviourSprite>()._default;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var other = eventData.pointerDrag;
        if (other.GetComponent<M6L116Drag>())
        {            
            if (other.GetComponent<M6L116Drag>().inDrop && inDrop) 
            {
                Vector2 posTemp = posFinal;
                GameObject tempDrop = drop;

                GetComponent<RectTransform>().anchoredPosition = other.GetComponent<M6L116Drag>().posFinal;
                posFinal = GetComponent<RectTransform>().anchoredPosition;
                other.GetComponent<RectTransform>().anchoredPosition = posTemp;
                other.GetComponent<M6L116Drag>().posFinal = posTemp;

                drop = other.GetComponent<M6L116Drag>().drop;
                other.GetComponent<M6L116Drag>().drop = tempDrop;                
            }
            else if (inDrop)
            {
                other.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                other.GetComponent<M6L116Drag>().drop = drop;

                if (cambioTransform)
                    transform.SetParent(padre);
                
                GetComponent<RectTransform>().anchoredPosition = posDefault;
                inDrop = false;
                drop = null;
            }
            StartCoroutine(manager.activarValidar());
        }
    }
}
