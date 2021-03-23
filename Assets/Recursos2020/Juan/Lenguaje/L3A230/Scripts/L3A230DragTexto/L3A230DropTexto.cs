using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class L3A230DropTexto : MonoBehaviour,IDropHandler,IPointerClickHandler
{
    [HideInInspector]public Text texto;
    public string textoCorrecto;
    public bool correcto = false;
    L3A230ManagerDragDropTexto manager;
    // Start is called before the first frame update
    void Start()
    {
        texto = transform.GetChild(0).GetComponent<Text>();
        manager = FindObjectOfType<L3A230ManagerDragDropTexto>();
        manager.dropsTexto.Add(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        var drag = eventData.pointerDrag;
        if (drag.GetComponent<L3A230DragTexto>())
        {
            drag.GetComponent<L3A230DragTexto>().indrop = true;
            texto.text = drag.name.ToLower();

            if (texto.text == "s" || texto.text == "c")
                texto.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -1.5f);
            else if (texto.text == "z")
                texto.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -1.4f);

            correcto = (textoCorrecto == texto.text);            

            StartCoroutine(manager.activarValidar());
        }
        else
            drag = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        manager.controlAudio.PlayAudio(0);
        texto.text = "_";
        texto.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        if (correcto)
            correcto = false;
    }
}
