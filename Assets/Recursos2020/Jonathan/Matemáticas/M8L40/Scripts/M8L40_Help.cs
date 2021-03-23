using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class M8L40_Help : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public GameObject letrero;

    private void Awake()
    {
        letrero = GameObject.Find("letrero_info");
        letrero.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GetComponent<Button>().enabled)
        {
            letrero.SetActive(true);
        }
        else
        {
            letrero.SetActive(false);
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        letrero.SetActive(false);
    }
}
