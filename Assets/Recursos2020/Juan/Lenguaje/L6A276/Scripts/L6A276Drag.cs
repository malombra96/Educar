using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class L6A276Drag : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler,IDropHandler,IPointerEnterHandler,IPointerExitHandler
{
    public L6A276ControlActividad controlActividad;
    Canvas canvas;
    public bool indrop;
    [HideInInspector] public L6A276Drop drop;

    //[HideInInspector] public GameObject drop;    
    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();        
    }

    public void OnDrag(PointerEventData eventData)
    {           
        GetComponent<RectTransform>().anchoredPosition += eventData.delta / canvas.scaleFactor;        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        controlActividad.controlAudio.PlayAudio(0);
        if (!indrop)
        {
            var inst = Instantiate(gameObject, controlActividad.GetComponent<RectTransform>());
            inst.transform.SetSiblingIndex(transform.GetSiblingIndex());
            inst.GetComponent<Image>().sprite = inst.GetComponent<BehaviourSprite>()._default;
            inst.name = name;
        }
        else
        {
            drop.drag = null;
            drop = null;
            indrop = false;
        }
        transform.SetAsLastSibling();
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        GetComponent<Image>().sprite = GetComponent<BehaviourSprite>()._selection;

    }
   //void crearDrag(GameObject nuevo drag)
    public void OnEndDrag(PointerEventData eventData)
    {
        controlActividad.activarValidar();
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        GetComponent<Image>().sprite = GetComponent<BehaviourSprite>()._default;

        if (!indrop)
            Destroy(gameObject);        
    }
    public void OnDrop(PointerEventData eventData)
    {
        var drag = eventData.pointerDrag;
        if (drag.GetComponent<L6A276Drag>())
        {
            if (indrop)
            {
                if (drop.padre)
                    drag.transform.SetParent(drop.padre);

                drag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                drop.drag = drag;
                drag.GetComponent<L6A276Drag>().drop = drop;
                drag.GetComponent<L6A276Drag>().indrop = true;
                Destroy(gameObject);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        controlActividad.controlAudio.PlayAudio(0);
        if (indrop)
        {
            //print("hola");
            drop.drag = null;
            controlActividad.activarValidar();
            Destroy(gameObject);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!indrop && GetComponent<CanvasGroup>().blocksRaycasts)
            GetComponent<Image>().sprite = GetComponent<BehaviourSprite>()._disabled;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!indrop && GetComponent<CanvasGroup>().blocksRaycasts)
            GetComponent<Image>().sprite = GetComponent<BehaviourSprite>()._default;
    }
}
