using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class M6L105huevo : MonoBehaviour,IPointerDownHandler
{
    public Toggle toggle;   

    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponentInParent<Toggle>();
    }  

    public void OnPointerDown(PointerEventData eventData)
    {
        if(toggle.interactable)
           toggle.OnPointerClick(eventData);
    }   

}
