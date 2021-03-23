using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class M9A79_drop : MonoBehaviour, IDropHandler
{
    /* [HideInInspector]  */
    [Header("Drag-IN")] public GameObject _drag;
    [Header("Grupo")] public M9A79_groupDrag _group;
    public M9A79_managerDragDrop _M9A79_managerDragDrop;

    [Header("Math Setup")]
    [Tooltip("Ingrese el index correspondiente a la fila o columna")] public int _row;

    private void Awake()
    {
        _M9A79_managerDragDrop = FindObjectOfType<M9A79_managerDragDrop>();
        _M9A79_managerDragDrop._drops.Add(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject x = eventData.pointerDrag;

        if (x != null && _drag == null)
        {
            _drag = eventData.pointerDrag;

            if (_drag.GetComponent<M9A79_drag>())
            {
                UpdateSlotDrop();
                _drag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                _drag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                _drag.GetComponent<Image>().sprite = _drag.GetComponent<BehaviourSprite>()._default;
                _drag.GetComponent<M9A79_drag>().inDrop = true;
                UpdateRowDrag(_drag);
                GetCalificationType();
            }
            else
                _drag = null;
        }

    }

    /// <summary>
    /// Relaciona el drop (this) con el elemento que hace drag
    /// </summary>
    void UpdateSlotDrop()
    {
        if (_drag.GetComponent<M9A79_drag>()._drop == null)
        {
            _drag.GetComponent<M9A79_drag>()._drop = gameObject;
        }
        else
        {
            var previousDrop = _drag.GetComponent<M9A79_drag>()._drop.GetComponent<M9A79_drop>();
            previousDrop._drag = null;

            _drag.GetComponent<M9A79_drag>()._drop = gameObject;
        }
    }

    void UpdateRowDrag(GameObject other)
    {
        if (_M9A79_managerDragDrop._OperatingMethod == M9A79_managerDragDrop.OperatingMethod.Match)
        {
            int x = other.GetComponent<M9A79_drag>()._row;
            Vector2 def = other.GetComponent<M9A79_drag>()._defaultPos;
            other.GetComponent<M9A79_drag>()._row = this._row;

            foreach (var drag in _M9A79_managerDragDrop._drags)
            {
                if (drag._row == other.GetComponent<M9A79_drag>()._row && drag.gameObject != other)
                {
                    drag._row = x;
                    drag.GetComponent<RectTransform>().anchoredPosition = def;
                    drag.GetComponent<M9A79_drag>().SwapDefaultPosition(other);
                    drag.UpdateCurrentPosition();
                    other.GetComponent<M9A79_drag>().UpdateCurrentPosition();
                }
            }
        }

    }

    void GetCalificationType()
    {
        if (_M9A79_managerDragDrop._TypeValidation == M9A79_managerDragDrop.TypeValidation.Inmediata)
            _M9A79_managerDragDrop.ImmediatelyValidation(_drag.GetComponent<M9A79_drag>(), this);
        else
            StartCoroutine(_M9A79_managerDragDrop.StateBtnValidar());
    }
}
