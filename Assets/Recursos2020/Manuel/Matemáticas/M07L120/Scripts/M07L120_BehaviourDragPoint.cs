using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class M07L120_BehaviourDragPoint : MonoBehaviour,IBeginDragHandler,IDragHandler, IEndDragHandler
{
    M07L120_ManagerPoint _ManagerPoint;
    RectTransform _rect;
    public  Vector2 _current;
    public  Vector2 _default = new Vector2(-236,36);

    void Start()
    {
        _ManagerPoint = transform.parent.GetComponent<M07L120_ManagerPoint>();
        _rect = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _rect.GetComponent<CanvasGroup>().blocksRaycasts = false;
        _ManagerPoint._controlAudio.PlayAudio(0);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rect.anchoredPosition += eventData.delta/FindObjectOfType<Canvas>().scaleFactor;
        GetComponent<Image>().color = _ManagerPoint._selection;
        _ManagerPoint._validar.interactable = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {  
        GetComponent<Image>().color = _ManagerPoint._default;
        _ManagerPoint.difference.Clear();

        for (int i = 0; i < _ManagerPoint._positions.Count; i++)
            _ManagerPoint.difference.Add( Vector2.Distance(_rect.anchoredPosition,_ManagerPoint._positions[i]));

        int min = _ManagerPoint.difference.IndexOf(_ManagerPoint.difference.Min());

        _current = _ManagerPoint._coordenadas[min];

        _rect.anchoredPosition = _ManagerPoint._positions[min];
        _ManagerPoint.X.text = _ManagerPoint._coordenadas[min].x.ToString();
        _ManagerPoint.Y.text = _ManagerPoint._coordenadas[min].y.ToString();

        _rect.GetComponent<CanvasGroup>().blocksRaycasts = true;
        _ManagerPoint._validar.interactable = true;

    }
}
