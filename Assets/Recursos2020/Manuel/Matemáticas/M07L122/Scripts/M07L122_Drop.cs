using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class M07L122_Drop : MonoBehaviour, IDropHandler
{

    [Header("Angle Drag Transportador")] public float angleZ;
    [HideInInspector] [Header("Drag-IN")] public GameObject _drag;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject x = eventData.pointerDrag;
        
        if (x != null && _drag == null)
        {
            _drag = eventData.pointerDrag;

            if (_drag.GetComponent<M07L122_Drag>())
            {
                UpdateSlotDrop();
                _drag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                _drag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                _drag.GetComponent<M07L122_Drag>().inDrop = true;

                _drag.GetComponent<RectTransform>().localEulerAngles = new Vector3(0,0,angleZ);
                _drag.GetComponent<Image>().color = new Color32(255,255,255,170);
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
        _drag.GetComponent<M07L122_Drag>()._drop = gameObject;
    }
}
