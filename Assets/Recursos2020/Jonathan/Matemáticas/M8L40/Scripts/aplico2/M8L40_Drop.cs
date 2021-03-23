using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class M8L40_Drop : MonoBehaviour,  IDropHandler
{
    public GameObject _dragactual;
    public GameObject _dragCorrect;
    [Header("Manager :")] public M8L40_Manager_aplico2 manager;
    public bool respuesta;


    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null )
        {
                 
            GameObject x = eventData.pointerDrag;
     
            x.transform.position = transform.position;
            _dragactual = x;
            x.GetComponent<M8L40_Drag>().stateDrop = true;
            
            manager.Ready();
                 
            if (_dragactual == _dragCorrect)
            {
                respuesta = true;
            }
            else
            {
                respuesta = false;
            }
        }
    }
}
