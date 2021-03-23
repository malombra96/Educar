using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class M1A66BehaviourDrop : MonoBehaviour, IDropHandler
{
   [HideInInspector]  [Header("Drag-IN")] public GameObject _drag;
    [Header("Grupo")] public M1A66BehaviourDropGroup _group;
    M1A66ManagerDragDrop _managerDragDrop;

    [Header("Math Setup")]
    [Tooltip("Ingrese el index correspondiente a la fila o columna")] public int _row;

    private void Awake()
    {
        _managerDragDrop = FindObjectOfType<M1A66ManagerDragDrop>();
        _managerDragDrop._drops.Add(this);        
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject x = eventData.pointerDrag;
        
        if (x != null && _drag == null)
        {
            _drag = eventData.pointerDrag;
            UpdateSlotDrop();
            _drag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            _drag.GetComponent<CanvasGroup>().blocksRaycasts = true;
            _drag.GetComponent<Image>().sprite = _drag.GetComponent<BehaviourSprite>()._default;
            _drag.GetComponent<M1A66BehaviourDrag>().inDrop = true;
            UpdateRowDrag(_drag);
            GetCalificationType();
        }
        
    }
    
    /// <summary>
    /// Relaciona el drop (this) con el elemento que hace drag
    /// </summary>
    void UpdateSlotDrop()
    {
        if (_drag.GetComponent<M1A66BehaviourDrag>()._drop == null)
        {
            _drag.GetComponent<M1A66BehaviourDrag>()._drop = gameObject;
        }
        else
        {
            var previousDrop = _drag.GetComponent<M1A66BehaviourDrag>()._drop.GetComponent<M1A66BehaviourDrop>();
            previousDrop._drag = null;
                
            _drag.GetComponent<M1A66BehaviourDrag>()._drop = gameObject;
        }
    }

    void UpdateRowDrag(GameObject other)
    {
        if(_managerDragDrop._OperatingMethod == M1A66ManagerDragDrop.OperatingMethod.Match)
        {
            int x = other.GetComponent<M1A66BehaviourDrag>()._row;
            Vector2 def = other.GetComponent<M1A66BehaviourDrag>()._defaultPos;
            other.GetComponent<M1A66BehaviourDrag>()._row = this._row;

            foreach (var drag in _managerDragDrop._drags)
            {
                if (drag._row == other.GetComponent<M1A66BehaviourDrag>()._row && drag.gameObject != other)
                {
                    drag._row = x;
                    drag.GetComponent<RectTransform>().anchoredPosition = def;
                    drag.GetComponent<M1A66BehaviourDrag>().SwapDefaultPosition(other);
                    drag.UpdateCurrentPosition();
                    other.GetComponent<M1A66BehaviourDrag>().UpdateCurrentPosition();
                }
            }
        }
        
    }

    void GetCalificationType()
    {
        if (_managerDragDrop._TypeValidation == M1A66ManagerDragDrop.TypeValidation.Inmediata)
            _managerDragDrop.ImmediatelyValidation(_drag.GetComponent<M1A66BehaviourDrag>(),this);
        else
            StartCoroutine(_managerDragDrop.StateBtnValidar());
    }
}
