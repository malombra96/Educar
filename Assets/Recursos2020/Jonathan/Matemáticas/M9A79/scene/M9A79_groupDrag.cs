using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M9A79_groupDrag : MonoBehaviour
{
    public M9A79_managerDragDrop _M9A79_managerDragDrop;
    [Header("Right Drop Needed")] public int _needRight;
    [HideInInspector] [Header("Current Right Drop")] public int _currentRight;
    [HideInInspector] [Header("Current Wrong Drop")] public int _currentWrong;

    public void Start()
    {
        _M9A79_managerDragDrop = FindObjectOfType<M9A79_managerDragDrop>();
        _M9A79_managerDragDrop._groups.Add(this);

    }
}
