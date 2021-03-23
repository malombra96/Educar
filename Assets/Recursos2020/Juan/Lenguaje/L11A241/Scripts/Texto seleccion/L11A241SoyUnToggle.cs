using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class L11A241SoyUnToggle : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    public L11A241Manager manager;
    public bool isOn;
    public bool interactuable;
    public bool correcto;
    Image image;
    public Color32 _defaul;
    public Color32 _ove;
    public Color32 _seleccion;
    public Color32 _correcto;
    public Color32 _incorrecto;

    // Start is called before the first frame update
    void Awake()
    {
        manager = FindObjectOfType<L11A241Manager>();
        manager.toggles.Add(this);

        interactuable = true;
        image = GetComponent<Image>();
        image.color = _defaul;
        if (transform.childCount > 0)
        {
            for (int x = 0; x < transform.childCount; x++)
                transform.GetChild(x).GetComponent<Image>().color = _defaul;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isOn && interactuable)
        {
            image.color = _ove;
            if (transform.childCount > 0)
            {
                for (int x = 0; x < transform.childCount; x++)
                    transform.GetChild(x).GetComponent<Image>().color = _ove;
            }
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isOn && interactuable)
        {
            image.color = _defaul;
            if (transform.childCount > 0)
            {
                for (int x = 0; x < transform.childCount; x++)
                    transform.GetChild(x).GetComponent<Image>().color = _defaul;
            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (interactuable)
        {
            manager.controlAudio.PlayAudio(0);
            if (!isOn)
            {
                isOn = true;
                image.color = _seleccion;
                if (transform.childCount > 0)
                {
                    for (int x = 0; x < transform.childCount; x++)
                        transform.GetChild(x).GetComponent<Image>().color = _seleccion;
                }
            }
            else
            {
                isOn = false;
                image.color = _defaul;
                if (transform.childCount > 0)
                {
                    for (int x = 0; x < transform.childCount; x++)
                        transform.GetChild(x).GetComponent<Image>().color = _defaul;
                }
            }
            manager.seleccion();
        }
    }
    public void estadoCalificar()
    {
        image.color = (correcto ? _correcto : _incorrecto);
        if (transform.childCount > 0)
        {
            for (int x = 0; x < transform.childCount; x++)
                transform.GetChild(x).GetComponent<Image>().color = (correcto ? _correcto : _incorrecto);
        }
    }
    public void estadoDefault()
    {
        image.color = _defaul;
        if (transform.childCount > 0)
        {
            for (int x = 0; x < transform.childCount; x++)
                transform.GetChild(x).GetComponent<Image>().color = _defaul;
        }
    }
}
