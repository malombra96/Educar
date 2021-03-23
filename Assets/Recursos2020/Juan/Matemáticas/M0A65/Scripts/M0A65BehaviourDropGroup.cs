using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M0A65BehaviourDropGroup : MonoBehaviour
{  
    M0A65ManagerDragDrop _managerDragDrop;
    public Text text;
    [HideInInspector] public int j;
    public GameObject drop;
    [Header("Right Drop Needed")] public int _needRight;
    /*[HideInInspector]*/ [Header("Current Right Drop")] public int _currentRight;
    [HideInInspector] [Header("Current Wrong Drop")] public int _currentWrong;

    public void Start()
    {
        text.text = _needRight.ToString();
        _managerDragDrop = FindObjectOfType<M0A65ManagerDragDrop>();
        _managerDragDrop._groups.Add(this);
    }

    public void acomodar()
    {
       drop = transform.GetChild(j).gameObject;              
       j++;       
    }

    public void reAcomodar(GameObject x)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            drop = transform.GetChild(i).gameObject;
            if (drop == x && transform.GetChild(i+1).gameObject.GetComponent<M0A65BehaviourDrop>()._drag == null)
            {   
                print(j);    
                drop.GetComponent<M0A65BehaviourDrop>()._drag.GetComponent<M0A65BehaviourDrag>().mover = true;                
                j--;
                drop.GetComponent<M0A65BehaviourDrop>()._drag = null;
                if(i > 0)                       
                    transform.GetChild(i-1).gameObject.GetComponent<M0A65BehaviourDrop>()._drag.GetComponent<Image>().raycastTarget=true;
                
                break;
            }
        }
    }


    public void bloquear(GameObject x)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            drop = transform.GetChild(i).gameObject;
            if (drop == x && i > 0)
            {                
                if(transform.GetChild(i-1).gameObject.GetComponent<M0A65BehaviourDrop>()._drag)
                     transform.GetChild(i-1).gameObject.GetComponent<M0A65BehaviourDrop>()._drag.GetComponent<Image>().raycastTarget=false;
                                                 
                break;
            }
        }
    }
}
