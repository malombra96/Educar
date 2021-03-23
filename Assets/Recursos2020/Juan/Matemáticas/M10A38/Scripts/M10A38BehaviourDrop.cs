using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class M10A38BehaviourDrop : MonoBehaviour, IDropHandler
{
    /*[HideInInspector]*/ [Header("Drag-IN")] public GameObject _drag;
    [Header("Grupo")] public M10A38BehaviourDropGroup _group;
    M10A38ManagerDragDrop _managerDragDrop;    

    [Header("Math Setup")]
    [Tooltip("Ingrese el index correspondiente a la fila o columna")] public int _row;

    private void Awake()
    {
        _managerDragDrop = FindObjectOfType<M10A38ManagerDragDrop>();
        _managerDragDrop._drops.Add(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject x = eventData.pointerDrag;
       
        if (x != null && _drag == null && x.GetComponent<M10A38BehaviourDrag>())
        {
            _drag = eventData.pointerDrag;
            UpdateSlotDrop();
            _drag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            _drag.GetComponent<CanvasGroup>().blocksRaycasts = true;
            _drag.GetComponent<Image>().sprite = _drag.GetComponent<BehaviourSprite>()._default;
            _drag.GetComponent<M10A38BehaviourDrag>().inDrop = true;
            UpdateRowDrag(_drag);
            GetCalificationType();
            
        }
        
        
    }


    /// <summary>
    /// Relaciona el drop (this) con el elemento que hace drag
    /// </summary>
    void UpdateSlotDrop()
    {
        if (_drag.GetComponent<M10A38BehaviourDrag>()._drop == null)
        {
            _drag.GetComponent<M10A38BehaviourDrag>()._drop = gameObject;
        }
        else
        {            
            var previousDrop = _drag.GetComponent<M10A38BehaviourDrag>()._drop.GetComponent<M10A38BehaviourDrop>();
            previousDrop._drag = null;
            
            _drag.GetComponent<M10A38BehaviourDrag>()._drop = gameObject;
            //_drag.GetComponent<M10A38BehaviourDrag>()._group = _group;
        }
        _drag.GetComponent<M10A38BehaviourDrag>()._group = _group;
    }

    void UpdateRowDrag(GameObject other)
    {
        if (_managerDragDrop._OperatingMethod == M10A38ManagerDragDrop.OperatingMethod.Math)
        {
            int x = other.GetComponent<M10A38BehaviourDrag>()._row;
            Vector2 def = other.GetComponent<M10A38BehaviourDrag>()._defaultPos;
            other.GetComponent<M10A38BehaviourDrag>()._row = this._row;

            foreach (var drag in _managerDragDrop._drags)
            {
                if (drag._row == other.GetComponent<M10A38BehaviourDrag>()._row && drag.gameObject != other)
                {
                    drag._row = x;
                    drag.GetComponent<RectTransform>().anchoredPosition = def;
                    //drag.GetComponent<M10A38BehaviourDrag>().SwapDefaultPosition(other);
                    drag.UpdateCurrentPosition();
                    other.GetComponent<M10A38BehaviourDrag>().UpdateCurrentPosition();
                }
            }
        }

    }

    void GetCalificationType()
    {   
        StartCoroutine(_managerDragDrop.StateBtnValidar());
    }
}
