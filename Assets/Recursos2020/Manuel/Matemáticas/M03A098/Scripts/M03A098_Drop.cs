using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class M03A098_Drop : MonoBehaviour, IDropHandler
{
    //[HideInInspector] [Header("Drag-IN")] public GameObject _drag;
    [Header("Grupo")] public BehaviourDropGroup _group;
    M03A098_ManagerDragDrop _managerDragDrop;

    [Header("Math Setup")]
    [Tooltip("Ingrese el index correspondiente a la fila o columna")] public int _row;

    [Header("Correct # Drag")] public int _correctDrag;
    [Header("Count Drag")] public int _countDrag;

    [Header("Initial Value Drag")] public int _initialValue;

    [Header("Max Value Drag Child")] public int _maxValue;

    private void Awake()
    {
        _countDrag = _initialValue;
        _managerDragDrop = FindObjectOfType<M03A098_ManagerDragDrop>();
        _managerDragDrop._drops.Add(this);
    }

     public void OnDrop(PointerEventData eventData)
    {
        GameObject x = eventData.pointerDrag;
        
        if (x != null ) //&& _drag == null
        {
            //_drag = eventData.pointerDrag;

            if (x.GetComponent<M03A098_Drag>() && x.GetComponent<M03A098_Drag>()._DropRight.Count > 0)
            {
                if(x.GetComponent<M03A098_Drag>()._DropRight.Contains(this) && transform.childCount < _maxValue)
                {
                    _countDrag++;
                    x.GetComponent<RectTransform>().SetParent(transform);
                    UpdateSlotDrop(x);
                }
            }
            
        }
        
    } 

    /// <summary>
    /// Relaciona el drop (this) con el elemento que hace drag
    /// </summary>
    void UpdateSlotDrop(GameObject x)
    {
        if (x.GetComponent<M03A098_Drag>()._drop == null)
        {
            x.GetComponent<M03A098_Drag>()._drop = gameObject;
        }
        else
        {
            M03A098_Drop previousDrop = x.GetComponent<M03A098_Drag>()._drop.GetComponent<M03A098_Drop>();
            previousDrop._countDrag--;

            x.GetComponent<M03A098_Drag>()._drop = gameObject;
        }
        
        StartCoroutine(_managerDragDrop.StateBtnValidar()); 
    }

/*    void UpdateRowDrag(GameObject other)
    {
        if(_managerDragDrop._OperatingMethod == M03A098_ManagerDragDrop.OperatingMethod.Match)
        {
            int x = other.GetComponent<M03A098_Drag>()._row;
            Vector2 def = other.GetComponent<M03A098_Drag>()._defaultPos;
            other.GetComponent<M03A098_Drag>()._row = this._row;

            foreach (var drag in _managerDragDrop._drags)
            {
                if (drag._row == other.GetComponent<M03A098_Drag>()._row && drag.gameObject != other)
                {
                    drag._row = x;
                    drag.GetComponent<RectTransform>().anchoredPosition = def;
                    drag.GetComponent<M03A098_Drag>().SwapDefaultPosition(other);
                    drag.UpdateCurrentPosition();
                    other.GetComponent<M03A098_Drag>().UpdateCurrentPosition();
                }
            }
        }
        
    }

     void GetCalificationType()
    {
        if (_managerDragDrop._TypeValidation == M03A098_ManagerDragDrop.TypeValidation.Inmediata)
            _managerDragDrop.ImmediatelyValidation(_drag.GetComponent<M03A098_Drag>(),this);
        else
            StartCoroutine(_managerDragDrop.StateBtnValidar());
    } */
}
