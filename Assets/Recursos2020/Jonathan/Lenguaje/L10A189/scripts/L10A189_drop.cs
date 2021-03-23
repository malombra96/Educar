using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class L10A189_drop : MonoBehaviour, IDropHandler
{
    [Header("Drag-IN")] public GameObject _drag;
    [Header("Grupo")] public L10A189_grop _group;
    public L10A189_manager _L10A189_manager;

    [Header("Math Setup")]
    [Tooltip("Ingrese el index correspondiente a la fila o columna")] public int _row;

    private void Awake()
    {
        _L10A189_manager = FindObjectOfType<L10A189_manager>();
        _L10A189_manager._drops.Add(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject x = eventData.pointerDrag;

        if (x != null && _drag == null)
        {
            _drag = eventData.pointerDrag;

            if (_drag.GetComponent<L10A189_drag>())
            {
                UpdateSlotDrop();
                _drag.transform.parent = _drag.GetComponent<L10A189_drag>().content.transform;
                _drag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                _drag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                _drag.GetComponent<Image>().sprite = _drag.GetComponent<BehaviourSprite>()._default;
                _drag.GetComponent<L10A189_drag>().inDrop = true;
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
        if (_drag.GetComponent<L10A189_drag>()._drop == null)
        {
            _drag.GetComponent<L10A189_drag>()._drop = gameObject;
        }
        else
        {
            var previousDrop = _drag.GetComponent<L10A189_drag>()._drop.GetComponent<L10A189_drop>();
            previousDrop._drag = null;

            _drag.GetComponent<L10A189_drag>()._drop = gameObject;
        }
    }

    void UpdateRowDrag(GameObject other)
    {
        if (_L10A189_manager._OperatingMethod == L10A189_manager.OperatingMethod.Match)
        {
            int x = other.GetComponent<L10A189_drag>()._row;
            Vector2 def = other.GetComponent<L10A189_drag>()._defaultPos;
            other.GetComponent<L10A189_drag>()._row = this._row;

            foreach (var drag in _L10A189_manager._drags)
            {
                if (drag._row == other.GetComponent<L10A189_drag>()._row && drag.gameObject != other)
                {
                    drag._row = x;
                    drag.GetComponent<RectTransform>().anchoredPosition = def;
                    drag.GetComponent<L10A189_drag>().SwapDefaultPosition(other);
                    drag.UpdateCurrentPosition();
                    other.GetComponent<L10A189_drag>().UpdateCurrentPosition();
                }
            }
        }

    }

    void GetCalificationType()
    {
        if (_L10A189_manager._TypeValidation == L10A189_manager.TypeValidation.Inmediata)
            _L10A189_manager.ImmediatelyValidation(_drag.GetComponent<L10A189_drag>(), this);
        else
            StartCoroutine(_L10A189_manager.StateBtnValidar());
    }
}
