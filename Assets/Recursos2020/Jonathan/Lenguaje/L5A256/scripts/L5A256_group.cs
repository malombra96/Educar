using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L5A256_group : MonoBehaviour
{
    public L5A256_managerDrag _managerDragDrop;
    [Header("Right Drop Needed")] public int _needRight;
    [HideInInspector] [Header("Current Right Drop")] public int _currentRight;
    [HideInInspector] [Header("Current Wrong Drop")] public int _currentWrong;

    public void Start()
    {
        _managerDragDrop = FindObjectOfType<L5A256_managerDrag>();
        _managerDragDrop._groups.Add(this);

    }
}
