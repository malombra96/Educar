using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class M0L69Drop : MonoBehaviour, IDropHandler
{
    /* [HideInInspector]  */
    [Header("Drag-IN")] public GameObject _drag;
    [Header("Grupo")] public M0L69DropGroup _group;
    M0L69Manager _managerDragDrop;

    [Header("Math Setup")]
    [Tooltip("Ingrese el index correspondiente a la fila o columna")] public int _row;

    private void Awake()
    {
        _managerDragDrop = FindObjectOfType<M0L69Manager>();
        _managerDragDrop._drops.Add(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject x = eventData.pointerDrag;

        if (x != null && _drag == null)
        {
            _drag = eventData.pointerDrag;

            if (_drag.GetComponent<M0L69Drag>())
            {
                UpdateSlotDrop();
                _drag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                _drag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                _drag.GetComponent<Image>().sprite = _drag.GetComponent<BehaviourSprite>()._default;
                _drag.GetComponent<M0L69Drag>().inDrop = true;
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
        if (_drag.GetComponent<M0L69Drag>()._drop == null)
        {
            _drag.GetComponent<M0L69Drag>()._drop = gameObject;
        }
        else
        {
            var previousDrop = _drag.GetComponent<M0L69Drag>()._drop.GetComponent<M0L69Drop>();
            previousDrop._drag = null;

            _drag.GetComponent<M0L69Drag>()._drop = gameObject;
        }
    }

    void UpdateRowDrag(GameObject other)
    {
        if (_managerDragDrop._OperatingMethod == M0L69Manager.OperatingMethod.Match)
        {
            int x = other.GetComponent<M0L69Drag>()._row;
            Vector2 def = other.GetComponent<M0L69Drag>()._defaultPos;
            other.GetComponent<M0L69Drag>()._row = this._row;

            foreach (var drag in _managerDragDrop._drags)
            {
                if (drag._row == other.GetComponent<M0L69Drag>()._row && drag.gameObject != other)
                {
                    drag._row = x;
                    drag.GetComponent<RectTransform>().anchoredPosition = def;
                    drag.GetComponent<M0L69Drag>().SwapDefaultPosition(other);
                    drag.UpdateCurrentPosition();
                    other.GetComponent<M0L69Drag>().UpdateCurrentPosition();
                }
            }
        }

    }

    void GetCalificationType()
    {
        if (_managerDragDrop._TypeValidation == M0L69Manager.TypeValidation.Inmediata)
            _managerDragDrop.ImmediatelyValidation(_drag.GetComponent<M0L69Drag>(), this);
        else
            StartCoroutine(_managerDragDrop.StateBtnValidar());
    }
}
