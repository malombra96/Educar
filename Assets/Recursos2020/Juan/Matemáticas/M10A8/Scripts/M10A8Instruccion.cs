using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class M10A8Instruccion : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private void Start()
    {
        transform.GetChild(0).GetComponent<Animator>().speed = 0;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = GetComponent<BehaviourSprite>()._selection;
        transform.GetChild(0).GetComponent<Animator>().speed = 1;
        transform.GetChild(0).GetComponent<Animator>().Play("Abrir Instruccion");        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = GetComponent<BehaviourSprite>()._default;
        transform.GetChild(0).GetComponent<Animator>().Play("Cerrar Instruccion");
    }
}
