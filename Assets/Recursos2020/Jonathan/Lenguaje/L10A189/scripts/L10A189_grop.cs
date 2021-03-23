using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L10A189_grop : MonoBehaviour
{
    public L10A189_manager _managerDragDrop;
    [Header("Right Drop Needed")] public int _needRight;
    [HideInInspector] [Header("Current Right Drop")] public int _currentRight;
    [HideInInspector] [Header("Current Wrong Drop")] public int _currentWrong;

    public void Start()
    {
        _managerDragDrop = FindObjectOfType<L10A189_manager>();
        _managerDragDrop._groups.Add(this);

    }
}
