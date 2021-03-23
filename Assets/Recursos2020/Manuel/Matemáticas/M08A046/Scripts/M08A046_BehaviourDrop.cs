using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class M08A046_BehaviourDrop : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    M08A046_ManagerDragDrop _managerDragDrop;

    private void Awake()
    {
        _managerDragDrop = FindObjectOfType<M08A046_ManagerDragDrop>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject x = eventData.pointerDrag;
        
        if (x != null && x.GetComponent<M08A046_BehaviourDrag>())
        {
            if( _managerDragDrop.countDrop < (_managerDragDrop._cubes.Count-1))
            {
                _managerDragDrop.countDrop++;
                _managerDragDrop.SetStateCube();
            }
                
        }
                
        else
            x = null;

        _managerDragDrop.SetValidar(); 
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(_managerDragDrop.countDrop >= 0 && (_managerDragDrop.countDrop != (_managerDragDrop._cubes.Count - 1)))
        {
            _managerDragDrop.countDrop--;
            _managerDragDrop.SetStateCube();
        }
            


        _managerDragDrop.SetValidar(); 

    }
}
