using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class M7A118_drop : MonoBehaviour, IDropHandler
{
    [Header("Drag-IN")] public GameObject _drag;
    [Header("Grupo")] public M7A118_grupoDrag _group;
    public M7A118_managerDrag _M7A118_managerDrag;

    [Header("Math Setup")]
    [Tooltip("Ingrese el index correspondiente a la fila o columna")] public int _row;

    private void Awake()
    {
        _M7A118_managerDrag = FindObjectOfType<M7A118_managerDrag>();
        _M7A118_managerDrag._drops.Add(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject x = eventData.pointerDrag;

        if (x != null && _drag == null)
        {
            _drag = eventData.pointerDrag;

            if (_drag.GetComponent<M7A118_drag>())
            {
                UpdateSlotDrop();
                _drag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                _drag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                _drag.GetComponent<Image>().sprite = _drag.GetComponent<BehaviourSprite>()._default;
                _drag.GetComponent<M7A118_drag>().inDrop = true;
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
        if (_drag.GetComponent<M7A118_drag>()._drop == null)
        {
            _drag.GetComponent<M7A118_drag>()._drop = gameObject;
        }
        else
        {
            var previousDrop = _drag.GetComponent<M7A118_drag>()._drop.GetComponent<M7A118_drop>();
            previousDrop._drag = null;

            _drag.GetComponent<M7A118_drag>()._drop = gameObject;
        }
    }

    void UpdateRowDrag(GameObject other)
    {
        if (_M7A118_managerDrag._OperatingMethod == M7A118_managerDrag.OperatingMethod.Match)
        {
            int x = other.GetComponent<M7A118_drag>()._row;
            Vector2 def = other.GetComponent<M7A118_drag>()._defaultPos;
            other.GetComponent<M7A118_drag>()._row = this._row;

            foreach (var drag in _M7A118_managerDrag._drags)
            {
                if (drag._row == other.GetComponent<M7A118_drag>()._row && drag.gameObject != other)
                {
                    drag._row = x;
                    drag.GetComponent<RectTransform>().anchoredPosition = def;
                    drag.GetComponent<M7A118_drag>().SwapDefaultPosition(other);
                    drag.UpdateCurrentPosition();
                    other.GetComponent<M7A118_drag>().UpdateCurrentPosition();
                }
            }
        }

    }

    void GetCalificationType()
    {
        if (_M7A118_managerDrag._TypeValidation == M7A118_managerDrag.TypeValidation.Inmediata)
            _M7A118_managerDrag.ImmediatelyValidation(_drag.GetComponent<M7A118_drag>(), this);
        else
            StartCoroutine(_M7A118_managerDrag.StateBtnValidar());
    }
}
