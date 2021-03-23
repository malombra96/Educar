using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BehaviourDrawLine : MonoBehaviour
{
    LineRenderer _lineRenderer;

    //[Header("State Selection")] public bool _state;

    [Header("Setup Colors")] 
    public Color32 _default;
    //public Color32 _select;
    public Color32 _right;
    public Color32 _wrong;

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.material.color = _default;
    }

}
