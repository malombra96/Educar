using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class M10L001_BehaviourDrag : MonoBehaviour, IBeginDragHandler,IDragHandler, IEndDragHandler,IPointerClickHandler
{
    M10L001_ManagerDragDrop _managerDragDrop;

    public bool ItsInfinite;
    [HideInInspector]  public Vector2 _defaultPos;
    
    [HideInInspector] public Vector2 _currentPos;

    [HideInInspector]  [Header("Current Drop")] public GameObject _drop;

    [Header("States")]
    [HideInInspector]  public bool inDrop;
    
   
    int indexSibling;
    RectTransform _rectTransform;
    Image _image;

    private void Awake()
    {
        inDrop = false;

        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();

        _defaultPos = _rectTransform.anchoredPosition;
        
        UpdateCurrentPosition();
        
        _managerDragDrop = FindObjectOfType<M10L001_ManagerDragDrop>();
        _managerDragDrop._drags.Add(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _managerDragDrop._controlAudio.PlayAudio(0);
        
        inDrop = false;
        UpdateCurrentPosition();
        
        indexSibling = _rectTransform.GetSiblingIndex(); // Obtiene el index layout respecto a los demas objetos del mismo nivel
        var max = _rectTransform.parent.transform.childCount; // Obtiene el index max
        _rectTransform.SetSiblingIndex(max-1); // Posiciona el objeto sobre todos los demas

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float Y = Display.main.systemHeight/2;
        float X = Display.main.systemWidth/2;

        Vector2 newEventData = new Vector2((eventData.position.x - X),(eventData.position.y -Y))/_managerDragDrop.canvas.scaleFactor;
        _rectTransform.anchoredPosition = newEventData;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //print("EndDrag"+name);

        if (!inDrop)
        {
            //print(name+"return");
            _rectTransform.anchoredPosition = _currentPos;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        else
        {
            //print(name+"Did drop");
            //inDrop = false;
            UpdateCurrentPosition();

            if (ItsInfinite && _managerDragDrop.dropCount <= 4)
                CreateDrag();

        }
        
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        inDrop = false;

        if(_drop)
        {
            _drop.GetComponent<M10L001_BehaviourDrop>()._drag[0] = null;
            _drop.GetComponent<M10L001_BehaviourDrop>()._drag[1] = null;
            _drop = null;
            _managerDragDrop.dropCount--;
        }

        if(_managerDragDrop._OperatingMethod == M10L001_ManagerDragDrop.OperatingMethod.Create)
        {
            if(_managerDragDrop._drags.Contains(this) && !ItsInfinite)
            {
                _managerDragDrop._drags.Remove(this);
                Destroy(this.gameObject);
            }            
        }
            
            
        GetComponent<RectTransform>().anchoredPosition = _defaultPos;
        UpdateCurrentPosition();
        _managerDragDrop.StateBtnTrazar();

    }

    /// Actualiza la posicion default del drag 
    public void UpdateDefaultPosition() => _defaultPos = GetComponent<RectTransform>().anchoredPosition;
    /// Actualiza la posicion actual del drag 
    public void UpdateCurrentPosition() => _currentPos = GetComponent<RectTransform>().anchoredPosition;
    

    /////////////////////// CREATE NEW DRAG /////////////////////////////////////////////////////////

    void CreateDrag()
    {
        GameObject aux = Instantiate(gameObject,_managerDragDrop.transform);
        aux.name = name;
        
        ItsInfinite = false;
        InicializatNewDrag(aux);
        
    }

    void InicializatNewDrag(GameObject aux)
    {
        aux.GetComponent<RectTransform>().anchoredPosition = _defaultPos;
        aux.GetComponent<M10L001_BehaviourDrag>()._defaultPos = aux.GetComponent<RectTransform>().anchoredPosition;
        aux.GetComponent<M10L001_BehaviourDrag>().inDrop = false;
        aux.GetComponent<M10L001_BehaviourDrag>()._drop = null;
        aux.GetComponent<M10L001_BehaviourDrag>().ItsInfinite = true;
    }

}
