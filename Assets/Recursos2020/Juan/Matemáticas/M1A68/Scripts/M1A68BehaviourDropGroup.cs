using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M1A68BehaviourDropGroup : MonoBehaviour
{
    M1A68ManagerDragDrop _managerDragDrop;
    [Header("Right Drop Needed")] public int _needRight;
    [HideInInspector] [Header("Current Right Drop")] public int _currentRight;
    [HideInInspector] [Header("Current Wrong Drop")] public int _currentWrong;

    public void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        _managerDragDrop = FindObjectOfType<M1A68ManagerDragDrop>();
        _managerDragDrop._groups.Add(this);

    }
}
