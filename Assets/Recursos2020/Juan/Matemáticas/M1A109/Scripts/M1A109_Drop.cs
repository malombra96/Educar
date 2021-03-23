using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class M1A109_Drop : MonoBehaviour, IDropHandler
{
    /* [HideInInspector]  */
    [Header("Drag-IN")] public List<GameObject> _drag;
    [Header("Drag-IN")] public List<Vector2> posiciones;
    M1A109_Manager _managerDragDrop;
    public bool Correcto;
    public GameObject caja;
    int p = 0;
    private void Awake()
    {
        _managerDragDrop = FindObjectOfType<M1A109_Manager>();
        _managerDragDrop._drops.Add(this);
        caja = transform.GetChild(0).gameObject;
        caja.GetComponent<Button>().onClick.AddListener(sacar);
    }
    
    public void OnDrop(PointerEventData eventData)
    {        
        GameObject x = eventData.pointerDrag;       
        if (x.GetComponent<M1A109_Drag>()) {
            if (p <= 2 && x != null && _drag[p] == null)
            {
                _drag[p] = eventData.pointerDrag;
                _drag[p].transform.SetParent(transform);
                caja.transform.SetAsLastSibling();
                UpdateSlotDrop();
                _drag[p].GetComponent<RectTransform>().anchoredPosition = posiciones[p];
                _drag[p].GetComponent<Image>().sprite = _drag[p].GetComponent<BehaviourSprite>()._default;
                _drag[p].GetComponent<M1A109_Drag>().inDrop = true;
                verficador();
                StartCoroutine(_managerDragDrop.StateBtnValidar());
                p++;
            }
        }
    }
    
    void verficador()
    {
        int calificar = 0;
        for(int x = 0; x < transform.childCount; x++)
        {
            var drag = transform.GetChild(x);
            if (drag.GetComponent<M1A109_Drag>() && drag.GetComponent<M1A109_Drag>()._DropRight[0].gameObject == gameObject)
                calificar++;
            
        }

        Correcto = (calificar == 3) ? true : false;

    }
    
    void UpdateSlotDrop()
    {
        if (_drag[p].GetComponent<M1A109_Drag>()._drop == null)
        {
            _drag[p].GetComponent<M1A109_Drag>()._drop = gameObject;
        }
        else
        {
            var previousDrop = _drag[p].GetComponent<M1A109_Drag>()._drop.GetComponent<M1A109_Drop>();
            previousDrop._drag = null;
            _drag[p].GetComponent<M1A109_Drag>()._drop = gameObject;
        }
    }

    void sacar()
    {
        caja.GetComponent<Button>().interactable = false;
        _managerDragDrop._controlAudio.PlayAudio(0);

        if (p >= 0 && _drag[0] != null)
        {            
            if (p > 0)
                p--;
            _drag[p].transform.SetParent(_managerDragDrop.transform);
            _drag[p].GetComponent<CanvasGroup>().blocksRaycasts = true;
            _drag[p].GetComponent<RectTransform>().anchoredPosition = _drag[p].GetComponent<M1A109_Drag>()._defaultPos;
            _drag[p].GetComponent<M1A109_Drag>()._drop = null;
            _drag[p] = null;
            verficador();
        }
        caja.GetComponent<Button>().interactable = true;
        StartCoroutine(_managerDragDrop.StateBtnValidar());
    }   
}
