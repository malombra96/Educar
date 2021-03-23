using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class M1L71BehaviourDrop : MonoBehaviour, IDropHandler
{
    [HideInInspector] [Header("Drag-IN")] public GameObject _drag;
    
    M1L71ManagerDragDrop _managerDragDrop;

    [Header("Math Setup")]
    [Tooltip("Ingrese el index correspondiente a la fila o columna")] public int _row;

    private void Awake()
    {
        _managerDragDrop = FindObjectOfType<M1L71ManagerDragDrop>();
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
            _drag.GetComponent<Image>().sprite = _drag.GetComponent<BehaviourSprite>()._selection;
            _drag.GetComponent<Image>().SetNativeSize();
            _drag.GetComponent<M1L71BehaviourDrag>().inDrop = true;
            UpdateRowDrag(_drag);
            GetCalificationType();
        }       
    }

    /// <summary>
    /// Relaciona el drop (this) con el elemento que hace drag
    /// </summary>
    void UpdateSlotDrop()
    {
        if (_drag.GetComponent<M1L71BehaviourDrag>()._drop == null)
        {
            _drag.GetComponent<M1L71BehaviourDrag>()._drop = gameObject;
        }
        else
        {
            var previousDrop = _drag.GetComponent<M1L71BehaviourDrag>()._drop.GetComponent<M1L71BehaviourDrop>();
            previousDrop._drag = null;
           
            _drag.GetComponent<M1L71BehaviourDrag>()._drop = gameObject;            
        }
        
    }

    void UpdateRowDrag(GameObject other)
    {
       
        if (_managerDragDrop._OperatingMethod == M1L71ManagerDragDrop.OperatingMethod.Match)
        {
            int x = other.GetComponent<M1L71BehaviourDrag>()._row;
            Vector2 def = other.GetComponent<M1L71BehaviourDrag>()._defaultPos;
            other.GetComponent<M1L71BehaviourDrag>()._row = this._row;

            foreach (var drag in _managerDragDrop._drags)
            {
                if (drag._row == other.GetComponent<M1L71BehaviourDrag>()._row && drag.gameObject != other)
                {                    
                    drag._row = x;                    
                    drag.GetComponent<RectTransform>().anchoredPosition = def;
                    drag.GetComponent<M1L71BehaviourDrag>().SwapDefaultPosition(other);
                    drag.UpdateCurrentPosition();
                    other.GetComponent<M1L71BehaviourDrag>().UpdateCurrentPosition();
                }
            }
        }

    }

    void GetCalificationType()
    {
        if (_managerDragDrop._TypeValidation == M1L71ManagerDragDrop.TypeValidation.Inmediata)
            _managerDragDrop.ImmediatelyValidation(_drag.GetComponent<M1L71BehaviourDrag>(), this);
        else
            StartCoroutine(_managerDragDrop.StateBtnValidar());
    }
}
