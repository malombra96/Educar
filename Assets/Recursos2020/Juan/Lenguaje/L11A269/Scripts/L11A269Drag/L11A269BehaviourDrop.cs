using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class L11A269BehaviourDrop : MonoBehaviour, IDropHandler
{
    /* [HideInInspector]  */
    [Header("Drag-IN")] public List<GameObject> _drag;
    L11A269ManagerDragDrop _managerDragDrop;

    [Header("Math Setup")]
    [Tooltip("Ingrese el index correspondiente a la fila o columna")] public int _row;
    [HideInInspector] public int i = 0;
    private void Awake()
    {
        _managerDragDrop = FindObjectOfType<L11A269ManagerDragDrop>();
        _managerDragDrop._drops.Add(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject x = eventData.pointerDrag;
        if (i <= 2)
        {
            if (x != null && _drag[i] == null)
            {
                _drag[i] = eventData.pointerDrag;

                if (_drag[i].GetComponent<L11A269BehaviourDrag>())
                {
                    UpdateSlotDrop();

                    if (i == 0)
                        _drag[i].GetComponent<RectTransform>().anchoredPosition =
                            new Vector2(GetComponent<RectTransform>().anchoredPosition.x, GetComponent<RectTransform>().anchoredPosition.y + 100);
                    else if (i == 1)
                        _drag[i].GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                    else
                        _drag[i].GetComponent<RectTransform>().anchoredPosition =
                            new Vector2(GetComponent<RectTransform>().anchoredPosition.x, GetComponent<RectTransform>().anchoredPosition.y - 100);

                    _drag[i].GetComponent<CanvasGroup>().blocksRaycasts = true;
                    _drag[i].GetComponent<Image>().sprite = _drag[i].GetComponent<BehaviourSprite>()._default;
                    _drag[i].GetComponent<L11A269BehaviourDrag>().inDrop = true;
                    GetCalificationType();
                    i++;
                }
                else
                    _drag = null;
            }
        }

    }

    /// <summary>
    /// Relaciona el drop (this) con el elemento que hace drag
    /// </summary>
    void UpdateSlotDrop()
    {
        //for (int x = 0; x < _drag.Count; x++)
        //{
        //    if (_drag[x].GetComponent<L11A269BehaviourDrag>()._drop == null)
        //    {
        //        _drag[x].GetComponent<L11A269BehaviourDrag>()._drop = gameObject;
        //        break;
        //    }
        //}

        if (_drag[i].GetComponent<L11A269BehaviourDrag>()._drop == null)
            _drag[i].GetComponent<L11A269BehaviourDrag>()._drop = gameObject;
        else
        {
            var previousDrop = _drag[i].GetComponent<L11A269BehaviourDrag>()._drop.GetComponent<L11A269BehaviourDrop>();
            previousDrop._drag = null;

            _drag[i].GetComponent<L11A269BehaviourDrag>()._drop = gameObject;
        }
    }

    void GetCalificationType()
    {
        StartCoroutine(_managerDragDrop.StateBtnValidar());
    }
}
