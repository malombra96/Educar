using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class M0L69BehaviourDrop : MonoBehaviour, IDropHandler
{
    /*[HideInInspector]*/
    [Header("Drag-IN")] public GameObject _drag;
    [Header("Grupo")] public M0L69BehaviourDropGroup _group;
    M0L69ManagerDragDrop _managerDragDrop;

    [Header("Math Setup")]
    [Tooltip("Ingrese el index correspondiente a la fila o columna")] public int _row;

    private void Awake()
    {
        _managerDragDrop = FindObjectOfType<M0L69ManagerDragDrop>();
        _managerDragDrop._drops.Add(this);
    }

    public void OnDrop(PointerEventData eventData)
    {

        GameObject x = eventData.pointerDrag;
        if (x != null && _drag == null && x.name != "Canvas" && x.GetComponent<M0L69BehaviourDrag>().mover)
        {
            _drag = eventData.pointerDrag;
            var _drag2 = eventData;
            _group.acomodar();
            UpdateSlotDrop();
            _drag.GetComponent<RectTransform>().anchoredPosition = _group.drop.GetComponent<RectTransform>().anchoredPosition;
            _drag.GetComponent<CanvasGroup>().blocksRaycasts = true;
            _drag.GetComponent<Image>().sprite = _drag.GetComponent<BehaviourSprite>()._default;
            _drag.GetComponent<M0L69BehaviourDrag>().inDrop = true;
            _drag.GetComponent<M0L69BehaviourDrag>()._drop = _group.drop;
            UpdateRowDrag(_drag);
            GetCalificationType();
            if (_group.drop != gameObject)
            {
                _group.drop.GetComponent<M0L69BehaviourDrop>()._drag = _drag;
                _group.drop.GetComponent<M0L69BehaviourDrop>().OnDrop(_drag2);
                _drag = null;
            }
        }
    }

    /// <summary>
    /// Relaciona el drop (this) con el elemento que hace drag
    /// </summary>
    void UpdateSlotDrop()
    {
        if (_drag.GetComponent<M0L69BehaviourDrag>()._drop == null)
        {
            _drag.GetComponent<RectTransform>().anchoredPosition = _group.drop.GetComponent<RectTransform>().anchoredPosition;
        }
        else
        {
            var previousDrop = _drag.GetComponent<M0L69BehaviourDrag>()._drop.GetComponent<M0L69BehaviourDrop>();
            previousDrop._drag = null;
            _drag.GetComponent<M0L69BehaviourDrag>()._drop = _group.drop;
        }

    }

    void UpdateRowDrag(GameObject other)
    {
        if (_managerDragDrop._OperatingMethod == M0L69ManagerDragDrop.OperatingMethod.Match)
        {
            int x = other.GetComponent<M0L69BehaviourDrag>()._row;
            Vector2 def = other.GetComponent<M0L69BehaviourDrag>()._defaultPos;
            other.GetComponent<M0L69BehaviourDrag>()._row = this._row;

            foreach (var drag in _managerDragDrop._drags)
            {
                if (drag._row == other.GetComponent<M0L69BehaviourDrag>()._row && drag.gameObject != other)
                {
                    drag._row = x;
                    drag.GetComponent<RectTransform>().anchoredPosition = def;
                    drag.GetComponent<M0L69BehaviourDrag>().SwapDefaultPosition(other);
                    drag.UpdateCurrentPosition();
                    other.GetComponent<M0L69BehaviourDrag>().UpdateCurrentPosition();
                }
            }
        }

    }

    void GetCalificationType()
    {
        if (_managerDragDrop._TypeValidation == M0L69ManagerDragDrop.TypeValidation.Inmediata)
            _managerDragDrop.ImmediatelyValidation(_drag.GetComponent<M0L69BehaviourDrag>(), this);
        else
            StartCoroutine(_managerDragDrop.StateBtnValidar());
    }
}
