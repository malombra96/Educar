using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class M8A127_Drop : MonoBehaviour, IDropHandler
{
    /* [HideInInspector]  */
    [Header("Drag-IN")] public GameObject _drag;
    [Header("Grupo")] public M8A127_groupDrag _group;
    public M8A127_managerDrag _M8A127_managerDrag;

    [Header("Math Setup")]
    [Tooltip("Ingrese el index correspondiente a la fila o columna")] public int _row;

    private void Awake()
    {
        _M8A127_managerDrag = FindObjectOfType<M8A127_managerDrag>();
        _M8A127_managerDrag._drops.Add(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject x = eventData.pointerDrag;

        if (x != null && _drag == null)
        {
            _drag = eventData.pointerDrag;

            if (_drag.GetComponent<M8A127_drag>())
            {
                UpdateSlotDrop();
                _drag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                _drag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                _drag.GetComponent<Image>().sprite = _drag.GetComponent<BehaviourSprite>()._default;
                _drag.GetComponent<M8A127_drag>().inDrop = true;
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
        if (_drag.GetComponent<M8A127_drag>()._drop == null)
        {
            _drag.GetComponent<M8A127_drag>()._drop = gameObject;
        }
        else
        {
            var previousDrop = _drag.GetComponent<M8A127_drag>()._drop.GetComponent<M8A127_Drop>();
            previousDrop._drag = null;

            _drag.GetComponent<M8A127_drag>()._drop = gameObject;
        }
    }

    void UpdateRowDrag(GameObject other)
    {
        if (_M8A127_managerDrag._OperatingMethod == M8A127_managerDrag.OperatingMethod.Match)
        {
            int x = other.GetComponent<M8A127_drag>()._row;
            Vector2 def = other.GetComponent<M8A127_drag>()._defaultPos;
            other.GetComponent<M8A127_drag>()._row = this._row;

            foreach (var drag in _M8A127_managerDrag._drags)
            {
                if (drag._row == other.GetComponent<M8A127_drag>()._row && drag.gameObject != other)
                {
                    drag._row = x;
                    drag.GetComponent<RectTransform>().anchoredPosition = def;
                    drag.GetComponent<M8A127_drag>().SwapDefaultPosition(other);
                    drag.UpdateCurrentPosition();
                    other.GetComponent<M8A127_drag>().UpdateCurrentPosition();
                }
            }
        }

    }

    void GetCalificationType()
    {
        if (_M8A127_managerDrag._TypeValidation == M8A127_managerDrag.TypeValidation.Inmediata)
            _M8A127_managerDrag.ImmediatelyValidation(_drag.GetComponent<M8A127_drag>(), this);
        else
            StartCoroutine(_M8A127_managerDrag.StateBtnValidar());
    }
}
