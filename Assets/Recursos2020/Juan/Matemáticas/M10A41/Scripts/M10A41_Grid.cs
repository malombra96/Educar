using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class M10A41_Grid : MonoBehaviour, IPointerClickHandler 
{
    [Header("Mira :")] public GameObject mira;
    
    [Header("Right :")] public bool Right;
    
    [Header("Manager :")] public M10A41_Manager_Aplico_3 manager;
        

    public void OnPointerClick(PointerEventData eventData)
    {
        if (mira.GetComponent<M10A41_Mira>().MoverAlClick)
        {
            mira.GetComponent<M10A41_Mira>().Mover = false;
            mira.GetComponent<M10A41_Mira>().posicionOpcion(gameObject.transform);            
            manager.Ready(this);
        }
    }
}
