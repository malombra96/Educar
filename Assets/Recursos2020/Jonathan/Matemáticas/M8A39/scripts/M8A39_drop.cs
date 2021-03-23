using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class M8A39_drop : MonoBehaviour, IDropHandler
{

    [HideInInspector] [Header("Drag-IN")] public GameObject _drag;
    [Header("Grupo")] public M8A39_group _group;
    public M8A39_managerDrag _managerDragDrop;

    [Header("Math Setup")]
    [Tooltip("Ingrese el index correspondiente a la fila o columna")] public int _row;

    private void Awake()
    {

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
            _drag.GetComponent<Image>().sprite = _drag.GetComponent<BehaviourSprite>()._default;
            _drag.GetComponent<M8A39_drag>().inDrop = true;
            UpdateRowDrag(_drag);
            GetCalificationType();
        }

    }

    /// <summary>
    /// Relaciona el drop (this) con el elemento que hace drag
    /// </summary>
    void UpdateSlotDrop()
    {
        if (_drag.GetComponent<M8A39_drag>()._drop == null)
        {
            _drag.GetComponent<M8A39_drag>()._drop = gameObject;
        }
        else
        {
            var previousDrop = _drag.GetComponent<M8A39_drag>()._drop.GetComponent<M8A39_drop>();
            previousDrop._drag = null;

            _drag.GetComponent<M8A39_drag>()._drop = gameObject;
        }
    }

    void UpdateRowDrag(GameObject other)
    {
        if (_managerDragDrop._OperatingMethod == M8A39_managerDrag.OperatingMethod.Math)
        {
            int x = other.GetComponent<M8A39_drag>()._row;
            Vector2 def = other.GetComponent<M8A39_drag>()._defaultPos;
            other.GetComponent<M8A39_drag>()._row = this._row;

            foreach (var drag in _managerDragDrop._drags)
            {
                if (drag._row == other.GetComponent<M8A39_drag>()._row && drag.gameObject != other)
                {
                    drag._row = x;
                    drag.GetComponent<RectTransform>().anchoredPosition = def;
                    drag.GetComponent<M8A39_drag>().SwapDefaultPosition(other);
                    drag.UpdateCurrentPosition();
                    other.GetComponent<M8A39_drag>().UpdateCurrentPosition();
                }
            }
        }

    }

    void GetCalificationType()
    {
        if (_managerDragDrop._TypeValidation == M8A39_managerDrag.TypeValidation.Inmediata)
            _managerDragDrop.ImmediatelyValidation(_drag.GetComponent<M8A39_drag>(), this);
        else
            StartCoroutine(_managerDragDrop.StateBtnValidar());
    }
}
