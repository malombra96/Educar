using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1A66BehaviourDropGroup : MonoBehaviour
{
     M1A66ManagerDragDrop _managerDragDrop;
    [Header("Right Drop Needed")] public int _needRight;
    [HideInInspector] [Header("Current Right Drop")] public int _currentRight; 
    [HideInInspector] [Header("Current Wrong Drop")] public int _currentWrong;

    public void Start()
    {
        _managerDragDrop = FindObjectOfType<M1A66ManagerDragDrop>();
        _managerDragDrop._groups.Add(this);

    }
}
