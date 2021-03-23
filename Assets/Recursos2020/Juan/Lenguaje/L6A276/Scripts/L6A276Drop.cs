using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class L6A276Drop : MonoBehaviour,IDropHandler
{
    public L6A276ControlActividad controlActividad;
    public RectTransform padre;
    public GameObject dragCorrecto;
    [HideInInspector] public GameObject drag;
    int index;
    void Start()
    {
        controlActividad._drops.Add(this);
        index = dragCorrecto.transform.GetSiblingIndex();
    }
    public void OnDrop(PointerEventData eventData)
    {
        var _drag = eventData.pointerDrag;
        if (_drag.GetComponent<L6A276Drag>())
        {
            drag = _drag;
            drag.GetComponent<L6A276Drag>().drop = this;
            drag.GetComponent<L6A276Drag>().indrop = true;
            if (padre)
                drag.transform.SetParent(padre);
            drag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;            
        }
    }
    public void BuscarDragCorrecto() => dragCorrecto = controlActividad.transform.GetChild(index).gameObject;

}
