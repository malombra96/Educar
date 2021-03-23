using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class M06A087_BehaviourDrop : MonoBehaviour, IDropHandler
{
    [HideInInspector] [Header("Drag-IN")] public GameObject _drag;
    [Header("Grupo")] public BehaviourDropGroup _group;
    M06A087_ManagerDD _managerDragDrop;

    [Header("Math Setup")]
    [Tooltip("Ingrese el index correspondiente a la fila o columna")] public int _row;

    private void Awake()
    {
        _managerDragDrop = FindObjectOfType<M06A087_ManagerDD>();
        _managerDragDrop._drops.Add(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject x = eventData.pointerDrag;
        
        if (x != null && _drag == null)
        {
            _drag = eventData.pointerDrag;

            if (_drag.GetComponent<M06A087_BehaviourDrag>())
            {
                UpdateSlotDrop();
                _drag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                _drag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                _drag.GetComponent<Image>().sprite = _drag.GetComponent<BehaviourSprite>()._default;
                _drag.GetComponent<M06A087_BehaviourDrag>().inDrop = true;
                UpdateRowDrag(_drag);
                GetCalificationType();

                _managerDragDrop._weightSelect.text = _drag.name.Split('_')[1];
            }
            else
            {
                _drag = null;
                _managerDragDrop._weightSelect.text = "";

            }
                
        }
        
    }
    
    /// <summary>
    /// Relaciona el drop (this) con el elemento que hace drag
    /// </summary>
    void UpdateSlotDrop()
    {
        if (_drag.GetComponent<M06A087_BehaviourDrag>()._drop == null)
        {
            _drag.GetComponent<M06A087_BehaviourDrag>()._drop = gameObject;
        }
        else
        {
            var previousDrop = _drag.GetComponent<M06A087_BehaviourDrag>()._drop.GetComponent<M06A087_BehaviourDrop>();
            previousDrop._drag = null;

            _drag.GetComponent<M06A087_BehaviourDrag>()._drop = gameObject;
        }
    }

    void UpdateRowDrag(GameObject other)
    {
        if(_managerDragDrop._OperatingMethod == M06A087_ManagerDD.OperatingMethod.Match)
        {
            int x = other.GetComponent<M06A087_BehaviourDrag>()._row;
            Vector2 def = other.GetComponent<M06A087_BehaviourDrag>()._defaultPos;
            other.GetComponent<M06A087_BehaviourDrag>()._row = this._row;

            foreach (var drag in _managerDragDrop._drags)
            {
                if (drag._row == other.GetComponent<M06A087_BehaviourDrag>()._row && drag.gameObject != other)
                {
                    drag._row = x;
                    drag.GetComponent<RectTransform>().anchoredPosition = def;
                    drag.GetComponent<M06A087_BehaviourDrag>().SwapDefaultPosition(other);
                    drag.UpdateCurrentPosition();
                    other.GetComponent<M06A087_BehaviourDrag>().UpdateCurrentPosition();
                }
            }
        }
        
    }

    void GetCalificationType()
    {
        if (_managerDragDrop._TypeValidation == M06A087_ManagerDD.TypeValidation.Inmediata)
            _managerDragDrop.ImmediatelyValidation(_drag.GetComponent<M06A087_BehaviourDrag>(),this);
        else
            StartCoroutine(_managerDragDrop.StateBtnValidar());
    }
}
