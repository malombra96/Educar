using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L4A238_groupDrag : MonoBehaviour
{
    public L4A238_managerDrag _managerDragDrop;
    [Header("Right Drop Needed")] public int _needRight;
    [HideInInspector] [Header("Current Right Drop")] public int _currentRight;
    [HideInInspector] [Header("Current Wrong Drop")] public int _currentWrong;

    public void Start()
    {
        _managerDragDrop = FindObjectOfType<L4A238_managerDrag>();
        _managerDragDrop._groups.Add(this);

    }
}
