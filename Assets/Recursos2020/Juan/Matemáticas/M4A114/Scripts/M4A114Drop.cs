using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class M4A114Drop : MonoBehaviour,IDropHandler
{
    [HideInInspector] public GameObject _drag;
    public void OnDrop(PointerEventData eventData)
    {
       var _drag = eventData.pointerDrag;

        if (_drag.GetComponent<M4A114ObjetoMovible>())
        {
            if (!_drag.GetComponent<M4A114ObjetoMovible>().click)
            {
                _drag.GetComponent<M4A114ObjetoMovible>().indrop = true;
                _drag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            }
        }
    }

}
