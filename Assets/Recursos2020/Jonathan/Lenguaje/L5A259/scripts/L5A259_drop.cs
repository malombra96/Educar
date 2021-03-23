using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class L5A259_drop : MonoBehaviour, IDropHandler
{
    /* [HideInInspector]  */
    [Header("Drag-IN")] public GameObject _drag;
    [Header("Grupo")] public L5A259_grpoDrag _group;
    public L5A259_managerDrag _L5A259_managerDrag;

    [Header("Math Setup")]
    [Tooltip("Ingrese el index correspondiente a la fila o columna")] public int _row;

    private void Awake()
    {
        _L5A259_managerDrag = FindObjectOfType<L5A259_managerDrag>();
        _L5A259_managerDrag._drops.Add(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject x = eventData.pointerDrag;

        if (x != null && _drag == null)
        {
            _drag = eventData.pointerDrag;

            if (_drag.GetComponent<L5A259_drag>())
            {
                UpdateSlotDrop();
                _drag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                _drag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                _drag.GetComponent<Image>().sprite = _drag.GetComponent<BehaviourSprite>()._default;
                _drag.GetComponent<L5A259_drag>().inDrop = true;
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
        if (_drag.GetComponent<L5A259_drag>()._drop == null)
        {
            _drag.GetComponent<L5A259_drag>()._drop = gameObject;
        }
        else
        {
            var previousDrop = _drag.GetComponent<L5A259_drag>()._drop.GetComponent<L5A259_drop>();
            previousDrop._drag = null;

            _drag.GetComponent<L5A259_drag>()._drop = gameObject;
        }
    }

    void UpdateRowDrag(GameObject other)
    {
        if (_L5A259_managerDrag._OperatingMethod == L5A259_managerDrag.OperatingMethod.Match)
        {
            int x = other.GetComponent<L5A259_drag>()._row;
            Vector2 def = other.GetComponent<L5A259_drag>()._defaultPos;
            other.GetComponent<L5A259_drag>()._row = this._row;

            foreach (var drag in _L5A259_managerDrag._drags)
            {
                if (drag._row == other.GetComponent<L5A259_drag>()._row && drag.gameObject != other)
                {
                    drag._row = x;
                    drag.GetComponent<RectTransform>().anchoredPosition = def;
                    drag.GetComponent<L5A259_drag>().SwapDefaultPosition(other);
                    drag.UpdateCurrentPosition();
                    other.GetComponent<L5A259_drag>().UpdateCurrentPosition();
                }
            }
        }

    }

    void GetCalificationType()
    {
        if (_L5A259_managerDrag._TypeValidation == L5A259_managerDrag.TypeValidation.Inmediata)
            _L5A259_managerDrag.ImmediatelyValidation(_drag.GetComponent<L5A259_drag>(), this);
        else
            StartCoroutine(_L5A259_managerDrag.StateBtnValidar());
    }
}
