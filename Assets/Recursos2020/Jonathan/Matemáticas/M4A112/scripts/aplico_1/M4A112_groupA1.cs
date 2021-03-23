using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M4A112_groupA1 : MonoBehaviour
{
    public M4A112_managerDragA1 _managerDragDrop;
    [Header("Right Drop Needed")] public int _needRight;
    [HideInInspector] [Header("Current Right Drop")] public int _currentRight;
    [HideInInspector] [Header("Current Wrong Drop")] public int _currentWrong;

    public void Start()
    {
        _managerDragDrop = FindObjectOfType<M4A112_managerDragA1>();
        _managerDragDrop._groups.Add(this);

    }
}
