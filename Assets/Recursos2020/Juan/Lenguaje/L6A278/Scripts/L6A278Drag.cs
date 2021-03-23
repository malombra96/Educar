using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class L6A278Drag : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler,IDropHandler,IPointerEnterHandler,IPointerExitHandler
{
    Canvas canvas;
    public bool inDrop;
    L6A278ManagerSeleccion managerSeleccion;
    [HideInInspector] public Vector2 posDefault;
    public GameObject dropRight;
    [HideInInspector] public GameObject drop;
    // Start is called before the first frame update
    void Awake()
    {
        drop = dropRight;

        managerSeleccion = FindObjectOfType<L6A278ManagerSeleccion>();
        managerSeleccion.drags.Add(this);

        posDefault = GetComponent<RectTransform>().anchoredPosition;
        canvas = FindObjectOfType<Canvas>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        GetComponent<RectTransform>().anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        managerSeleccion.controlAudio.PlayAudio(0);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        GetComponent<Image>().sprite = GetComponent<BehaviourSprite>()._selection;
        transform.SetAsLastSibling();
        inDrop = false;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        GetComponent<Image>().sprite = GetComponent<BehaviourSprite>()._default;
        if (!inDrop)
            GetComponent<RectTransform>().anchoredPosition = posDefault;
        else
            posDefault = GetComponent<RectTransform>().anchoredPosition;
    }
    public void OnDrop(PointerEventData eventData)
    {
        var _drag = eventData.pointerDrag;
        if (_drag.GetComponent<L6A278Drag>())
        {
            var temDrop = _drag.GetComponent<L6A278Drag>().drop;

            _drag.GetComponent<L6A278Drag>().inDrop = true;
            _drag.GetComponent<L6A278Drag>().drop = drop;
            drop.GetComponent<L6A278Drop>().drag = _drag.gameObject;
            _drag.GetComponent<RectTransform>().anchoredPosition = drop.GetComponent<RectTransform>().anchoredPosition;
            _drag.GetComponent<L6A278Drag>().posDefault = drop.GetComponent<RectTransform>().anchoredPosition;

            inDrop = true;
            drop = temDrop;
            temDrop.GetComponent<L6A278Drop>().drag = gameObject;
            GetComponent<RectTransform>().anchoredPosition = temDrop.GetComponent<RectTransform>().anchoredPosition;
            GetComponent<L6A278Drag>().posDefault = temDrop.GetComponent<RectTransform>().anchoredPosition;
        }
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
