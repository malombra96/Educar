using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class M6L116Drop : MonoBehaviour,IDropHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        var drag = eventData.pointerDrag;
        if (drag.GetComponent<M6L116Drag>())
        {            
            drag.GetComponent<M6L116Drag>().inDrop = true;
            drag.GetComponent<M6L116Drag>().drop = gameObject;

            if (drag.GetComponent<M6L116Drag>().posAlSoltar)
                drag.GetComponent<RectTransform>().anchoredPosition += eventData.delta / drag.GetComponent<M6L116Drag>().canvas.scaleFactor;
            else
                drag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            drag.GetComponent<M6L116Drag>().posFinal = drag.GetComponent<RectTransform>().anchoredPosition;            
        }
    }
}
