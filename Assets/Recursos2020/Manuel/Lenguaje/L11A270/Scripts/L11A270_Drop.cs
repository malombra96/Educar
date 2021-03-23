using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class L11A270_Drop : MonoBehaviour, IDropHandler
{
    [HideInInspector] [Header("Drag-IN")] public GameObject _drag;
    [Header("Grupo")] public BehaviourDropGroup _group;
    L11A270_ManagerDD _managerDragDrop;

    [Header("Math Setup")]
    [Tooltip("Ingrese el index correspondiente a la fila o columna")] public int _row;

    private void Awake()
    {
        _managerDragDrop = FindObjectOfType<L11A270_ManagerDD>();
        _managerDragDrop._drops.Add(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject x = eventData.pointerDrag;
        
        if (x != null && _drag == null)
        {
            _drag = eventData.pointerDrag;

            if (_drag.GetComponent<L11A270_Drag>())
            {
                UpdateSlotDrop();
                _drag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                _drag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                _drag.GetComponent<Image>().sprite = _drag.GetComponent<BehaviourSprite>()._default;
                _drag.GetComponent<L11A270_Drag>().inDrop = true;
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
        if (_drag.GetComponent<L11A270_Drag>()._drop == null)
        {
            _drag.GetComponent<L11A270_Drag>()._drop = gameObject;
        }
        else
        {
            var previousDrop = _drag.GetComponent<L11A270_Drag>()._drop.GetComponent<L11A270_Drop>();
            previousDrop._drag = null;

            _drag.GetComponent<L11A270_Drag>()._drop = gameObject;
        }
    }

    void UpdateRowDrag(GameObject other)
    {
        if(_managerDragDrop._OperatingMethod == L11A270_ManagerDD.OperatingMethod.Match)
        {
            int x = other.GetComponent<L11A270_Drag>()._row;
            Vector2 def = other.GetComponent<L11A270_Drag>()._defaultPos;
            other.GetComponent<L11A270_Drag>()._row = this._row;

            foreach (var drag in _managerDragDrop._drags)
            {
                if (drag._row == other.GetComponent<L11A270_Drag>()._row && drag.gameObject != other)
                {
                    drag._row = x;
                    drag.GetComponent<RectTransform>().anchoredPosition = def;
                    drag.GetComponent<L11A270_Drag>().SwapDefaultPosition(other);
                    drag.UpdateCurrentPosition();
                    other.GetComponent<L11A270_Drag>().UpdateCurrentPosition();
                }
            }
        }
        
    }

    void GetCalificationType()
    {
        if (_managerDragDrop._TypeValidation == L11A270_ManagerDD.TypeValidation.Inmediata)
            _managerDragDrop.ImmediatelyValidation(_drag.GetComponent<L11A270_Drag>(),this);
        else
            StartCoroutine(_managerDragDrop.StateBtnValidar());
    }
}
