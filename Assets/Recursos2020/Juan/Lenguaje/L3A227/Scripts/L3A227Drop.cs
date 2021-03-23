using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class L3A227Drop : MonoBehaviour,IDropHandler
{
    L3A227Manager manager;
    public L3A227Drag drag;
    public List<RectTransform> espacios;    
    [HideInInspector] public int x;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<L3A227Manager>();
        manager._drop = this;
    }
    private void Update()
    {
        manager.botonLanzar.interactable = drag;
    }
    public void OnDrop(PointerEventData eventData)
    {
        var _drag = eventData.pointerDrag;
        if (_drag.GetComponent<L3A227Drag>() && x < 2)
        {
            drag = _drag.GetComponent<L3A227Drag>();
            drag.lanzador = gameObject;
            drag._drop = gameObject;
            drag.posActual = GetComponent<RectTransform>().anchoredPosition;                     
            _drag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;            
            x++;
        }
    }
}
