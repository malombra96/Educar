using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class M10L001_BehaviourDrop : MonoBehaviour, IDropHandler
{
    [Header("Drag-IN")] public GameObject[] _drag;

    public bool _hasCollision;
    public string typeCollision;

    public M10L001_CollisionsList listCollision;

    M10L001_ManagerDragDrop _managerDragDrop;

    private void Awake()
    {
        _managerDragDrop = FindObjectOfType<M10L001_ManagerDragDrop>();
        _managerDragDrop._drops.Add(this);
        _drag = new GameObject[2];
        _hasCollision = false;
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject x = eventData.pointerDrag;
        
        if (x != null)
        {/* 
            print($"{x}");
            print($"{_drag[0]}");
            print($"{_drag[1]}"); */

            if (_drag[0] == null)
            {
                _drag[0] = eventData.pointerDrag;
                
                if (_drag[0].GetComponent<M10L001_BehaviourDrag>())
                {
                    UpdateSlotDrop(0);
                    _drag[0].GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                    _drag[0].GetComponent<CanvasGroup>().blocksRaycasts = true;
                    _drag[0].GetComponent<M10L001_BehaviourDrag>().inDrop = true;
                }
            }
            else if(_drag[1] == null && _drag[0].name != x.name)
            {
                 _drag[1] = eventData.pointerDrag;
                
                if (_drag[1].GetComponent<M10L001_BehaviourDrag>())
                {
                    UpdateSlotDrop(1);
                    _drag[1].GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                    _drag[1].GetComponent<CanvasGroup>().blocksRaycasts = true;
                    _drag[1].GetComponent<M10L001_BehaviourDrag>().inDrop = true;
                }
            }
            else
            {
                _drag[0] = null;
                _drag[1] = null;
            }
                //_drag = null;
        }
        else
        {
            print($"{x}{_drag[0]}{_drag[1]}");
        }
        
    }
    
    /// <summary>
    /// Relaciona el drop (this) con el elemento que hace drag
    /// </summary>
    void UpdateSlotDrop(int index)
    {
        if (_drag[index].GetComponent<M10L001_BehaviourDrag>()._drop == null)
        {
            _drag[index].GetComponent<M10L001_BehaviourDrag>()._drop = gameObject;
            _managerDragDrop.dropCount++;
        }
        else
        {
            var previousDrop = _drag[index].GetComponent<M10L001_BehaviourDrag>()._drop.GetComponent<M10L001_BehaviourDrop>();
            previousDrop._drag[0] = null;
            previousDrop._drag[1] = null;

            _drag[index].GetComponent<M10L001_BehaviourDrag>()._drop = gameObject;
        }

        _managerDragDrop.StateBtnTrazar();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        _hasCollision = true;
        
        if(!listCollision._collision.Contains(other.gameObject))
            listCollision._collision.Add(other.gameObject);
            if(!listCollision._collider.Contains(other))
                listCollision._collider.Add(other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        _hasCollision = false;
        listCollision._collider.Clear();
        listCollision._collision.Clear();
    }
}
