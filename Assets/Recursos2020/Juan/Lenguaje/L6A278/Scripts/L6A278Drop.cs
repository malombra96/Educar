using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class L6A278Drop : MonoBehaviour, IDropHandler
{
    L6A278ManagerSeleccion managerSeleccion;
    public GameObject drag;
    // Start is called before the first frame update
    void Awake()
    {
        managerSeleccion = FindObjectOfType<L6A278ManagerSeleccion>();
        managerSeleccion.drops.Add(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        var _drag = eventData.pointerDrag;
        if (drag.GetComponent<L6A278Drag>())
        {
            var tempDrop = _drag.GetComponent<L6A278Drag>().drop;            

            _drag.GetComponent<L6A278Drag>().inDrop = true;
            _drag.GetComponent<L6A278Drag>().drop = gameObject;
            _drag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            _drag.GetComponent<L6A278Drag>().posDefault = GetComponent<RectTransform>().anchoredPosition;

            drag.GetComponent<L6A278Drag>().inDrop = true;
            drag.GetComponent<L6A278Drag>().drop = tempDrop;
            drag.GetComponent<RectTransform>().anchoredPosition = tempDrop.GetComponent<RectTransform>().anchoredPosition;
            drag.GetComponent<L6A278Drag>().posDefault = tempDrop.GetComponent<RectTransform>().anchoredPosition;

            tempDrop.GetComponent<L6A278Drop>().drag = drag;
            drag = _drag;
        }
    }
}
