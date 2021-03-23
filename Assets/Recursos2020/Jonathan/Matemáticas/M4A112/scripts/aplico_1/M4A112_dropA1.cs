using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class M4A112_dropA1 : MonoBehaviour, IDropHandler
{
    /* [HideInInspector]  */
    [Header("Drag-IN")] public GameObject _drag;
    [Header("Grupo")] public M4A112_groupA1 _group;
    public M4A112_managerDragA1 _M4A112_managerDragA1;

    [Header("Math Setup")]
    [Tooltip("Ingrese el index correspondiente a la fila o columna")] public int _row;

    private void Awake()
    {
        _M4A112_managerDragA1 = FindObjectOfType<M4A112_managerDragA1>();
        _M4A112_managerDragA1._drops.Add(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject x = eventData.pointerDrag;

        if (x != null && _drag == null)
        {
            _drag = eventData.pointerDrag;

            if (_drag.GetComponent<M4A112_dragA1>())
            {
                UpdateSlotDrop();
                _drag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                _drag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                _drag.GetComponent<Image>().sprite = _drag.GetComponent<BehaviourSprite>()._default;
                _drag.GetComponent<M4A112_dragA1>().inDrop = true;
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
        if (_drag.GetComponent<M4A112_dragA1>()._drop == null)
        {
            _drag.GetComponent<M4A112_dragA1>()._drop = gameObject;
        }
        else
        {
            var previousDrop = _drag.GetComponent<M4A112_dragA1>()._drop.GetComponent<M4A112_dropA1>();
            previousDrop._drag = null;

            _drag.GetComponent<M4A112_dragA1>()._drop = gameObject;
        }
    }

    void UpdateRowDrag(GameObject other)
    {
        if (_M4A112_managerDragA1._OperatingMethod == M4A112_managerDragA1.OperatingMethod.Match)
        {
            int x = other.GetComponent<M4A112_dragA1>()._row;
            Vector2 def = other.GetComponent<M4A112_dragA1>()._defaultPos;
            other.GetComponent<M4A112_dragA1>()._row = this._row;

            foreach (var drag in _M4A112_managerDragA1._drags)
            {
                if (drag._row == other.GetComponent<M4A112_dragA1>()._row && drag.gameObject != other)
                {
                    drag._row = x;
                    drag.GetComponent<RectTransform>().anchoredPosition = def;
                    drag.GetComponent<M4A112_dragA1>().SwapDefaultPosition(other);
                    drag.UpdateCurrentPosition();
                    other.GetComponent<M4A112_dragA1>().UpdateCurrentPosition();
                }
            }
        }

    }

    void GetCalificationType()
    {
        if (_M4A112_managerDragA1._TypeValidation == M4A112_managerDragA1.TypeValidation.Inmediata)
            _M4A112_managerDragA1.ImmediatelyValidation(_drag.GetComponent<M4A112_dragA1>(), this);
        else
            StartCoroutine(_M4A112_managerDragA1.StateBtnValidar());
    }
}
