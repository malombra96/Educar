using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M8A39_group : MonoBehaviour
{
    public M8A39_managerDrag _managerDragDrop;
    [Header("Right Drop Needed")] public int _needRight;
    [HideInInspector] [Header("Current Right Drop")] public int _currentRight;
    [HideInInspector] [Header("Current Wrong Drop")] public int _currentWrong;
}
