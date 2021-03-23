using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using NCalc.Domain;

public class M6A92_Drop : MonoBehaviour, IDropHandler
{
    public int value;
    public M6A92_barra _barra;
    /* [HideInInspector]  */
    [Header("Drag-IN")] public GameObject _drag;
    [Header("Grupo")] public M6A92_grupo _group;
    public M6A92_manager _managerDragDrop;

    [Header("Math Setup")]
    [Tooltip("Ingrese el index correspondiente a la fila o columna")] public int _row;

    private void Awake()
    {
        //_managerDragDrop = FindObjectOfType<ManagerDragDrop>();
        //_managerDragDrop._drops.Add(this);
        value = -1;
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject x = eventData.pointerDrag;

        if (x != null && _drag == null)
        {
            _drag = eventData.pointerDrag;

            if (_drag.GetComponent<M6A92_drag>())
            {
                UpdateSlotDrop();
                _drag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                _drag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                _drag.GetComponent<Image>().sprite = _drag.GetComponent<BehaviourSprite>()._default;
                _drag.GetComponent<M6A92_drag>().inDrop = true;
                UpdateRowDrag(_drag);
                GetCalificationType();
                value = _drag.GetComponent<M6A92_drag>().value;
                _barra.value = value;
            }
            else {
                _drag = null;
                value = -1;
                _barra.value = -1;
            }
                
        }

    }

    /// <summary>
    /// Relaciona el drop (this) con el elemento que hace drag
    /// </summary>
    void UpdateSlotDrop()
    {
        if (_drag.GetComponent<M6A92_drag>()._drop == null)
        {
            _drag.GetComponent<M6A92_drag>()._drop = gameObject;
        }
        else
        {
            var previousDrop = _drag.GetComponent<M6A92_drag>()._drop.GetComponent<M6A92_Drop>();
            previousDrop._drag = null;

            _drag.GetComponent<M6A92_drag>()._drop = gameObject;
        }
    }

    void UpdateRowDrag(GameObject other)
    {
        if (_managerDragDrop._OperatingMethod == M6A92_manager.OperatingMethod.Match)
        {
            int x = other.GetComponent<M6A92_drag>()._row;
            Vector2 def = other.GetComponent<M6A92_drag>()._defaultPos;
            other.GetComponent<M6A92_drag>()._row = this._row;

            foreach (var drag in _managerDragDrop._drags)
            {
                if (drag._row == other.GetComponent<M6A92_drag>()._row && drag.gameObject != other)
                {
                    drag._row = x;
                    drag.GetComponent<RectTransform>().anchoredPosition = def;
                    drag.GetComponent<M6A92_drag>().SwapDefaultPosition(other);
                    drag.UpdateCurrentPosition();
                    other.GetComponent<M6A92_drag>().UpdateCurrentPosition();
                }
            }
        }

    }

    void GetCalificationType()
    {
        if (_managerDragDrop._TypeValidation == M6A92_manager.TypeValidation.Inmediata)
            _managerDragDrop.ImmediatelyValidation(_drag.GetComponent<M6A92_drag>(), this);
        else
            StartCoroutine(_managerDragDrop.StateBtnValidar());
    }
}
