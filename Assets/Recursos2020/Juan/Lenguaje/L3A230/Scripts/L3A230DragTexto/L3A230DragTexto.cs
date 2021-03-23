using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class L3A230DragTexto : MonoBehaviour,IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Canvas canvas;
    public GameObject son;
    public Transform posdefaul;
    [HideInInspector] public bool indrop;
    L3A230ManagerDragDropTexto manager;
    GameObject child;
    // Start is called before the first frame update
    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        manager = FindObjectOfType<L3A230ManagerDragDropTexto>();
        //posdefaul = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {        
        GetComponent<RectTransform>().anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        manager.controlAudio.PlayAudio(0);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        createDrag();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        child.GetComponent<Image>().sprite = child.GetComponent<BehaviourSprite>()._default;
        Destroy(gameObject);
    }
    void createDrag()
    {
        var son = Instantiate(this, posdefaul);
        son.name = name;
        son.transform.SetSiblingIndex(transform.GetSiblingIndex());
        son.GetComponent<CanvasGroup>().blocksRaycasts = true;
        son.GetComponent<Image>().sprite = son.GetComponent<BehaviourSprite>()._selection;

        child = son.gameObject;
        transform.SetAsLastSibling();
        GetComponent<Image>().sprite = null;
        GetComponent<Image>().color = new Color32(0, 0, 0, 0);
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
